using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingVale : Collectable
{
    // Start is called before the first frame update
    void Start()
    {
        value = 10;
    }

    protected override void RestoreValue()
    {
        Player.Instance.health += value;
    }

    // Update is called once per frame
    void Update()
    {
        AnimateObject();
    }
}
