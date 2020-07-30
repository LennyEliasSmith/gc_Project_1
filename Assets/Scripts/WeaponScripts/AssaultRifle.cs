using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour
{
    public float damage;
    public float gunSpread;
    public float fireRate;
    public float nextTimeToFire;
    public float reloadTime;

    public bool isReloading;
    public bool isShooting;

    private Camera playerCam;
    private GameObject hitObject;
    private PlayerHUD hud;

    public AudioSource gunAudio;
    public AudioClip gunShoot;
    public AudioClip gunReload;
    public AudioClip gunEmpty;

    public ParticleSystem muzzle;
    public ParticleSystem tracers;
    public ParticleSystem blood;
    public ParticleSystem sparks;

    public WeaponAmmo ammo;
    public WeaponStats stats;

    public Animator animator;

    public float hitTimer;
    public float hTimer;

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
        gunEmpty = sources[2].clip;

        muzzle = GetComponentInChildren<ParticleSystem>();

        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ammo.currentMag > 0 && !isReloading && !isShooting && Time.time >= nextTimeToFire)
        {
            if (Input.GetButton("Fire1"))
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            } else {
                animator.SetBool("isShooting", false);
                // muzzle.Stop();
            }
        }
        else if (ammo.currentMag == 0 && !isReloading && !isShooting && Time.time >= nextTimeToFire)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                gunAudio.PlayOneShot(gunEmpty);
            }
        }

        if (ammo.currentMag == 0)
        {
            animator.SetBool("isShooting", false);

        }

        Hitmarker();

        if (Input.GetButtonDown("Reload") && !isReloading && ammo.currentMag != ammo.maxMag && ammo.currentReserveAmmo > 0)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        muzzle.Play();

        isShooting = true;

        gunAudio.PlayOneShot(gunShoot);
        animator.SetBool("isShooting", true);

        stats.shotsFired++;

        Vector3 direction = playerCam.transform.forward;
        Vector3 spread = new Vector3();

        spread += playerCam.transform.up * Random.Range(-gunSpread, gunSpread);
        spread += playerCam.transform.right * Random.Range(-gunSpread, gunSpread);
        direction += spread.normalized * Random.Range(0f, gunSpread);

        ParticleSystem tracerClone = Instantiate(tracers, this.gameObject.transform);
        tracerClone.transform.position = tracers.transform.position;
        tracerClone.transform.forward = direction;
        tracerClone.gameObject.AddComponent<CleanUp>();

        tracerClone.Play();

        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, direction, out hit))
        {
            hitObject = hit.transform.gameObject;

            // Debug.Log(hitObject);
            Debug.DrawLine(playerCam.transform.position, hit.point, Color.green, 2);

            if (hitObject.CompareTag("Enemy"))
            {
                hud.hitmarker.enabled = true;
                ParticleSystem bloodClone = Instantiate(blood, hit.point, Quaternion.LookRotation(hit.normal));
                bloodClone.gameObject.AddComponent<CleanUp>();
                stats.shotsHit++;
                Health targetHP = hitObject.GetComponent<Health>();
                targetHP.TakeDamage(damage);
            } else if (hitObject.CompareTag("E nvironment")) {
                ParticleSystem sparkClone = Instantiate(sparks, hit.point, Quaternion.LookRotation(hit.normal));
                sparkClone.gameObject.AddComponent<CleanUp>();
            }
        }

        ammo.currentMag--;
        isShooting = false;
        // animator.SetTrigger("Shoot");

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

    IEnumerator Reload()
    {

        // Debug.Log("Reloading...");
        animator.SetTrigger("Reload");
        gunAudio.PlayOneShot(gunReload);

        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        ammo.Reload();

        // Debug.Log("Finished reload");

        isReloading = false;
    }
}
