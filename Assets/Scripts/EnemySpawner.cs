using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTimer = 5f;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;

        if(timer >= spawnTimer)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
        timer = 0;
    }
}
