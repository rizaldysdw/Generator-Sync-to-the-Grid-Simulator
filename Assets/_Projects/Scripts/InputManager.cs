using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;

    void Awake()
    {
        playerInput = new PlayerInput();
    }

    public Vector2 ReadMoveInput()
    {
        Vector2 moveInput = playerInput.Gameplay.Move.ReadValue<Vector2>();
        
        return moveInput;
    }

    public void ReadInteractInput(InputAction.CallbackContext context)
    {
        bool isInteractPressed = context.ReadValueAsButton();

        Debug.Log(isInteractPressed);
    }

    void OnEnable()
    {
        playerInput.Enable();    
    }

    void OnDisable()
    {
        playerInput.Disable();
    }
}
