using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputAction.GameplayActions gameplay;

    private void Awake()
    {
        PlayerInputAction action = new PlayerInputAction();
        gameplay = action.Gameplay;
        gameplay.move.performed += Move;
        gameplay.jump.performed += Jump;
    }

    private void OnEnable()
    {
        gameplay.Enable();
    }

    private void OnDisable()
    {
        gameplay.Disable();
    }


    private void Move(InputAction.CallbackContext ctx)
    {

        print("Move performed, context: " + ctx.ReadValueAsObject());
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        print("Jump performed, context: " + ctx.ReadValueAsObject());
    }
}
