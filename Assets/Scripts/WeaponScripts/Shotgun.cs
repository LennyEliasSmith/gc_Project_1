﻿using System.Collections;
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
    public AudioClip gunShell;
    public AudioClip gunPump;
    public AudioClip gunEmpty;

    public WeaponAmmo ammo;
    public WeaponStats stats;

    public ParticleSystem muzzle;
    public ParticleSystem tracers;
    public ParticleSystem blood;
    public ParticleSystem sparks;

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
        gunPump = sources[1].clip;
        gunShell = sources[2].clip;
        gunEmpty = sources[3].clip;

        muzzle = GetComponentInChildren<ParticleSystem>();

        isReloading = false;

    }

    // Update is called once per frame 
    void Update()
    {
        // AnimationLogic();

        if (ammo.currentMag > 0 && !isReloading && !isShooting && Time.time >= nextTimeToFire)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                isShooting = true;
            }

            if (Input.GetButtonDown("Fire2"))
            {
                DebugAnimation();
            }
        }
        else if (ammo.currentMag == 0 && !isReloading && !isShooting && Time.time >= nextTimeToFire)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                gunAudio.PlayOneShot(gunEmpty);
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
            ammo.currentMag--;
            isShooting = false;
        }

        if (Input.GetButtonDown("Reload") && !isReloading && ammo.currentMag != ammo.maxMag && ammo.currentReserveAmmo > 0)
        {
            isReloading = true;
            animator.SetTrigger("InsertFirstShell");
            Reload();
        }

        if(isReloading && (Input.GetButtonDown("Fire1") && ammo.currentMag != ammo.maxMag))
        {
            isReloading = false;
            gunAudio.PlayOneShot(gunPump);
            animator.ResetTrigger("InsertShell");
            animator.SetTrigger("FinishReload");
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

        ParticleSystem tracerClone = Instantiate(tracers, this.gameObject.transform);
        tracerClone.transform.position = tracers.transform.position;
        tracerClone.transform.forward = direction;
        tracerClone.gameObject.AddComponent<CleanUp>();

        tracerClone.Play();

        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, direction, out hit))
        {
            
            Debug.DrawLine(playerCam.transform.position, hit.point, Color.green, 2);
            hitObject = hit.transform.gameObject;
            // Debug.Log(hitObject);

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

        // Debug.Log("Reloading...");

        ammo.currentMag++;
        ammo.currentReserveAmmo--;

        gunAudio.PlayOneShot(gunShell);

        yield return new WaitForSeconds(reloadTime);

        // Debug.Log("Loaded shell");

        if (ammo.currentMag == ammo.maxMag)
        {
            // Debug.Log("Finished Reload");
            animator.SetTrigger("FinishReload");
            gunAudio.PlayOneShot(gunPump);
            isReloading = false;
        }
        else if (isReloading && ammo.currentReserveAmmo > 0)
        {
            animator.SetTrigger("InsertShell");
            Reload();
        } else if (isReloading && ammo.currentReserveAmmo == 0)
        {
            animator.SetTrigger("FinishReload");
            gunAudio.PlayOneShot(gunPump);
            isReloading = false;
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

    void DebugAnimation()
    {
        animator.ResetTrigger("FinishReload");
        animator.ResetTrigger("InsertShell");
        animator.ResetTrigger("InsertFirstShell");
        animator.ResetTrigger("Shoot");
    }

    /* void AnimationLogic()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("shotgun_reload")
            || this.animator.GetCurrentAnimatorStateInfo(0).IsName("shotgun_reload_loop")
            || this.animator.GetCurrentAnimatorStateInfo(0).IsName("shotgun_reload_loop 0")
            || this.animator.GetCurrentAnimatorStateInfo(0).IsName("shotgun_reload_finish")) {
            isReloading = true;
        } else
            isReloading = false;

        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("shotgunIdle")) 
        {
            animator.ResetTrigger("FinishReload");
            animator.ResetTrigger("InsertShell");
            animator.ResetTrigger("InsertFirstShell");
            animator.ResetTrigger("Shoot");
        }
    } */
}
