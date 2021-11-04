//------------------------------------------------------------------------------
//
// File Name:	GameManager.cs
// Author(s):	Gavin Cooper (gavin.cooper@digipen.edu)
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
    public static UnityEvent onItemAdded = new UnityEvent();

    private static float healthModifier;
    public static float HealthModifier
    {
        get => healthModifier;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public static void AddItem(ItemBase item)
    {
        items.Add(item);
        CalculateStats();
        onItemAdded.Invoke();
    }

    public static void CalculateStats()
    {
        foreach(ItemBase i in items)
        {
            switch(i.Type)
            {
                case ItemType.MaxHealthIncrease:
                    healthModifier += i.Value;
                    break;
                default:
                    break;
            }
        }
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
