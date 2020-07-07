using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shotgun : MonoBehaviour
{
    public float damage;
    public float fireRate;
    public float nextTimeToFire;
    public float reloadTime;
    public int shotgunShots;

    public bool isReloading;
    public bool isShooting;

    private Camera playerCam;
    private GameObject hitObject;
    private PlayerHUD hud;

    public AudioSource gunAudio;
    public AudioClip gunShoot;
    public AudioClip gunReload;
    public AudioClip gunShell;

    public WeaponAmmo ammo;
    public WeaponStats stats;
    public ParticleSystem muzzle;

    public Animator animator;

    public float hitTimer;
    public float hTimer;


    // Start is called before the first frame update 
    void Start()
    {
        playerCam = GetComponentInParent<Camera>();
        ammo = GetComponent<WeaponAmmo>();
        stats = GetComponent<WeaponStats>();
        hud = GetComponentInParent<PlayerHUD>();

        AudioSource[] sources = GetComponentsInChildren<AudioSource>();

        gunAudio = sources[0];
        gunShoot = sources[0].clip;
        gunReload = sources[1].clip;
        gunShell = sources[2].clip;

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
                isShooting = true;
            }
        }

        if (isShooting)
        {
            gunAudio.PlayOneShot(gunShoot);
            animator.SetTrigger("Shoot");
            muzzle.Play();

            for (int i = 0; i < shotgunShots; i++)
            {       
                ShotgunShot();
            }

            stats.shotsFired++;
            ammo.currentAmmo--;
            isShooting = false;
        }

        if (Input.GetButtonDown("Reload") && !isReloading && ammo.currentAmmo != ammo.maxAmmo)
        {
            isReloading = true;
            Reload();
        }

        Hitmarker();
    }

    void ShotgunShot()
    {
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
                hud.hitmarker.enabled = true;
                stats.shotsHit++;
                Health targetHP = hitObject.GetComponent<Health>();
                targetHP.TakeDamage(damage);
                // if (targetHP.currentHP <= 0)
                    // stats.killCount++;
                    
            }
        }
        else
        {
            Debug.DrawRay(playerCam.transform.position, direction, Color.red, 2);
        }
    }

    void Reload()
    {
        StartCoroutine(ShotgunReload());
    }

    IEnumerator ShotgunReload()
    {

        Debug.Log("Reloading...");

        // animator.SetTrigger("Reload");

        ammo.currentAmmo++;

        gunAudio.PlayOneShot(gunShell);

        yield return new WaitForSeconds(reloadTime);

        Debug.Log("Loaded shell");

        if (ammo.currentAmmo == ammo.maxAmmo)
        {
            isReloading = false;
        } else {
            Reload();
        }
    }

    void Hitmarker()
    {
        if (hud.hitmarker.enabled == true)
        {
            hTimer = hTimer + Time.deltaTime;
            if (hTimer >= hitTimer)
            {
                hud.hitmarker.enabled = false;
                hTimer = 0;
            }
        }
    }
}
