using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Transform target; //character's position
    private Vector3 offset; //offset between character and cam

    void Start()
    {
        offset = transform.position - target.position;
    }

    
    void LateUpdate()
    {
        //claculate the new position of cam per frame
        //only need to consider the z coordinate of cam; x and y would not change
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z+target.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, 10 * Time.deltaTime);
    }
}
