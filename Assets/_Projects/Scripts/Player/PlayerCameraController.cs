using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;
    private Vector2 mouseMovement;

    private float xRotation;

    void Start()
    {
        // cameraY = transform.position.y - playerObject.transform.position.y;

        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get "Look" value in new input system
        mouseMovement = Mouse.current.delta.ReadValue();
        float mouseX = mouseMovement.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseMovement.y * mouseSensitivity * Time.deltaTime;

        // Calculate the camera rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

/*    void OnLookInput(InputAction.CallbackContext context)
    {
        Vector3 currentLookInput = context.ReadValue<Vector2>();
        Vector3 currentLook;

        currentLook.x = currentLookInput.x;
        currentLook.y = currentLookInput.y;

        // Get the mouse input
        float mouseX = currentLook.x * mouseSensitivity * Time.deltaTime;
        float mouseY = currentLook.y * mouseSensitivity * Time.deltaTime;

        // Calculate the camera rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }*/
}
