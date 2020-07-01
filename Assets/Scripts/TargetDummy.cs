using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{

    public Health hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
