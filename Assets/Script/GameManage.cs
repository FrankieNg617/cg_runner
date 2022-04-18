using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public GameObject gameOverPanel;
    private AudioManage audioManage;

    public event Action onGameStart;
    public event Action onGameOver;

    private void Awake()
    {
        audioManage = FindObjectOfType<AudioManage>();
    }

    public void EndGame()
    {
        audioManage.StopSound("MainTheme");
        audioManage.PlaySound("GameOverTheme");
        audioManage.PlaySound("GameOverVoice");
        if (onGameOver != null) onGameOver();
        gameOverPanel.SetActive(true);
    }
}
