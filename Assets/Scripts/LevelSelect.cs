using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager manager;

    private void Start()
    {
       
    }

    public void LoadNightClub()
    {
        SceneManager.LoadScene(2);
    }

    public  void LoadOffices()
    {
        SceneManager.LoadScene(3);

    }

    public void LoadGauntlet()
    {
        SceneManager.LoadScene(4);
    }
}
