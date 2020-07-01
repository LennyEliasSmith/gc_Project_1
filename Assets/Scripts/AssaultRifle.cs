using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour
{
    public float fireRate;
    public float nextTimeToFire;
    public float reloadTime;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
