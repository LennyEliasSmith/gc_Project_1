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
    public float rTimer;

    public Vector3 direction;
    public bool playerLOS;

    public Animator animator;

    public AudioSource gunSound;

    private GameObject player;
    private Transform playerPos;
    private Health playerHealth;
    private GameObject hitObject;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckLOS();

        if(playerLOS && !playerHealth.isDead)
        {
            rTimer = rTimer + Time.deltaTime;

            if(rTimer >= reactionTime)
            {
                if (Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;
                    EnemyShoot();
                }
            }
        } else if (!playerLOS) {
            rTimer = 0;
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
            hitObject = hit.transform.gameObject;
            gunSound.Play();
            animator.SetTrigger("Shoot");

            if (hitObject.CompareTag("Player"))
            {
                Health playerHP = hitObject.GetComponent<Health>();
                playerHP.TakeDamage(damage);
            }
        }

    }
}
