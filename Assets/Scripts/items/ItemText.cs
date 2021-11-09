using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private bool visible;
    private float dissapearTime = 2f;
    private float time = 0f;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.enabled = false;
    }

    public void ChangeText(string s)
    {
        text.text = s;
        EnableText();
    }

    public void AddText(string s)
    {
        text.text += s;
        EnableText();
    }

    private void EnableText()
    {
        text.enabled = true;
        visible = true;
        time = 0f;
    }

    private void Update()
    {
        if(visible)
        {
            time += Time.deltaTime;
            if(time >= dissapearTime)
            {
                visible = false;
                text.enabled = false;
            }
        }
    }
}
