//------------------------------------------------------------------------------
//
// File Name:	DamageItem.cs
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

public class DamageItem : ItemBase
{
    public float damageIncrease = 1f;
    protected override void Init()
    {
        InitStats(ItemType.DamageIncrease, damageIncrease);
    }

    protected override void PickupItem()
    {
        FindObjectOfType<Sword>().UpdateDamage();
    }
}
