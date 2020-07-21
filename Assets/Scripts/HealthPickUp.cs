using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public float healAmount;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.LookAt(player);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Health hp = other.GetComponent<Health>();
            if(hp.currentHP < hp.maxHP)
            {
                hp.currentHP = hp.currentHP + healAmount;
                Destroy(this.gameObject);
            }
            
        }
    }
}
