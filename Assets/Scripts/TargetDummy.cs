using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{

    public Health hp;
    public BoxCollider[] colliders;
    public EnemyPistol weapon;

    public GameObject ammoDrop;
    public GameObject healthDrop;
    public bool canDrop;

    private Transform player;

    private int[] dropChance = new int[] { 0, 1, 2, 3, 4};
    public int i;

    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<Health>();
        colliders = GetComponentsInChildren<BoxCollider>();
        weapon = GetComponent<EnemyPistol>();
        player = GameObject.FindGameObjectWithTag("playerTarget").transform;

        canDrop = true;
        DropChance();
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

    void DropChance()
    {
        if (canDrop)
        {
            i = Random.Range(0, dropChance.Length);
            i = Mathf.RoundToInt(i);
        }

    }

    public void DropAmmo()
    {
        if (dropChance[i] == dropChance[3])
        {
            GameObject ammoPickUp = Instantiate(ammoDrop, transform.position, Quaternion.identity);
        }
        else if (dropChance[i] == dropChance[4])
        {
            GameObject healthPickUp = Instantiate(healthDrop, transform.position, Quaternion.identity);
        }

    }

}
