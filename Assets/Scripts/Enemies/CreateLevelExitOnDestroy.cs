//------------------------------------------------------------------------------
//
// File Name:	CreateLevelExitOnDestroy.cs
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

public class CreateLevelExitOnDestroy : MonoBehaviour
{
    [Tooltip("The prefab of the level exit")]
    public GameObject levelExit;
   
    // Create level exit on destroy
    public void SpawnPortal()
    {
        Instantiate(levelExit, transform.position, Quaternion.identity);
    }
}
