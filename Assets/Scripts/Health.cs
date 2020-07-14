using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHP;
    public float maxHP;
    public float deathTime;

    public AudioSource deathSound;
    public AudioClip[] deathSounds;
    public AudioSource hitSound;
    public AudioClip[] hitSounds;

    public SpriteRenderer sprite;

    void Start()
    {
        currentHP = maxHP;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public void TakeDamage(float dmg)
    {
        
        currentHP = currentHP - dmg;

        if (currentHP <= 0)
        {
            DeathSound();
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
