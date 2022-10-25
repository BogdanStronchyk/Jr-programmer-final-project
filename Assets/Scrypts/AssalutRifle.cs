using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssalutRifle : Gun
{

    private void Awake()
    {
        GunType = "Assalut Rifle";
        fireRate = 0.15f;
        maxAmmo = 30;
        currentAmmo = 30;
        damage = 3;
        reloadTime = 3f;
        isAutomatic = true;
    }

    public override void Fire()
    {
        InvokeRepeating("Shot", 0f, fireRate);

        if (currentAmmo == 0)
        {
            HoldFire();
        }
    }

}
