//------------------------------------------------------------------------------
//
// File Name:	DropItem.cs
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

public class DropItem : MonoBehaviour
{
    [Tooltip("The prefabs of the items")]
    public GameObject[] items;

    // Create item on destroy
    public void SpawnItem()
    {
        int index = Random.Range(0, items.Length);
        Instantiate(items[index], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
