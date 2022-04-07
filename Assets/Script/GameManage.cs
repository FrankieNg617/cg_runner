using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    //public bool isGameStarted;
    public bool isGameOver;
    //public GameObject startingText;
    public GameObject gameOverPanel;

    public event Action onGameStart;
    public event Action onGameOver;

    void Start()
    {
        //isGameStarted = false;
        isGameOver = false;
    }


    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
            print(isGameStarted);
            if (onGameStart != null) onGameStart();
            Destroy(startingText);
        }
        */

        if (isGameOver)
        {
            if (onGameOver != null) onGameOver();
            gameOverPanel.SetActive(true);
        }
    }

/*
    public void StartGame()
    {
        isGameStarted = true;
        if (onGameStart != null) onGameStart();
        Destroy(startingText);
    }
    */
}
