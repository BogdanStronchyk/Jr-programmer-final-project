using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{

    private void Awake()
    {
        GunType = "Shotgun";
        fireRate = 0.5f;
        maxAmmo = 2;
        currentAmmo = 2;
        damage = 20;
        reloadTime = 4f;
    }

    public override void Fire()
    {
        if (isShooting)
        {
            Shot();
            isShooting = false;
            StartCoroutine(ShotDelay());
        }
    }


}
