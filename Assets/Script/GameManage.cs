using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public static bool isGameStarted;
    public static bool isGameOver;
    public GameObject startingText;
    public GameObject gameOverPanel;


    void Start()
    {
        isGameStarted = false;
        isGameOver = false;
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
            Destroy(startingText);
        }

        if(isGameOver)
        {
            gameOverPanel.SetActive(true);
        }

    }
}
