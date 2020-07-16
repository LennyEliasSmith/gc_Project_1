using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightPingPong : MonoBehaviour
{

    public Transform lightObj;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lightObj.position = new Vector2(Mathf.PingPong(Time.time * speed, 100), transform.position.y);
    }
}
