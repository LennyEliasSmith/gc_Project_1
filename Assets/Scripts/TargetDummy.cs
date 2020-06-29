using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{

    public float health = 1;
    public float deathTime = 0.5f;

    public AudioSource deathSound;

    public AudioClip[] audioSources;

    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        deathSound = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        health--;

        if(health <= 0)
        {
            RandomSound();

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

    void RandomSound()
    {
        deathSound.clip = audioSources[Random.Range(0, audioSources.Length)];
        deathSound.Play();
    }
}
