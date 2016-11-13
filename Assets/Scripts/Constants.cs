﻿using UnityEngine;
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
    /// How many units a tile moves down per crumble step
    /// </summary>
    public static float CrumbleDistancePerStep = 0.15f;

    /// <summary>
    /// How many levels a tile can crumble on a crumble turn if it is crumbling
    /// </summary>
    public static MyMath.R_Range TileCrumbleRangeOnFirstCrumble = new MyMath.R_Range(1, 3);

    /// <summary>
    /// How many levels a tile can crumble on a crumble turn if it is crumbling
    /// </summary>
    public static MyMath.R_Range TileCrumbleRangeOnCrumble = new MyMath.R_Range(1, 1);

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

    /// <summary>
    /// How high is the chance that an AI will switch preferred target to a different (attackable) target 
    /// </summary>
    public static float AI_TARGET_SWITCH_WHEN_OUT_OF_ATTACK_RANGE = 0.5f;

    /// <summary>
    /// How high is the chance that an aI will switch its target if the prefferred target is not reachable in 1 move
    /// </summary>
    public static float AI_TARGET_SWITCH_TO_ATTACK_MOVE_WHEN_OUT_OF_MOVE_ATTACK_RANGE = 0.3f;

    /// <summary>
    /// How high is the chance that an enemy will switch to another far away target when the current is not reachable in 1 move
    /// </summary>
    public static float AI_TARGET_SWITCH_TO_CHASE_WHEN_OUT_OF_ATTACK_RANGE = 0.3f;

    /// <summary>
    /// when the unit is not triggered, how high is the chance that it will patrol around its start tile
    /// </summary>
    public static float AI_PATROL_CHANCE = 0.5f;

    /// <summary>
    /// How far can an AI patrol around its start tile?
    /// </summary>
    public static float AI_PATROL_DISTANCE = 2f;

    public static List<string> names = new List<string>(new string[]
    {
         "SHEPPHARD", "BRENDA", "DAVIS", "CLARK", "LEWIS", "DUDE", "JACKSON", "SCHMIDT",
        "ZERO", "DIXON", "HUNT","RAY", "MCCOY","SCHNEIDER","ORTEGA","VEGA","MCGEE","HAMMOND","YATES","CLAYTON","NGUYEN", "VALENTINE","NIXON",
        "MACIAS",    "LYNCH",    "PITTS",    "HARVEY",    "GARDNER",    "PHELPS",    "PARKS",    "WOLFE",    "SANTANA",    "ZUNIGA",    "DECKER",    "MENDEZ",    "FARRELL",
    "CHUNG",    "CALDWELL",    "SHAFFER",    "GOLDEN",    "HUANG",    "BYRD",    "RASMUSSEN",    "SHARP",    "MASSEY",    "HINTON",    "CLAY",
    "RIDDLE",    "HOWELL",    "GARRET",    "CLAYTON",    "PECK",    "MULLEN",    "LAWRENCE",    "MALONE",    "LAWSON",    "ARNOLD",    "DELEON",
    "FERGUSON",    "PERRY","STEPHENS" }
    );


    /// <summary>
    /// How much dust the player will get when looting 
    /// base amount is determined randomly upon looting and depends on the loot category
    /// player level is the number of checkpoints reached (start counts as 1)
    /// </summary>
    /// <param name="base_amount"></param>
    /// <param name="checkpoints_reached"></param>
    /// <returns></returns>
    public static int GetDustForProgress(int base_amount, int checkpoints_reached)
    {
        return base_amount * Mathf.Max(1, checkpoints_reached);
    }

    public static int GetAdrenalineRushBonus(UnitStats stats)
    {
        int adr = (int)  stats.GetStatAmount(StatType.adrenaline);

        return  (int) (adr /  2f) + 1;
    }

    public static int GetGainedAdrenaline(UnitStats stats, int rolls)
    {
        float min = Mathf.Max(0, stats.GetStatAmount(StatType.adrenaline_conversion_min));
        float max = Mathf.Max(0, stats.GetStatAmount(StatType.adrenaline_conversion_max));

        float result = 0;
        for (int i = 0; i < rolls; i++)
        {
            result += Random.Range(min, max);
        }

        //For Testing
        result += 4;
        return (int)Mathf.Round(result);
    }

    public static  int GetGainedAdrenaline(Unit unit, int rolls)
    {
        return GetGainedAdrenaline(unit.Stats, rolls);
    }


    public static int GetAttackTimeDelay(float base_delay_from_stats, float delay_from_weapon)
    {
        int time_cost = (int) (base_delay_from_stats + delay_from_weapon);
        Debug.Log(" Attack Cost Calculation :" + time_cost);
        return time_cost;
    }

    #region
    public static float CRUMBLE_WEIGHT_DEPTH_MULTIPLER = 2f;
    public static float CRUMBLE_WEIGHT_WIDTH_MODIFIER = 1f;
    public static float CRUMBLE_WEIGHT_INACESSIBLE_WEIGHT = 5;
    public static float CRUMBLE_WEIGHT_STAGE_WEIGHT = 5;
    public static float CRUMBLE_WEIGHT_DISTANCE = 5;
    #endregion
}
