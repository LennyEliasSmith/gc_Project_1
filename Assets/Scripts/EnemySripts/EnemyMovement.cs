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

    private float startingYPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startingYPos = transform.position.y;
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (LineOfSight())
        {
            if (distance < range)
            {
                transform.LookAt(player);

                if (distance > stoppingDistance)
                {
                    Vector3 newPos = new Vector3(transform.position.x, startingYPos, transform.position.z);
                    transform.position = Vector3.MoveTowards(newPos, player.position, speed * Time.deltaTime);
                }
                else if (distance < retreatDistance)
                {
                    Vector3 newPos = new Vector3(transform.position.x, startingYPos, transform.position.z);
                    transform.position = Vector3.MoveTowards(newPos, player.position, -speed * Time.deltaTime);
                }
            }
        }
    }

    bool LineOfSight()
    {
        RaycastHit hit;

        Vector3 direction = player.position - transform.position;
        Debug.DrawRay(transform.position, direction, Color.red);

        if(Physics.Raycast(transform.position, direction, out hit, range))
        {
            if(hit.transform.CompareTag("Player"))
            {
                return true; 
            }
        }
        return false;
    }

}
