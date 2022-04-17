using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Transform target; //character's position
    private Vector3 offset; //offset between character and cam
    private Vector3 moveVector;

    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        offset = transform.position - target.position - new Vector3(0, 0, 2);
    }


    void LateUpdate()
    {
        moveVector = target.position + offset;
        moveVector.x = playerTransform.position.x;
        moveVector.y = target.position.y + 6;

        transform.position = moveVector;
    }

}
