//------------------------------------------------------------------------------
//
// File Name:	CurrencyText.cs
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
using TMPro;

public class CurrencyText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
        GameManager.OnMoneyChanged.AddListener(UpdateText);
    }

    // Update text
    private void UpdateText()
    {
        GetComponent<TextMeshProUGUI>().text = "Currency: " + GameManager.Money;
    }
}
