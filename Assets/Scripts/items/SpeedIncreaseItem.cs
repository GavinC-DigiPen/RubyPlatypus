using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncreaseItem : ItemBase
{
    public float speedIncrease = 0.05f;
    protected override void Init()
    {
        InitStats(ItemType.SpeedIncrease, speedIncrease);
    }

    protected override void PickupItem()
    {
        FindObjectOfType<Movement>().UpdateSpeed();
    }
}
