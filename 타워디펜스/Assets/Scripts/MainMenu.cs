using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    bool start = false;

    public int alpha =255;

    SpriteRenderer render;
    public GameObject go;

    public SceneFader sceneFader;
    public string firstRound = "SecondGameScene";


    void Start()
    {
        sceneFader = FindObjectOfType<SceneFader>();

        render = go.GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (start)
        {
            //sceneFader.FadeTo("SecondGameScene");
            SceneManager.LoadScene("SecondGameScene");

            start = false;
            Debug.Log(alpha);
        }
    }

    public void OnclickStartGame()
    {
        start = true;

    }

    public void OnclickExit()
    {
        Debug.Log("dfgfdg");
    }
}
