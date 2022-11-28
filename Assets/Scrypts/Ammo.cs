using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Collectable
{
    private void Start()
    {
        value = 10;
    }

    protected override void RestoreValue()
    {
        Player.Instance.ammunition += value;
    }

    

    // Update is called once per frame
    void Update()
    {
        AnimateObject();
    }
}
