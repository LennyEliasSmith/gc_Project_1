  using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public TextMeshProUGUI waveText;

    public GameObject[] spawnPoints;

    public Wave[] waves;
    public int nextWave = 0;

    public float timeBetweenWaves;
    public float waveCountdown;

    private float searchCountDown = 1f;

    private GameManager gManager;
    private GameObject player;
    private PlayerHUD hud;
    private SpawnState state = SpawnState.COUNTING;

    private bool waveTextSet;


    void Start()
    {
        gManager = GetComponent<GameManager>();
        player = gManager.player;
        hud = player.GetComponentInChildren<PlayerHUD>();
        
        waveCountdown = timeBetweenWaves;

        waveTextSet = false;

        if (spawnPoints.Length == 0)
        {
            Debug.Log("No Spawn Points Referenced");
        }

    }

    void Update()
    {

        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawner");

        // Debug.Log(EnemyIsAlive());

        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted(); 
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0 && gManager.isArena)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
            
        }
        else if (gManager.isArena)
        {
            waveCountdown -= Time.deltaTime;
        }

        
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        state = SpawnState.COUNTING;

        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All Waves complete");
        }

        nextWave++;
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
        
        waveText.text = waves.name;

        for(int i = 0; i < waves.count; i++)
        {
            // waves.name = waveText.ToString();
            SpawnEnemy(waves.enemy);
            yield return new WaitForSeconds(1f / waves.rate);
            // Debug.Log(waves.name);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
        Instantiate(enemy, _sp.position, _sp.rotation);
        Debug.Log("spawning enemy: " + enemy.name);
    }

}
