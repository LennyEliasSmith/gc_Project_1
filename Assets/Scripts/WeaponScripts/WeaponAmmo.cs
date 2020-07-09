using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{

    public float maxAmmo;
    public float currentAmmo;

    void Start()
    {
        // set ammo to maximum during startup
        if (currentAmmo <= 0)
        {
            currentAmmo = maxAmmo;
        }
    }

    void Update()
    {

        // eliminates any ammo overloads
        if (currentAmmo >= maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
    }

    // refill ammo
    public void Reload()
    {
        currentAmmo = maxAmmo;
    }
}
