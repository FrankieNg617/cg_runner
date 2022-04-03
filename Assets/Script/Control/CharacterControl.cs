using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
    //  REF
    private CharacterController controller;
    private PlayerInputAction.GameplayActions gameplay;

    //  CONFIG PARAMS
    [SerializeField] float forwardSpeed;
    [SerializeField] float horizontalSpeed;
    [SerializeField] float acceleration = 100;
    [SerializeField] float laneDistance = 4; //distance between two lane

    [SerializeField] float jumpForce;
    [SerializeField] float gravity = -20;

    //  STATE
    private Vector3 direction;
    private bool jumpAction = false;
    private int desiredLane = 0; //-1:left 0:middle 1:right

    private float targetX;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        // Setup Input and Listener
        gameplay = new PlayerInputAction().Gameplay;
        gameplay.move.performed += SwitchLane;
        gameplay.jump.performed += ctx => jumpAction = true;
    }


    private void OnEnable()
    {
        gameplay.Enable();
    }

    private void OnDisable()
    {
        gameplay.Disable();
    }

    void Update()
    {
        if (!GameManage.isGameStarted || GameManage.isGameOver)  //unable to control the character if the game is not started yet
            return;


        if (direction.z < forwardSpeed)
            direction.z += acceleration * Time.deltaTime;  //increase the character's speed by 0.1 each sec

        HandleJump();

        HandleLaneMovement();

        if (!GameManage.isGameStarted || GameManage.isGameOver)
            return;

        controller.Move(direction * Time.deltaTime);
    }

    private void HandleLaneMovement()
    {
        Vector3 targetPos = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane != 0)
            targetPos += desiredLane * Vector3.right * laneDistance;

        direction.x = !Mathf.Approximately(transform.position.x, targetPos.x) ? (targetPos - transform.position).x * horizontalSpeed : 0;
    }

    private void HandleJump()
    {
        if (controller.isGrounded && jumpAction) //only able to jump when character is on the ground -> avoid double jump
        {
            direction.y = jumpForce;
            jumpAction = false;
        }
        else //only add the gravity to the character when it is jumping
        {
            direction.y += gravity * Time.deltaTime;
        }
    }

    private void SwitchLane(InputAction.CallbackContext ctx)
    {
        int value = (int)ctx.ReadValue<float>();
        desiredLane += value;

        desiredLane = Mathf.Clamp(desiredLane, -1, 1);
    }

    // Collision
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            GameManage.isGameOver = true;
        }

    }
}
