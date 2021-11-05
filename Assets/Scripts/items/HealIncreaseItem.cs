//------------------------------------------------------------------------------
//
// File Name:	HealIncreaseItem.cs
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

public class HealIncreaseItem : ItemBase
{
    public float healIncrease = 1f;
    protected override void Init()
    {
        InitStats(ItemType.HealIncrease, healIncrease);
    }

    protected override void PickupItem()
    {
        
    }
}
