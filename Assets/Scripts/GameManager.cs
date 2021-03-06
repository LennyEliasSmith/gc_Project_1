﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public PlayerHUD hud;

    public GameObject[] enemies;
    public float killCount;
    public float gameTimer;

    private int levelNumber;

    public bool isArena;
    public bool gameActive;
    public bool gameEnd;
    public bool gameEndCheck;

    private waveScript waveScript;
    

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        waveScript = GetComponent<waveScript>();
        gameEndCheck = true;
    }

    // Update is called once per frame
    void Update()
    {

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (gameEnd == false && gameActive == true)
        gameTimer = gameTimer + Time.deltaTime;

        if (Input.GetButtonDown("Function1"))
            Restart();

        if (Input.GetButtonDown("Function2"))
            DebugCursor();

        if (Input.GetButtonDown("Function3"))
            LoadMenu();
        

        if (enemies.Length == 0 && gameEndCheck && gameActive && !isArena)
        {
            GameEnd();
            hud.CalculateAcc();
            hud.GameEndOverlay();
        }
    }

    void Restart()
    {
        killCount = 0;
        gameTimer = 0;
        waveScript.nextWave = 0;

        SceneManager.LoadScene(levelNumber);
    }

    public void PlayerDeath()
    {
        GameEnd();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        hud.CalculateAcc();
        hud.GameEndOverlay();

        GameObject weaponHolder = GameObject.Find("WeaponHolder");
        weaponHolder.SetActive(false);

    }

    public void LoadMenu()
    {
        killCount = 0;
        gameEnd = false;
        gameTimer = 0;
        waveScript.nextWave = 0;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        SceneManager.LoadScene(0);
    }

    public void GameEnd()
    {
        gameEnd = true;
        gameEndCheck = false;

        gameTimer = Mathf.RoundToInt(gameTimer);
    }

    void DebugCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0 || level == 1)

            levelNumber = level;

            isArena = false;
            gameActive = false;

        if (level == 2 || level == 3)
        {
            levelNumber = level;

            isArena = false;
            player = GameObject.FindGameObjectWithTag("Player");
            hud = player.GetComponentInChildren<PlayerHUD>();
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            gameEnd = false;
            gameActive = true;
            gameEndCheck = true;
        }

        if (level == 4)
        {
            levelNumber = level;

            isArena = true;
            player = GameObject.FindGameObjectWithTag("Player");
            hud = player.GetComponentInChildren<PlayerHUD>();
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            gameEnd = false;
            gameActive = true;
            gameEndCheck = true;
        }
    }





}
