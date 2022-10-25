using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : Gun
{

    private void Awake()
    {
        GunType = "Handgun";
        fireRate = 0.5f;
        maxAmmo = 10;
        currentAmmo = 10;
        damage = 5;
        reloadTime = 1f;
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
