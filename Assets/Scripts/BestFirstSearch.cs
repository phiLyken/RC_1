﻿using UnityEngine;
using System.Collections.Generic;

///////////////////////
//
// A simple 2D implementation of the best first search algoirithm (robotics motion planning)
//
public class BestFirstSearch : MonoBehaviour
{
	public int StartX;
	public int StartY;
	
	public int EndX;
	public int EndY;
	
	public int width;
	public int height;
	
	public float distance;
	
	///////////////////////
	//
	// A search node - nodes are visited from a neighboring node in the grid
	//
	public class Node
	{
		public int x;			// the grid (x, y) coordinate for the node
		public int y;
		public Node parent;		// the neighboring node (the source of the visit)
		public float cost;
		
		public Node(int _x, int _y, float _cost, Node _parent)
		{
			x = _x;
			y = _y;
			cost = _cost;
			parent = _parent;
		}
	}
	
	private bool[,] m_collisiongrid;	// the collision grid (true == obstacle)
	private bool[,] m_visit;			// track the grid points that have already been visited in the search
	private int m_width;				// the width of the grid
	private int m_height;				// the height of the grid
	private int m_startx;				// the start (x, y) coordinate
	private int m_starty;
	private List<Node>[] m_open;		// the OPEN list (these are the promising nodes that we've visited)
	private int m_best;					// the index in our OPEN list of the best (most promising) node (i.e. a priority queue)
	
	///////////////////////
	//
	// Constructor - allocate space for the width x height grid 
	//	

	public void Awake(){
		CurrentPath = new List<Node>();
		m_collisiongrid = new bool[width,height];
		m_visit = new bool[width,height];
		m_width = width;
		m_height = height;
		

	}

	///////////////////////
	//
	// Add obstacles to the grid
	//	
	public void AddObstacle(int x, int y)
	{
		if(TestWidthBounds(x) && TestHeightBounds(y))
			m_collisiongrid[x,y] = true;
	}
	Vector3 GetPos(int x, int y){
		return 	new Vector3( x * distance, 0, y*distance);
	}
	void Update(){
		FindPath( StartX, StartY, EndX, EndY);	
	}
	
	void OnDrawGizmos(){
		
		if( Application.isEditor && Application.isPlaying){
			
			
			
			Gizmos.color = Color.blue;
			for(int i = 0; i < width; i++){
				for(int j = 0; j < height; j++){
					Gizmos.DrawWireSphere( GetPos(i,j), 0.5f);
				}
			}
			Gizmos.color = Color.red;
			
			if(CurrentPath != null){			
				foreach(Node n in CurrentPath){
					if(n != null)
					Gizmos.DrawWireSphere( GetPos(n.x, n.y)+Vector3.up, 0.5f);
				}
			
			}
		}
	}
	///////////////////////
	//
	// Find a path from (startx, starty) to (goalx, goaly)
	//
	public bool FindPath(int startx, int starty, int goalx, int goaly)
	{
		m_visit = new bool[width,height];
		m_open = new List<Node>[width + height];
		for(int i = 0; i < width + height; i++)
			m_open[i] = new List<Node>();
		
		CurrentPath = new List<Node>();
		MDebug.Log("Find path :"+startx+"|"+starty+"  -> "+goalx+"|"+goaly);
		if(TestWidthBounds(startx) == false)
			return false;
		if(TestHeightBounds(starty) == false)
			return false;
		if(TestWidthBounds(goalx) == false)
			return false;
		if(TestHeightBounds(goaly) == false)
			return false;
		
		
		// we actually search from goal to start
		m_startx = startx;
		m_starty = starty;
		
		// clear the visit lookup
	
		
		
		// initialize the best index to the max value (no best yet)
		m_best = m_width + m_height;
		
		// insert the node into the OPEN list (if it's legal)
		Insert(null, goalx, goaly);
		
		// pull the best node and start searching
		Node n = GetBest();
		while(n != null)
		{
			if(n.x == startx && n.y == starty)
				break;
			
			// visit the neighbors
			bool top = Visit(n, n.x, 		n.y + 1);
			bool right = Visit(n, n.x + 1,	n.y);
			bool bottom = 	Visit(n, n.x,		n.y - 1);
			bool left = Visit(n, n.x - 1,	n.y);
				
			MDebug.Log( top+"  "+right+"  "+bottom+" "+left);
			
			if(top && right){	
				MDebug.Log("Check Diagonal");
				Visit(n, n.x + 1,	n.y + 1);
			}
			
			if(right && bottom){
				MDebug.Log("Check Diagonal");
				Visit(n, n.x + 1,	n.y - 1);
			}
			
			if(bottom && left){
				MDebug.Log("Check Diagonal");
				Visit(n, n.x - 1,	n.y - 1);
			}
		
			if(left && top){
				MDebug.Log("Check Diagonal");
				Visit(n, n.x - 1,	n.y + 1);
			}
			
			n = GetBest();
		}
		
		if(n == null)
		{
			Debug.LogError("No path found");
			return false;	
		}
		
		int nodes = 0;
	//	MDebug.Log("The following path was found:");
		while(n != null)
		{
			string coord = string.Format("{0}, {1}", n.x, n.y);
			MDebug.Log(coord);
			CurrentPath.Add(n);
			n = n.parent;
			nodes++;
		}
		//MDebug.Log(nodes);
		return true;
	}
	
	List<Node> CurrentPath;
	
	///////////////////////
	//
	// Visit a neighboring node and insert it into the OPEN list if it is valid
	//	
	bool Visit(Node parent, int x, int y)
	{
		if(TestWidthBounds(x) && TestHeightBounds(y))
		{
			if(m_visit[x,y])
				return false;
			
			if(m_collisiongrid[x,y])
				return false;
			
			m_visit[x,y] = true;
			
			Insert(parent, x, y);
			return true;
		}	
		return false;
	}
	
	///////////////////////
	//
	// Helper function to sort the bins of our OPEN list
	//	
	private static int CompareNodes(Node x, Node y)
	{
		if(x.cost == y.cost)
			return 0;
		if(x.cost < y.cost)
			return 1;
		else
			return -1;
	}
	
	///////////////////////
	//
	// Insert a node into the OPEN List (a hash table)
	//	
	void Insert(Node parent, int x, int y)
	{
		// the heuristic for the search is the distance to the start point
		float distance = Mathf.Sqrt((x-m_startx)*(x-m_startx) + (y-m_starty)*(y-m_starty));
		
		// also our hash function
		int index = (int)distance;
		
		m_open[index].Add(new Node(x, y, distance, parent));
		m_open[index].Sort(CompareNodes);
		
		// keep track of the best index
		if(index < m_best)
			m_best = index;
	}
	
	///////////////////////
	//
	// Extract the best node (within a bucket the items are sorted with the best node at the end of the list)
	//	
	Node GetBest()
	{
		if(m_best < (m_width+m_height))
		{
			Node best = m_open[m_best][m_open[m_best].Count-1];
			m_open[m_best].RemoveAt(m_open[m_best].Count-1);
			
			// after removing the best node, if the bucket is empty
			// we need to find the new best bucket
			if(m_open[m_best].Count == 0)
			{
				// find the next best bucket
				while(m_best < (m_width+m_height))
				{
					if(m_open[m_best].Count > 0)
						break;
					m_best++;
				}
			}
			
			return best;
		}
		
		return null;
	}
	
	///////////////////////
	//
	// Helper functions to ensure the coordinates are valid
	//	
	bool TestWidthBounds(int q)
	{

		if(q >= 0 && q < m_width)
			return true;
		else
			return false;	
	}
	
	///////////////////////
	//
	// Helper functions to ensure the coordinates are valid
	//
	bool TestHeightBounds(int q)
	{
		if(q >= 0 && q < m_height)
			return true;
		else
			return false;	
	}
}
