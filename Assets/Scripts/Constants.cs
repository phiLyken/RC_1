using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Constants : MonoBehaviour {
    
    
    /// <summary>
    /// How close a player unit must come before the an enemy AI is activated and added to the turn system
    /// </summary>
    public static float UNIT_ACTIVATION_RANGE = 10;

    /// <summary>
    /// For each point of will, intensity received is decreased by _x_ points
    /// </summary>
     // public static float WILL_TO_INT = 0.0f;

    /// <summary>
    /// Foreach point of damage a unit gets, it will receive some _x_ points of int
    /// </summary>
    public static float RCV_DMG_TO_INT = 1f;

    /// <summary>
    /// Amount of will restored per int when using rest ability
    /// </summary>
    public static float INT_TO_HEAL = 1f;

    /// <summary>
    /// Amount of Extra Damage Dealt per Point of Intensity
    /// </summary>
    public static int INT_TO_DMG = 1;

    /// <summary>
    /// How much the tile can crumble before it is removed
    /// </summary>
    public static int CrumbleHeightThreshold = 4;


    /// <summary>
    /// How many levels a tile can crumble on a crumble turn if it is crumbling
    /// </summary>
    public static MyMath.R_Range TileCrumbleRangeOnCrumble = new MyMath.R_Range(1, 3);

    /// <summary>
    /// Cost of moving diagonal
    /// </summary>
    public static float MovementCost_Diagonal = 1.35f;

    /// <summary>
    /// Cost of moving straight
    /// </summary>
    public static float MovementCost_Straight = 1;

    /// <summary>
    /// Movement cost of going up or down by 1
    /// </summary>
    public static float MovementCost_Height = 0;

    /// <summary>
    /// The range in tiles that the world crumble has (counted from the last not crumbling row)
    /// </summary>
    public static int CrumbleRange = 10;




    public static List<string> names = new List<string>(new string[]
    {
         "SHEPPHARD", "BRENDA", "DAVIS", "CLARK", "LEWIS", "DUDE", "JACKSON", "SCHMIDT",
        "ZERO", "DIXON", "HUNT","RAY", "MCCOY","SCHNEIDER","ORTEGA","VEGA","MCGEE","HAMMOND","YATES","CLAYTON","NGUYEN", "VALENTINE","NIXON",
        "MACIAS",    "LYNCH",    "PITTS",    "HARVEY",    "GARDNER",    "PHELPS",    "PARKS",    "WOLFE",    "SANTANA",    "ZUNIGA",    "DECKER",    "MENDEZ",    "FARRELL",
    "CHUNG",    "CALDWELL",    "SHAFFER",    "GOLDEN",    "HUANG",    "BYRD",    "RASMUSSEN",    "SHARP",    "MASSEY",    "HINTON",    "CLAY",
    "RIDDLE",    "HOWELL",    "GARRET",    "CLAYTON",    "PECK",    "MULLEN",    "LAWRENCE",    "MALONE",    "LAWSON",    "ARNOLD",    "DELEON",
    "FERGUSON",    "PERRY","STEPHENS" }
    );


}
