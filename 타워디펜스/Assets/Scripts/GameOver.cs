using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundText;

    public SceneFader sceneFader;
    void OnEnable()
    {
        //roundText.text = PlayerState.round.ToString();
    }

    public void Retry()
    {
        WaveSpawner.waveIndex = 0;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("SecondGameScene");
        //sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        //sceneFader.FadeTo("SecondGameScene");
        //퍼즈상태일때도 onButton 써야해서 시간 정상으로 돌리기 
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        WaveSpawner.waveIndex = 0;
        SceneManager.LoadScene("MainScene");
        //sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        //sceneFader.FadeTo("MainScene");   
        SceneFader.isCheck = true;

        //퍼즈상태일때도 onButton 써야해서 시간 정상으로 돌리기
        Time.timeScale = 1f;
        //StartCoroutine(LoadSceneAsync());

    }
  
}
