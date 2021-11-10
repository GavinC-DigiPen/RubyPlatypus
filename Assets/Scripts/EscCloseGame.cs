//------------------------------------------------------------------------------
//
// File Name:	EscCloseGame.cs
// Author(s):	Gavin Cooper (gavin.cooper@digipen.edu)
//              Jeremy Kings (j.kings@digipen.edu)
// Project:	    RubyPlatypus
// Course:	    WANIC VGP2
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscCloseGame : MonoBehaviour
{
    [Tooltip("The key to exit the game")]
    public KeyCode exitKey = KeyCode.Escape;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(exitKey))
        {
            #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
