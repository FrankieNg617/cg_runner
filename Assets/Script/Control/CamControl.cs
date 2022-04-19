using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Transform target; //character's position
    private Vector3 offset; //offset between character and cam
    private Vector3 moveVector;

    private Transform playerTransform;

    void Start()
    {
        offset = transform.position - Vector3.zero - new Vector3(0, 0, 2);
    }


    void LateUpdate()
    {
        if (target == null)
        {
            target = GameObject.FindWithTag("Player")?.transform;
            return;
        }
        moveVector = target.position + offset;
        moveVector.x = target.position.x;
        moveVector.y = target.position.y + 6;

        transform.position = moveVector;
    }

}
