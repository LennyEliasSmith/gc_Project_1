using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPistol : MonoBehaviour
{
    public float damage;
    public float fireRate;
    public float nextTimeToFire;
    public float gunSpread;
    public float reactionTime;

    public Vector3 direction;
    public bool playerLOS;

    public Animator animator;

    public AudioSource gunSound;

    private Transform player;
    private GameObject hitObject;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckLOS();

        if(playerLOS)
        {
            if (Time.time >= nextTimeToFire)
            { 
                nextTimeToFire = Time.time + 1f / fireRate;
                EnemyShoot();
            }
        }
    }

    void CheckLOS()
    {
        direction = transform.forward;
        RaycastHit hit;

        Physics.Raycast(transform.position, direction, out hit);
        hitObject = hit.transform.gameObject;

        if (hitObject.CompareTag("Player"))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red, 2);
            playerLOS = true;
        }
        else
        {
            Debug.DrawLine(transform.position, hit.point, Color.green, 2);
            playerLOS = false;
        }
    }

    void EnemyShoot()
    {
        direction = transform.forward;
        RaycastHit hit;

        Vector3 spread = new Vector3();

        spread += transform.up * Random.Range(-gunSpread, gunSpread);
        spread += transform.right * Random.Range(-gunSpread, gunSpread);
        direction += spread.normalized * Random.Range(0f, gunSpread);

        if (Physics.Raycast(transform.position, direction, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.blue, 2);
            gunSound.Play();
            animator.SetTrigger("Shoot");
        }

    }
}
