using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Text ammoCount;
    public Text killCount;

    public Image crosshair;
    public Image hitmarker;

    public WeaponAmmo ammo;
    public WeaponStats statsPSTL;
    public WeaponStats statsSHTG;
    public WeaponStats statsAR;
    public float kills;

    public string ammoString;

    // Start is called before the first frame update
    void Start()
    {
        ammo = GetComponentInChildren<WeaponAmmo>();
        hitmarker.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        ammo = GetComponentInChildren<WeaponAmmo>();

        // ammoString = player.ammo.ToString() + " / " + player.maxAmmo.ToString();
        ammoCount.text = ammo.currentAmmo.ToString() + " / " + ammo.maxAmmo.ToString();
        // ammoCount.text = ammoString;

        kills = statsPSTL.killCount + statsSHTG.killCount + statsAR.killCount;

        killCount.text = kills.ToString();
    }
}
