using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{

    public Text ammoCount;
    public Text killCount;
    public Pistol player;
    public string ammoString;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInChildren<Pistol>();
    }

    // Update is called once per frame
    void Update()
    {
        // ammoString = player.ammo.ToString() + " / " + player.maxAmmo.ToString();
        ammoCount.text = player.ammo.ToString() + " / " + player.maxAmmo.ToString();
        // ammoCount.text = ammoString;

        killCount.text = player.killCount.ToString();
    }
}
