using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public GameObject magnet;
    public GameObject coinDetector;
    private GameManage gameManager;

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManage")?.GetComponent<GameManage>();
    }

    void Update()
    {
        transform.Rotate(0, 100 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<AudioManage>().PlaySound("PickUpCoin");
            coinDetector.SetActive(true);
            Destroy(magnet);
            gameManager.Magnet();
        }
    }

}
