using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1; //0:left 1:middle 2:right
    public float laneDistance = 4; //distance between two lane

    public float jumpForce;
    public float gravity = -20;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(forwardSpeed < maxSpeed)
           forwardSpeed += 0.1f * Time.fixedDeltaTime;  //increase the character's speed by 0.1 each sec
           
        direction.z = forwardSpeed;
        

        if(Input.GetKeyDown(KeyCode.D))
        {
            desiredLane++;
            if(desiredLane == 3)
               desiredLane = 2;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            desiredLane--;
            if(desiredLane == -1)
               desiredLane = 0;
        }

        if(controller.isGrounded) //only able to jump when character is on the ground -> avoid double jump
        {
            direction.y = -1;
            if(Input.GetKeyDown(KeyCode.Space))
            {
               Jump();
            }
        }
        else //only add the gravity to the character when it is jumping
        {
            direction.y += gravity * Time.deltaTime;
        }

        Vector3 targetPos = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane == 0)
        {
            targetPos += Vector3.left * laneDistance;
        }else if(desiredLane == 2)
        {
            targetPos += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, 300 * Time.fixedDeltaTime);
        controller.center = controller.center;
    }


    private void FixedUpdate()
    {
        controller.Move(direction * Time.deltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;

    }

    

}
