using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int selectedWeapon = 0;
    private float prevInput = 0, newInput;
    private string mwheelString = "Mouse ScrollWheel";

    public Pistol pistol;
    public Shotgun shotgun;
    public AssaultRifle rifle;

    public bool isReloading;

    // Start is called before the first frame update
    void Start()
    {
        pistol = GetComponentInChildren<Pistol>();
        shotgun = GetComponentInChildren<Shotgun>();
        rifle = GetComponentInChildren<AssaultRifle>();

        ReloadStatus();
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        ReloadStatus();

        newInput = Input.GetAxisRaw(mwheelString);

        if(prevInput == 0)
        {
            if (Input.GetAxisRaw(mwheelString) > 0f && !isReloading)
            {
                if (selectedWeapon >= transform.childCount - 1)
                    selectedWeapon = 0;
                else
                    selectedWeapon++;
            }
            if(Input.GetAxisRaw(mwheelString) < 0f && !isReloading)
            {
                if (selectedWeapon <= 0)
                    selectedWeapon = transform.childCount - 1;
                else
                    selectedWeapon--;
            }

            SelectWeapon();
        }

        prevInput = newInput;
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    void ReloadStatus()
    {
        if (pistol.isReloading || shotgun.isReloading || rifle.isReloading)
            isReloading = true;
        else
            isReloading = false;
    }
}
