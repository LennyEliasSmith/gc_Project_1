using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public float ammoMultiplier;

    public Sprite[] sprites;
    public Sprite ammoSprite;

    public AudioSource pickUpSound;
    public float deathTimer;

    private Transform playerTform;
    private GameObject player;
    private SpriteRenderer spriteRender;
    private Collider ammoCollider;

    void Start()
    {

        playerTform = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRender = GetComponent<SpriteRenderer>();
        ammoCollider = GetComponent<Collider>();

        // assign a random sprite to define which ammo the pickup grants
        // sprites[0] = pistol ammo
        // sprites[1] = rifle ammo
        // sprites[2] = shotgun ammo

        ammoSprite = sprites[Random.Range(0, sprites.Length)];
        spriteRender.sprite = ammoSprite;
    }

    void Update()
    {
        transform.LookAt(playerTform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Pistol pistol = player.GetComponentInChildren(typeof(Pistol),true) as Pistol;
            AssaultRifle rifle = player.GetComponentInChildren(typeof(AssaultRifle),true) as AssaultRifle;
            Shotgun shotgun = player.GetComponentInChildren(typeof(Shotgun),true) as Shotgun;

            if (ammoSprite == sprites[0]) {
                WeaponAmmo ammo = pistol.GetComponent<WeaponAmmo>();
                if(ammo.currentReserveAmmo != ammo.maxReserveAmmo)
                {
                    pickUpSound.Play();
                    ammo.currentReserveAmmo = ammo.currentReserveAmmo + (ammoMultiplier * ammo.maxMag);
                    if (ammo.currentReserveAmmo > ammo.maxReserveAmmo)
                        ammo.currentReserveAmmo = ammo.maxReserveAmmo;
                    StartCoroutine(DeleteObject());
                }

            } else if (ammoSprite == sprites[1]) {

                WeaponAmmo ammo = rifle.GetComponent<WeaponAmmo>();
                if(ammo.currentReserveAmmo != ammo.maxReserveAmmo)
                {
                    pickUpSound.Play();
                    ammo.currentReserveAmmo = ammo.currentReserveAmmo + (ammoMultiplier * ammo.maxMag);
                    if (ammo.currentReserveAmmo > ammo.maxReserveAmmo)
                        ammo.currentReserveAmmo = ammo.maxReserveAmmo;
                    StartCoroutine(DeleteObject());
                }

            } else if (ammoSprite == sprites[2]) {
                WeaponAmmo ammo = shotgun.GetComponent<WeaponAmmo>();
                if (ammo.currentReserveAmmo != ammo.maxReserveAmmo)
                {
                    pickUpSound.Play();
                    ammo.currentReserveAmmo = ammo.currentReserveAmmo + (ammoMultiplier * ammo.maxMag);
                    if (ammo.currentReserveAmmo > ammo.maxReserveAmmo)
                        ammo.currentReserveAmmo = ammo.maxReserveAmmo;
                    StartCoroutine(DeleteObject());
                }
            }
        }
    }

    IEnumerator DeleteObject()
    {
        spriteRender.enabled = false;
        ammoCollider.enabled = false;

        yield return new WaitForSeconds(deathTimer);

        Destroy(this.gameObject);

    }
}
