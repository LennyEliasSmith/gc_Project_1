using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{

    public float maxMag;
    public float currentMag;
    public float maxReserveAmmo;
    public float currentReserveAmmo;

    public float ReserveAmmoMultiplier;

    void Start()
    {
        // set ammo to maximum during startup
        maxReserveAmmo = ReserveAmmoMultiplier * maxMag;
        currentMag = maxMag;
        currentReserveAmmo = maxReserveAmmo;

    }

    void Update()
    {

        // eliminates any ammo overloads
        if (currentMag >= maxMag)
        {
            currentMag = maxMag;
        }
    }

    // refill ammo
    public void Reload()
    {
        if (currentReserveAmmo > 0)
        {
            float ammoNeeded = maxMag - currentMag;
            if (ammoNeeded > currentReserveAmmo)
            {
                ammoNeeded = currentReserveAmmo;
            }
            currentReserveAmmo = currentReserveAmmo - ammoNeeded;
            currentMag = currentMag + ammoNeeded;
        }

}
}
