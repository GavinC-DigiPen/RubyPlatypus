//------------------------------------------------------------------------------
//
// File Name:	ItemBase.cs
// Author(s):	Nathan Stern (nathan.stern@digipen.edu)
// Project:	    RubyPlatypus
// Course:	    WANIC VGP2
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    private ItemType type;
    public ItemType Type
    {
        get => GetItemType();
    }

    private float value;
    public float Value
    {
        get => GetValue();
    }

    private ItemType GetItemType()
    {
        if (type == ItemType.None)
            Debug.LogError("No ItemType or value found in item: Add a call to InitStats in the Start function.");
        return type;
    }

    private float GetValue()
    {
        if (type == ItemType.None)
            Debug.LogError("No ItemType or value found in item: Add a call to InitStats in the Start function.");
        return value;
    }


    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        if(Vector3.Distance(transform.position,playerPos) <= 0.5f)
        {
            GameManager.AddItem(this);
            PickupItem();
            Destroy(gameObject);
        }
    }

    protected void InitStats(ItemType initType, float initValue)
    {
        value = initValue;
        type = initType;
    }

    protected abstract void Init();
    protected abstract void PickupItem();
}

public enum ItemType
{
    None,
    MaxHealthIncrease,
    HealIncrease,
    DamageIncrease,
    AttackIncrease,
    Shield
}
