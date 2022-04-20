using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    public Transform playerTransform;
    public float magnetSpeed = 20f;

    void Start()
    {
       playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, magnetSpeed * Time.deltaTime);
    }
}
