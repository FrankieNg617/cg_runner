using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    public GameObject gameOverPanel;
    private AudioManage audioManage;

    public event Action onGameStart;
    public event Action onGameOver;

    public static int numberOfCoins;
    public Text score;
    public Text finalScore;
    public Text highScore;

    private void Awake()
    {
        audioManage = FindObjectOfType<AudioManage>();
        numberOfCoins = 0;
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void EndGame()
    {
        audioManage.StopSound("MainTheme");
        audioManage.PlaySound("GameOverTheme");
        audioManage.PlaySound("GameOverVoice");
        if (onGameOver != null) onGameOver();
        gameOverPanel.SetActive(true);

        finalScore.text = numberOfCoins.ToString();
        if(numberOfCoins > PlayerPrefs.GetInt("HighScore", 0))
        {
            //Store the highest score into the system 
            PlayerPrefs.SetInt("HighScore", numberOfCoins);
            highScore.text = numberOfCoins.ToString();
        }
           
    }

    void Update()
    {
        score.text = numberOfCoins.ToString();
    }
}
