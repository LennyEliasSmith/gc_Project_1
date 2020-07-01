using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Text ammoCount;
    public Text killCount;

    public WeaponAmmo ammo;
    public WeaponStats stats1;
    public WeaponStats stats2;
    public float kills;

    public string ammoString;

    // Start is called before the first frame update
    void Start()
    {
        ammo = GetComponentInChildren<WeaponAmmo>();

    }

    // Update is called once per frame
    void Update()
    {
        ammo = GetComponentInChildren<WeaponAmmo>();

        // ammoString = player.ammo.ToString() + " / " + player.maxAmmo.ToString();
        ammoCount.text = ammo.currentAmmo.ToString() + " / " + ammo.maxAmmo.ToString();
        // ammoCount.text = ammoString;

        kills = stats1.killCount + stats2.killCount;

        killCount.text = kills.ToString();
    }
}
