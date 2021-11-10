//------------------------------------------------------------------------------
//
// File Name:	GameManager.cs
// Author(s):	Nathan Stern (nathan.stern#digipen.edu)
//              Gavin Cooper (gavin.cooper@digipen.edu)
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

    public static UnityEvent OnMoneyChanged = new UnityEvent();

    public static List<ItemBase> items = new List<ItemBase>();

    #region ItemModifiers
    private static float healthModifier = 0;
    public static float HealthModifier
    {
        get => healthModifier;
    }

    private static float damageModifier = 1;
    public static float DamageModifier
    {
        get => damageModifier;
    }

    private static float healModifier = 1;
    public static float HealModifier
    {
        get => healModifier;
    }

    public static void AddItem(ItemBase item)
    {
        items.Add(item);
        CalculateStats();
        switch (item.Type)
        {  
            case ItemType.DamageIncrease:
                FindObjectOfType<ItemText>().AddText(damageModifier.ToString() + "x");
                break;
            case ItemType.HealIncrease:
                FindObjectOfType<ItemText>().AddText(healModifier.ToString() + "x");
                break;
            default:
                break;
        }
    }

    public static void CalculateStats()
    {
        healthModifier = 0;
        healModifier = 1;
        damageModifier = 1; 
        foreach (ItemBase i in items)
        {
            switch(i.Type)
            {
                case ItemType.MaxHealthIncrease:
                    healthModifier += i.Value;
                    break;
                case ItemType.DamageIncrease:
                    damageModifier += i.Value;
                    break;
                case ItemType.HealIncrease:
                    healModifier += i.Value;
                    break;
                default:
                    break;
            }
        }
    }
    #endregion

    private static int money = 0;

    public static int Money
    {
        get => money;
    }

    public static bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            OnMoneyChanged.Invoke();
            return true;
        }
        return false;
    }

    public static void AddMoney(int amount)
    {
        money += amount;
        OnMoneyChanged.Invoke();
    }

    public static void ResetStats()
    {
        money = 0;
        Loop = 0;
        healthModifier = 0;
        healModifier = 1;
        damageModifier = 1;
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
