//------------------------------------------------------------------------------
//
// File Name:	GameManager.cs
// Author(s):	Gavin Cooper (gavin.cooper@digipen.edu)
//              Nathan Stern (nathan.stern#digipen.edu)
// Project:	    RubyPlatypus
// Course:	    WANIC VGP2
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static int loop = 0;
    public static List<ItemBase> items = new List<ItemBase>();

    private static float healthModifier = 0;
    public static float HealthModifier
    {
        get => healthModifier;
    }

    private static float speedModifier = 1;
    public static float SpeedModifier
    {
        get => speedModifier;
    }

    public static void AddItem(ItemBase item)
    {
        items.Add(item);
        CalculateStats();
    }

    public static void CalculateStats()
    {
        ResetStats();
        foreach(ItemBase i in items)
        {
            switch(i.Type)
            {
                case ItemType.MaxHealthIncrease:
                    healthModifier += i.Value;
                    break;
                case ItemType.SpeedIncrease:
                    speedModifier += i.Value;
                    break;
                default:
                    break;
            }
        }
    }

    public static void ResetStats()
    {
        healthModifier = 0;
        speedModifier = 1;
    }

    public static int Loop
    {
        get
        {
            return loop;
        }
        set
        {
            loop = value;
        }
    }
}
