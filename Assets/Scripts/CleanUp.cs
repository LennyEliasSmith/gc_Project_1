using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : MonoBehaviour
{
    public float timer;
    public float cleanUpTime = 1f;

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;

        if (timer >= cleanUpTime)
            Destroy(this.gameObject);

    }
}
