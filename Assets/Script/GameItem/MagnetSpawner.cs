using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSpawner : MonoBehaviour
{
    public GameObject magnet;

    void Start()
    {
        magnet.SetActive(false);

        int prob = Random.Range(0, 5);
        if(prob >= 3)
           magnet.SetActive(true);
    
    }


}
