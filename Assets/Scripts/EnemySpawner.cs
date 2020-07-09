using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    private GameObject enemyClone;
    public float spawnTimer;
    public float timer;

    public bool spawnActive;

    // Start is called before the first frame update
    void Start()
    {
        spawnActive = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(!spawnActive)
        {
            timer = timer + Time.deltaTime;
            if(timer >= spawnTimer)
            {
                SpawnEnemy();
                timer = 0;
            }
            
        }

        if (enemyClone == null)
        {
            spawnActive = false;
        }
    }

    void SpawnEnemy()
    {
        enemyClone = Instantiate(enemy, transform.position, Quaternion.identity);
        spawnActive = true;
        timer = 0;
    }
}
