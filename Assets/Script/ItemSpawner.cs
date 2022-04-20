using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject item;

    void Start()
    {
        item.SetActive(false);

        int prob = Random.Range(0, 5);
        if(prob >= 3)
           item.SetActive(true);
    
    }


}
