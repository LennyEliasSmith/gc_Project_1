using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Text ammoCount;
    public Text killCount;
    public Text hpamount;

    public Image crosshair;
    public Image hitmarker;
    public Image healthbar;

    public WeaponAmmo ammo;
    public WeaponStats statsPSTL;
    public WeaponStats statsSHTG;
    public WeaponStats statsAR;
    public Health health;
    public float kills;

    public string ammoString;

    // Start is called before the first frame update
    void Start()
    {
        ammo = GetComponentInChildren<WeaponAmmo>();
        health = GetComponentInParent<Health>();
        hitmarker.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        HealthBar();
        SetAmmo();
        SetKills();
    }

    public void HealthBar()
    {
        // hpamount.text = health.currentHP.ToString();
        healthbar.fillAmount = health.currentHP / health.maxHP;
        // P1_HpText.text = P1_Hp.health + " / " + P1_Hp.maxhealth;
    }

    void SetAmmo()
    {
        ammo = GetComponentInChildren<WeaponAmmo>();
        // ammoString = player.ammo.ToString() + " / " + player.maxAmmo.ToString();
        ammoCount.text = ammo.currentAmmo.ToString() + " / " + ammo.maxAmmo.ToString();
        // ammoCount.text = ammoString;
    }

    void SetKills()
    {
        kills = statsPSTL.killCount + statsSHTG.killCount + statsAR.killCount;
        killCount.text = kills.ToString();
    }
}

