using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private Camera cameraTransform;

    // Reference variables
    private CharacterController controller;
    public PlayerInput playerInput;

    // Reference to store Player Input values
    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private Vector3 cameraRelativeMovement;
    private bool isMovementPressed;
    private bool isStatsShowed;

    // Constants
    private float rotationFactorPerFrame = 8f;

    // UI References
    public GameObject statsContainer;

    private Vector3 playerVelocity;
    private bool groundedPlayer;

    void Awake()
    {
        playerInput = new PlayerInput();
        controller = GetComponent<CharacterController>();

        // Handle "Movement" input
        playerInput.Gameplay.Move.started += OnMovementInput;
        playerInput.Gameplay.Move.performed += OnMovementInput;
        playerInput.Gameplay.Move.canceled += OnMovementInput;

        // Handle "Statistics" input
        playerInput.Gameplay.Statistics.performed += OnStatisticsInput;
    }

    void Update()
    {
        // float horizontal = Input.GetAxis("Horizontal");
        // float vertical = Input.GetAxis("Vertical");

        // Vector3 move = transform.right * horizontal + transform.forward * vertical;
        // cameraRelativeMovement = ConvertToCameraSpace(currentMovement);
        controller.Move(currentMovement * Time.deltaTime);

        //RotationHandler();
        

        // Check if the player is on the ground
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Handle jumping
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * Physics.gravity.y);
        }

        // Apply gravity
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Update camera position and rotation
        // cameraTransform.position = transform.position + new Vector3(0f, 0.8f, 0f);
        // cameraTransform.rotation = Quaternion.Euler(cameraTransform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);

    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x * movementSpeed;
        currentMovement.z = currentMovementInput.y * movementSpeed;
        isMovementPressed = currentMovement.x != 0 || currentMovement.z != 0;
    }

    void OnStatisticsInput(InputAction.CallbackContext context)
    {
        if (isStatsShowed == false)
        {
            isStatsShowed = true;
            statsContainer.SetActive(true);
        } else
        {
            isStatsShowed = false;
            statsContainer.SetActive(false);
        }
    }

    void RotationHandler()
    {
        Vector3 positionToLookAt;

        // The changes in position Player Character should point to
        positionToLookAt.x = cameraRelativeMovement.x;
        positionToLookAt.y = 0f;
        positionToLookAt.z = cameraRelativeMovement.z;

        // The current rotation of Player Character
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            // Creates new rotation based on where the player is pressing
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);

            // Rotate the character to face the positionToLookAt
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        // Store the Y value of the original vector
        float currentYValue = vectorToRotate.y;

        // Get the Forward and Right directional vectors of the camera
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Remove the Y values to ignore upward/downward camera angles
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        // Re-normalize both vectors so they each have a magnitude of 1
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        // Rotate the X and Z vectorToRotate values to camera space
        Vector3 cameraForwardZProduct = vectorToRotate.z * cameraForward;
        Vector3 cameraRightXProduct = vectorToRotate.x * cameraRight;

        // The sum of both products is the Vector3 in camera space
        Vector3 vectorRotatedToCameraSpace = cameraForwardZProduct + cameraRightXProduct;
        vectorRotatedToCameraSpace.y = currentYValue;
        return vectorRotatedToCameraSpace;
    }

    private void OnEnable()
    {
        playerInput.Gameplay.Enable();
    }

    private void OnDisable()
    {
        playerInput.Gameplay.Disable();
    }
}
