using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{

    public Health hp;
    public BoxCollider[] colliders;
    public EnemyPistol weapon;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<Health>();
        colliders = GetComponentsInChildren<BoxCollider>();
        weapon = GetComponent<EnemyPistol>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);

        if (hp.currentHP <= 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                Destroy(colliders[i]);
                Destroy(weapon);
            }
        }
    }

}
