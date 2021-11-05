//------------------------------------------------------------------------------
//
// File Name:	HealthItemIncrease.cs
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

public class HealthIncreaseItem : ItemBase
{
    public float healthIncrease = 1;
    protected override void Init()
    {
        InitStats(ItemType.MaxHealthIncrease, healthIncrease);
    }

    protected override void PickupItem()
    {
        FindObjectOfType<Health>().UpdateHealth();
    }
}
