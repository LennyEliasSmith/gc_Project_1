using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text txt;

    private float time = 0;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        float displayTime = Mathf.Round(time * 100f) / 100f;

        txt.text = "Time: " + displayTime;
    }
}
