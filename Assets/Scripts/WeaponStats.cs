using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public float killCount;
    public float shotsFired;
    public float shotsHit;
    public float accuracy;

    void Start()
    {
        
    }

    void Update()
    {
        accuracy = shotsHit / shotsFired;

        if (accuracy > 1)
            accuracy = 1;
    }
}
