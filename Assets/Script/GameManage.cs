using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameManage : MonoBehaviourPunCallbacks
{
    public GameObject gameOverPanel;
    private AudioManage audioManage;

    public bool isMultiplayer = false;

    [SerializeField] TextMeshProUGUI countDownTxt;
    [SerializeField] int countDownSeconds = 5;

    public event Action onGameStart;
    public event Action onGameOver;

    public static int numberOfCoins;
    public Text score;
    public Text finalScore;
    public Text highScore;

    [SerializeField] float magnetDuration;
    [SerializeField] float multipleDuration;
    [SerializeField] GameObject coinDetector;
    public static bool isMultiple = false;
    public static bool isMagnet = false;

    [SerializeField] GameObject floatingTextPrefab;
    [SerializeField] GameObject player;

    [SerializeField] Image magnetImg;
    [SerializeField] Image multipleImg;

    [SerializeField] GameObject magnetTimerBar;
    [SerializeField] GameObject multipleTimerBar;

    private void Awake()
    {
        GameObject bgm = GameObject.FindGameObjectWithTag("WelcomeBGM");
        if(bgm != null)
        {
            bgm.GetComponent<AudioSource>().Stop();
        }

        audioManage = FindObjectOfType<AudioManage>();
        numberOfCoins = 0;
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    private void Start()
    {
        if (!isMultiplayer) StartCountDown();
    }

    [PunRPC]
    private void StartCountDown()
    {
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        int curSec = countDownSeconds;
        countDownTxt.gameObject.SetActive(true);
        countDownTxt.text = curSec.ToString();
        while (curSec > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            curSec--;
            countDownTxt.text = curSec.ToString();
        }
        countDownTxt.gameObject.SetActive(false);
        audioManage.PlaySound("MainTheme");
        if (onGameStart != null) onGameStart();
    }

    public void EndGame()
    {
        audioManage.StopSound("MainTheme");
        audioManage.PlaySound("GameOverTheme");
        audioManage.PlaySound("GameOverVoice");
        onGameOver();
        gameOverPanel.SetActive(true);

        finalScore.text = numberOfCoins.ToString();
        if (numberOfCoins > PlayerPrefs.GetInt("HighScore", 0))
        {
            //Store the highest score into the system 
            PlayerPrefs.SetInt("HighScore", numberOfCoins);
            highScore.text = numberOfCoins.ToString();
        }
    }

    public void onMagnet()
    {
        if(isMagnet) return;  //disallow the double pickup of magnet
        if (coinDetector == null) coinDetector = GameObject.FindWithTag("Player").GetComponent<CharacterControl>().coinDetector;

        isMagnet = true;
        coinDetector.SetActive(true);
        magnetImg.color = new Color(1f, 1f, 1f, 1f);
        magnetTimerBar.SetActive(true);
        magnetTimerBar.GetComponent<TimerBarManage>().startTimer();

        StartCoroutine(magnetTimer(magnetDuration));
    }

    IEnumerator magnetTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        magnetImg.color = new Color(1f, 1f, 1f, 0.4f);
        magnetTimerBar.SetActive(false);
        coinDetector.SetActive(false);
    }

    public void onMultiple()
    {
        if(isMultiple) return;  //disallow the double pickup of multiple

        isMultiple = true;
        multipleImg.color = new Color(1f, 1f, 1f, 1f);
        multipleTimerBar.SetActive(true);
        multipleTimerBar.GetComponent<TimerBarManage>().startTimer();

        StartCoroutine(multipleTimer(multipleDuration));
    }

    IEnumerator multipleTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        isMultiple = false;
        multipleImg.color = new Color(1f, 1f, 1f, 0.4f);
        multipleTimerBar.SetActive(false);
    }

    public void showFloatingText()
    {
        if (player == null) player = GameObject.FindWithTag("Player");
        Vector3 floatingTextPos = new Vector3(player.transform.position.x + 1, player.transform.position.y + 5, player.transform.position.z);
        Instantiate(floatingTextPrefab, floatingTextPos, Quaternion.identity);
    }

    void Update()
    {
        score.text = numberOfCoins.ToString();
    }
}
