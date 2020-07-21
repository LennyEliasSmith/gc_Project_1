using System.Collections;
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

    public bool gameActive;
    public bool gameEnd;
    public bool gameEndCheck;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
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
        

        if (enemies.Length == 0 && gameEndCheck && gameActive)
        {
            GameEnd();
            hud.CalculateAcc();
            hud.GameEndOverlay();
        }
    }

    void Restart()
    {
        killCount = 0;
        gameEndCheck = true;

        SceneManager.LoadScene(1);
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
        if (level == 0)
            gameActive = false;

        if (level == 1)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            hud = player.GetComponentInChildren<PlayerHUD>();
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            gameTimer = 0;
            gameActive = true;
        }
    }





}
