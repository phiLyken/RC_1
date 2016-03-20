using UnityEngine;
using System.Collections.Generic;

///////////////////////
//
// A simple 2D implementation of the best first search algoirithm (robotics motion planning)
//
public class TilePathFinder 
{
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
	
	private bool[,] m_visit;			// track the grid points that have already been visited in the search
	private int m_width;				// the width of the grid
	private int m_height;				// the height of the grid
	private int m_startx;				// the start (x, y) coordinate
	private int m_starty;
	private List<Node>[] m_open;		// the OPEN list (these are the promising nodes that we've visited)
	private int m_best;                 // the index in our OPEN list of the best (most promising) node (i.e. a priority queue)
    private TileManager manager;

	///////////////////////
	//
	// Constructor - allocate space for the width x height grid 
	//	
	public TilePathFinder(TileManager _manager, int width, int height)
	{
        if(_manager == null)
        {
            Debug.LogWarning("no tile manager");
            return;
        } else
        {
         //   Debug.Log(_manager.name);
        }
        //	m_collisiongrid = new bool[width,height];
        manager = _manager;
		m_visit = new bool[width,height];
		m_width = width;
		m_height = height;
		
		m_open = new List<Node>[width + height];

		for(int i = 0; i < width + height; i++)
			m_open[i] = new List<Node>();

       // Debug.Log("new path finder " + width + " " + height);
	}
	
	
	///////////////////////
	//
	// Add obstacles to the grid
	//	
	/*	public void SetObstacle(Tile t, bool blocked)
	{
		if(TestWidthBounds(t.TilePos.x) && TestHeightBounds(t.TilePos.y))
			m_collisiongrid[t.TilePos.x,t.TilePos.y] = blocked;
	}
	
	*/
	
	///////////////////////
	//
	// Find a path from (startx, starty) to (goalx, goaly)
	//
	public List<Tile> FindPath(Tile startTile, Tile endTile)
	{
		List<Tile> Path = new List<Tile>();
     
		if(!endTile.IsFree) return Path;
		int startx = (int) startTile.TilePos.x;
		int startz = (int) startTile.TilePos.z; 
		
		int endx =  (int)  endTile.TilePos.x;
		int endz =  (int)  endTile.TilePos.z;
      
		Debug.DrawLine(manager.Tiles[startx, startz].transform.position+ Vector3.up,
                      manager.Tiles[endx, endz].transform.position + Vector3.up, Color.cyan, 0.2f);
			
		
		if(TestWidthBounds(startx) == false)
			return Path;
		if(TestHeightBounds(startz) == false)
			return Path;
		if(TestWidthBounds(endx) == false)
			return Path;
		if(TestHeightBounds(endz) == false)
			return Path;
		
		// we actually search from goal to start
		m_startx = startx;
		m_starty = startz;
		
		// clear the visit lookup
		System.Array.Clear(m_visit, 0, m_width*m_height);
		
		// initialize the best index to the max value (no best yet)
		m_best = m_width + m_height;
		
		// insert the node e the OPEN list (if it's legal)
		Insert(null, endx, endz);
		
		// pull the best node and start searching
		Node n = GetBest();
		while(n != null)
		{
			if(n.x == startx && n.y == startz) {
             
				break;
            }

            // visit the neighbors
            bool top = Visit(n, n.x, 		n.y + 1);
			bool right = Visit(n, n.x + 1,	n.y);
			bool bottom = 	Visit(n, n.x,		n.y - 1);
			bool left = Visit(n, n.x - 1,	n.y);			

			
			if(top && right){	
				//Debug.Log("Check Diagonal");
				Visit(n, n.x + 1,	n.y + 1);
			}
			
			if(right && bottom){
			//	Debug.Log("Check Diagonal");
				Visit(n, n.x + 1,	n.y - 1);
			}
			
			if(bottom && left){
			//	Debug.Log("Check Diagonal");
				Visit(n, n.x - 1,	n.y - 1);
			}
			
			if(left && top){
			//	Debug.Log("Check Diagonal");
				Visit(n, n.x - 1,	n.y + 1);
			}
			
			
			n = GetBest();  
		}
		
		if(n == null)
		{

			return Path;	
		}
			
		
		while(n != null)
		{
			Path.Add( manager.Tiles[n.x, n.y]);
			n = n.parent;	
		}

		return Path;
	}
	
	
	///////////////////////
	//
	// Visit a neighboring nTileode and insert it into the OPEN list if it is valid
	//	
	bool Visit(Node parent, int x, int y)
	{
		if(TestWidthBounds(x) && TestHeightBounds(y))
		{
            //if it is already visted, skip
			if(m_visit[x,y])
				return false;

            //if the height difference is too high, we can not visit
            int m_height = manager.Tiles[parent.x, parent.y].currentHeightStep;
            int other_height = manager.Tiles[x, y].currentHeightStep;
            int diff = m_height - other_height;
        
            if (diff > 1)
            {
               // Debug.DrawLine(manager.Tiles[parent.x, parent.y].transform.position, manager.Tiles[x, y].transform.position, Color.yellow, 3f);
                return false;
            }

            //if tile is blocked we can not visit - unless it is the start tile
            if (!(x == m_startx && y == m_starty)){
                if (!manager.Tiles[x, y].IsFree)
                    return false;
            }

			//set tile to visted
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
