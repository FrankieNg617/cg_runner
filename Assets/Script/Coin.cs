using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

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

    }

    public void collectCoin()
    {
        FindObjectOfType<AudioManage>().PlaySound("PickUpCoin");
        GameManage.numberOfCoins += 1;
        Destroy(gameObject);
    }

}
