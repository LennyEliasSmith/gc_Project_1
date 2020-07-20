﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Text ammoCount;
    public Text killCount;
    public Text hpamount;
    public Text levelTimer;
    public Text completionText1;
    public Text completionText2;
    public Text completionText3;
    public Text completionText4;

    public RawImage crosshair;
    public Image hitmarker;
    public Image healthbar;

    public WeaponAmmo ammo;
    public WeaponStats statsPSTL;
    public WeaponStats statsSHTG;
    public WeaponStats statsAR;
    public Health health;
    public float kills;
    public float accuracyTotal;

    public string ammoString;

    private GameObject weaponHolder;
    private GameObject managerObject;
    private GameManager managerScript;

    // Start is called before the first frame update
    void Start()
    {
        ammo = GetComponentInChildren<WeaponAmmo>();
        health = GetComponentInParent<Health>();
        hitmarker.enabled = false;

        weaponHolder = GameObject.Find("WeaponHolder");
        managerObject = GameObject.FindGameObjectWithTag("GameManager");
        managerScript = managerObject.GetComponent<GameManager>();

        completionText1.enabled = false;
        completionText2.enabled = false;
        completionText3.enabled = false;
        completionText4.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!health.isDead)
        {
            SetAmmo();
            SetKills();
            LevelTimer();
        }

        HealthBar();
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
        kills = managerScript.killCount;
        killCount.text = kills.ToString();
    }

    void LevelTimer()
    {
        levelTimer.text = managerScript.gameTimer.ToString();
    }

    public void GameEndOverlay()
    {
        if (managerScript.gameEnd && health.isDead)
            completionText1.text = "You died, press F1 to restart";

        completionText2.text = completionText2.text + " " + accuracyTotal.ToString() + "%";
        completionText3.text = completionText3.text + " " + kills.ToString();
        completionText4.text = completionText4.text + " " + managerScript.gameTimer.ToString();

        completionText1.enabled = true;
        completionText2.enabled = true;
        completionText3.enabled = true;
        completionText4.enabled = true;

        levelTimer.enabled = false;

    }

    public void CalculateAcc()
    {
        accuracyTotal = (statsAR.accuracy + statsPSTL.accuracy + statsSHTG.accuracy) / 3;
        accuracyTotal = accuracyTotal * 100;
        accuracyTotal = Mathf.RoundToInt(accuracyTotal);
    }
}

