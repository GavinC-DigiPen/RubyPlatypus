//------------------------------------------------------------------------------
//
// File Name:	Sword.cs
// Author(s):	Nathan Stern (nathan.stern@digipen.edu)
// Project:	    RubyPlatypus
// Course:	    WANIC VGP2
//
// Copyright � 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool attacking = false;
    public int startDamage = 10;
    public int damage;

    private void Start()
    {
        damage = startDamage;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    public void UpdateDamage()
    {
        damage = startDamage + (int)GameManager.DamageModifier;
    }
}
