﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{

    public Health hp;
    public BoxCollider[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<Health>();
        colliders = GetComponentsInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp.currentHP <= 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                Destroy(colliders[i]);
            }
        }
    }

}
