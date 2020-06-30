using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pistol : MonoBehaviour
{

    public float fireRate;
    public float nextTimeToFire;
    public float reloadTime;

    public float killCount;
    public float shotsFired;
    public float accuracy;
    
    public bool isReloading;
    public bool isShooting;

    private Camera playerCam;
    private GameObject hitObject;

    public AudioSource gunAudio;
    public AudioClip gunShoot;
    public AudioClip gunReload;

    public ParticleSystem muzzle;
    public WeaponAmmo ammo;

    public Animator animator;


    // Start is called before the first frame update 
    void Start()
    {
        playerCam = GetComponentInParent<Camera>();
        ammo = GetComponent<WeaponAmmo>();

        AudioSource[] sources = GetComponentsInChildren<AudioSource>();

        gunAudio = sources[0];
        gunShoot = sources[0].clip;
        gunReload = sources[1].clip;

        muzzle = GetComponentInChildren<ParticleSystem>();

        isReloading = false;

    }

    // Update is called once per frame 
    void Update()
    {

        if (ammo.currentAmmo > 0 && !isReloading && !isShooting && Time.time >= nextTimeToFire)
        {

            if (Input.GetButtonDown("Fire1"))
            {

                nextTimeToFire = Time.time + 1f / fireRate;

                Shoot();

            }
        }

        if (Input.GetButtonDown("Reload") && !isReloading && ammo.currentAmmo != ammo.maxAmmo)
        {

            StartCoroutine(Reload());

        }



    }

    void Shoot()
    {

        isShooting = true;

        Debug.Log("PewPew");

        gunAudio.PlayOneShot(gunShoot);
        animator.SetTrigger("Shoot");
        muzzle.Play();

        shotsFired++;

        Vector3 direction = playerCam.transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, direction, out hit))
        {
            hitObject = hit.transform.gameObject;

            Debug.Log(hitObject);

            Debug.DrawLine(playerCam.transform.position, hit.point, Color.green, 2);

            if (hitObject.CompareTag("Enemy"))
            {
                killCount++;
                TargetDummy targetHP = hitObject.GetComponent<TargetDummy>();
                targetHP.TakeDamage();
            }
        }

        ammo.currentAmmo--;

        isShooting = false;

        animator.SetTrigger("Shoot");

        accuracy = killCount / shotsFired;
    }

    IEnumerator Reload()
    {

        Debug.Log("Reloading...");

        animator.SetTrigger("Reload");

        gunAudio.PlayOneShot(gunReload);

        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        ammo.Reload();

        Debug.Log("Finished reload");

        isReloading = false;
    }
}
