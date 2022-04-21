using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    CoinMagnet coinMagnetScript;
    private GameManage gameManager;

    void Start()
    {
        coinMagnetScript = gameObject.GetComponent<CoinMagnet>();
        gameManager = GameObject.FindWithTag("GameManage")?.GetComponent<GameManage>();
    }

    void Update()
    {
        transform.Rotate(0, Random.Range(80, 180) * Time.deltaTime, 0);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            collectCoin();
        }

        if (other.tag == "CoinDetector")
        {
            coinMagnetScript.enabled = true;

        }
    }


    public void collectCoin()
    {
        FindObjectOfType<AudioManage>().PlaySound("PickUpCoin");

        if (!GameManage.isMultiple)
        {
            GameManage.numberOfCoins += 1;
        }
        else
        {
            gameManager.showFloatingText();
            GameManage.numberOfCoins += 2;
        }

        gameObject.SetActive(false);
    }

}
