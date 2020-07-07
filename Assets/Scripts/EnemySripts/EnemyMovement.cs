using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header ("Stats")]
    public float speed;
    public float range;
    public float stoppingDistance;
    public float retreatDistance;

    [Header ("References")]
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < range)
        {
            transform.LookAt(player);

            if(distance > stoppingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            } else if(distance < retreatDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }
        }
    }
}
