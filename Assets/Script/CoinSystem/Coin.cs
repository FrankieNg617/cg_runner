using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    CoinMagnet coinMagnetScript;

    void Start()
    {
        coinMagnetScript = gameObject.GetComponent<CoinMagnet>();
    }

    void Update()
    {
        transform.Rotate(0, Random.Range(80, 180) * Time.deltaTime, 0);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            collectCoin();
        }

        if(other.tag == "CoinDetector")
        {
            coinMagnetScript.enabled = true;

        }
    }

    public void collectCoin()
    {
        FindObjectOfType<AudioManage>().PlaySound("PickUpCoin");
        
        if(!GameManage.isMultiple)
        {
            GameManage.numberOfCoins += 1;
        }
        else
        {
            GameManage.numberOfCoins += 2;
        }
        
        Destroy(gameObject);
    }

}
