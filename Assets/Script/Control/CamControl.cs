using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Transform target; //character's position
    private Vector3 offset; //offset between character and cam
    private Vector3 moveVector;


    void Start()
    {
        offset = transform.position - target.position - new Vector3(0,0,2);
    }


    void LateUpdate()
    {
        moveVector = target.position + offset;
        moveVector.x = transform.position.x;
        moveVector.y = transform.position.y;

        transform.position = moveVector;
    }
}
