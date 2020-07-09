  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveScript : MonoBehaviour
 {
    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    //Wave variables
    [System.Serializable]
    public class Wave
    {
        public string name;

        public Transform enemy;

        public int count;

        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountDown = 1f;

    private SpawnState state = SpawnState.COUNTING;


    void Start()
    {
        waveCountdown = timeBetweenWaves;
        
    }

    void Update()
    {
        Debug.Log(waveCountdown);
        Debug.Log(EnemyIsAlive());
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                Debug.Log("Wave Completed");
                return;
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
            
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if(searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
       
        return true;
    }

    IEnumerator SpawnWave(Wave waves)
    {
        Debug.Log("Spawning");
        state = SpawnState.SPAWNING;

        for(int i = 0; i < waves.count; i++)
        {
            SpawnEnemy(waves.enemy);
            yield return new WaitForSeconds(1f / waves.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Instantiate(enemy, transform.position, transform.rotation);
        Debug.Log("spawning enemy: " + enemy.name);
    }

}
