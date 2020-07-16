using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHP;
    public float maxHP;
    public float deathTime;

    private bool isDead;

    public AudioSource deathSound;
    public AudioClip[] deathSounds;
    public AudioSource hitSound;
    public AudioClip[] hitSounds;

    public SpriteRenderer sprite;

    private GameObject managerObject;
    private GameManager managerScript;

    void Start()
    {
        currentHP = maxHP;
        sprite = GetComponent<SpriteRenderer>();
        managerObject = GameObject.FindGameObjectWithTag("GameManager");
        managerScript = managerObject.GetComponent<GameManager>();
        isDead = false;

    }

    void Update()
    {
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public void TakeDamage(float dmg)
    {
        
        currentHP = currentHP - dmg;

        if (isDead)
            return;

        if (currentHP <= 0)
        {
            DeathSound();
            isDead = true;
            if(this.CompareTag("Enemy"))
            {
                sprite.enabled = false;
            }
            StartCoroutine(Die());
        } else
            HurtSound();
    }

    IEnumerator Die()
    {
        if (this.gameObject.CompareTag("Enemy"))
            managerScript.killCount++;

        // Debug.Log("death");
        yield return new WaitForSeconds(deathTime);
        
        Destroy(this.gameObject);
    }

    void DeathSound()
    {
        deathSound.clip = deathSounds[Random.Range(0, deathSounds.Length)];
        deathSound.Play();
    }

    void HurtSound()
    {
        hitSound.clip = hitSounds[Random.Range(0, hitSounds.Length)];
        hitSound.Play();
    }
}
