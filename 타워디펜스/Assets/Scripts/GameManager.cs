using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameOver = false;
    public static bool pause = false;
    public static bool pKey = false;

    public GameObject gameOverUI;
    public GameObject pauseUI;
    public GameObject nextRoundUI;

    private Enemy enemy;
    private WaveSpawner waveSpawner;
    void Start()
    {
        enemy = FindObjectOfType<Enemy>(); 
    }

    void Update()
    {
        if (gameOver) return;
        if (pause) return;
        if (pKey) return;

        //라이프 0되면 게임오버 ui 띄우기
        if(PlayerState.Life <= 0)
        {
            EndGame();
        }

        if (!pause)
        {
            if(Input.GetKey("p"))
            {
                Pause();
            }
        }

        if (pause)
        {
            Debug.Log("퍼즈");
            pauseUI.SetActive(true);
            Time.timeScale = 0f;

            if (Input.GetKey("t")) Time.timeScale = 1f;
        }

    }

    void EndGame()
    {
        gameOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void Pause()
    {
            pauseUI.SetActive(true);
            Time.timeScale = 0f;
            pause = true;
        
    }

    public void Continue()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        pause = false;
    }

    public void NextRound()
    {
        nextRoundUI.SetActive(true);
        Time.timeScale = 1f;
    }

    public void NextRoundButton()
    {
        nextRoundUI.SetActive(false);
        SceneManager.LoadScene("ThirdGameScene");
        WaveSpawner.waveIndex = 0;
    }

}
