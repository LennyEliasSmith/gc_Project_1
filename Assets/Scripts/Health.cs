using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHP;
    public float maxHP;
    public float deathTime;

    public AudioSource deathSound;
    public AudioClip[] audioSources;

    public SpriteRenderer sprite;

    void Start()
    {
        currentHP = maxHP;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    public void TakeDamage(float dmg)
    {
        currentHP = currentHP - dmg;

        if (currentHP <= 0)
        {
            DeathSound();
            sprite.enabled = false;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        Debug.Log("death");
        yield return new WaitForSeconds(deathTime);
        Destroy(this.gameObject);
    }

    void DeathSound()
    {
        deathSound.clip = audioSources[Random.Range(0, audioSources.Length)];
        deathSound.Play();
    }
}
