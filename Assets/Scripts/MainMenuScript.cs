using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public string loadLevel;

    public void StartGame()
    {
        Cursor.visible = false;
        SceneManager.LoadScene(1);
    }
}
