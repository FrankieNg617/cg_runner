using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private GameManage gameManager;
    public GameObject magnet;

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
            FindObjectOfType<AudioManage>().PlaySound("PickUpMagnet");
            Destroy(magnet);
            gameManager.onMagnet();
        }
    }

}
