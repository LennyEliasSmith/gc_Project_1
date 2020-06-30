using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shotgun : MonoBehaviour
{

    public float fireRate;
    public float nextTimeToFire;
    public float maxAmmo;
    public float ammo;
    public float reloadTime;
    public int shotgunShots;

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

    // public Animator animator;


    // Start is called before the first frame update 
    void Start()
    {
        playerCam = GetComponentInParent<Camera>();

        AudioSource[] sources = GetComponentsInChildren<AudioSource>();

        gunAudio = sources[0];
        gunShoot = sources[0].clip;
        gunReload = sources[1].clip;

        muzzle = GetComponentInChildren<ParticleSystem>();

        isReloading = false;

        ammo = maxAmmo;

    }

    // Update is called once per frame 
    void Update()
    {

        if (ammo > 0 && !isReloading && !isShooting && Time.time >= nextTimeToFire)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("shotgun should be shooting");
                // nextTimeToFire = Time.time + 1f / fireRate;
                isShooting = true;
            }
        }

        if (isShooting)
        {
            gunAudio.PlayOneShot(gunShoot);

            for (int i = 0; i < shotgunShots; i++)
            {
                Debug.Log("shotgun should be shooting");
                ShotgunShot();
            }

            ammo--;
            isShooting = false;
        }

        if (Input.GetButtonDown("Reload") && !isReloading && ammo != maxAmmo)
        {

            StartCoroutine(Reload());

        }



    }

    void ShotgunShot()
    {

        Debug.Log("KaPOW");

        // gunAudio.PlayOneShot(gunShoot);
        // animator.SetTrigger("Shoot");
        // muzzle.Play();

        Vector3 direction = playerCam.transform.forward;
        Vector3 spread = new Vector3();

        spread += playerCam.transform.up * Random.Range(-1f, 1f);
        spread += playerCam.transform.right * Random.Range(-1f, 1f);
        direction += spread.normalized * Random.Range(0f, 0.1f);

        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, direction, out hit))
        {

            Debug.DrawLine(playerCam.transform.position, hit.point, Color.green, 2);

            hitObject = hit.transform.gameObject;

            Debug.Log(hitObject);

            if (hitObject.CompareTag("Enemy"))
            {
                killCount++;
                TargetDummy targetHP = hitObject.GetComponent<TargetDummy>();
                targetHP.TakeDamage();
            }
        }
        else
        {
            Debug.DrawRay(playerCam.transform.position, direction, Color.red, 2);
        }

        // animator.SetTrigger("Shoot");

        accuracy = killCount / shotsFired;
    }

    IEnumerator Reload()
    {

        Debug.Log("Reloading...");

        // animator.SetTrigger("Reload");

        gunAudio.PlayOneShot(gunReload);

        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        ammo = maxAmmo;

        Debug.Log("Finished reload");

        isReloading = false;
    }
}
