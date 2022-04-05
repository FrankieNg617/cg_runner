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
    private GameManage gameManager;
    [SerializeField] Animator anim;

    //  CONFIG PARAMS
    [SerializeField] float forwardSpeed;
    [SerializeField] float horizontalSpeed;
    [SerializeField] float acceleration = 100;
    [SerializeField] float laneDistance = 4; //distance between two lane

    [SerializeField] float jumpForce;
    [SerializeField] float gravity = -20;

    //  STATE
    private bool enableMovement = true;
    private Vector3 direction;
    private bool jumpAction = false;
    private int desiredLane = 0; //-1:left 0:middle 1:right


    void Awake()
    {
        controller = GetComponent<CharacterController>();
        gameManager = GameObject.FindWithTag("GameManage")?.GetComponent<GameManage>();
        gameManager.onGameStart += enableControl;
        gameManager.onGameOver += () => enableMovement = false;

        // Setup Input and Listener
        gameplay = new PlayerInputAction().Gameplay;
        gameplay.move.performed += SwitchLane;
        gameplay.jump.performed += ctx => jumpAction = true;
        gameplay.roll.performed += Roll;
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
        if (!enableMovement) 
            return;


        if (direction.z < forwardSpeed)
            direction.z += acceleration * Time.deltaTime;  //increase the character's speed by 0.1 each sec

        HandleJump();

        HandleLaneMovement();

        controller.Move(direction * Time.deltaTime);
    }

    private void HandleLaneMovement()
    {
        Vector3 targetPos = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane != 0)
            targetPos += desiredLane * Vector3.right * laneDistance;

        if (Mathf.Abs(transform.position.x - targetPos.x) > 0.1f)
        {
            Vector3 dir = (targetPos - transform.position);
            direction.x = dir.x * horizontalSpeed;
        }
        else
        {
            direction.x = 0;
        }

        if (Mathf.Abs(transform.position.x - targetPos.x) > 0.5)
            anim.SetFloat("xDirection", Mathf.Sign(direction.x) / 2);
        else
            anim.SetFloat("xDirection", 0f);
    }

    private void HandleJump()
    {
        if (controller.isGrounded && jumpAction) //only able to jump when character is on the ground -> avoid double jump
        {
            direction.y = jumpForce;
            anim.SetTrigger("Jump");
            jumpAction = false;
        }
        else //only add the gravity to the character when it is jumping
        {
            direction.y += gravity * Time.deltaTime;
        }
        anim.SetBool("Run", controller.isGrounded);
    }

    private void SwitchLane(InputAction.CallbackContext ctx)
    {
        int value = (int)ctx.ReadValue<float>();
        desiredLane += value;

        desiredLane = Mathf.Clamp(desiredLane, -1, 1);
    }
    private void Roll(InputAction.CallbackContext obj)
    {
        anim.SetTrigger("Roll");
    }

    // Callback
    private void enableControl()
    {
        enableMovement = true;
        anim.SetBool("Run", true);
    }

    // Collision
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        /*
        if (hit.transform.tag == "Obstacle")
        {
            gameManager.isGameOver = true;
        }
        */


        //game over only if the character collided with the obstacles in front of it 
        if (hit.gameObject.tag == "Obstacle" && hit.point.z > transform.position.z + controller.radius)
        {
            gameManager.isGameOver = true;
        }
    }
}
