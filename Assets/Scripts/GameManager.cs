//------------------------------------------------------------------------------
//
// File Name:	GameManager.cs
// Author(s):	Gavin Cooper (gavin.cooper@digipen.edu)
// Project:	    RubyPlatypus
// Course:	    WANIC VGP2
//
// Copyright � 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static int loop = 0;

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
