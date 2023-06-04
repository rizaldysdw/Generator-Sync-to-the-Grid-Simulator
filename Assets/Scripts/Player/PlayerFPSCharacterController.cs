using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFPSCharacterController : MonoBehaviour
{
    [Header("GameObject & Component References")]
    private CharacterController characterController;
    private PlayerInput playerInput;
    private Animator playerAnimator;
    private Camera playerCamera;
    public GameObject statsContainer;

    [Header("Movement Variables")]
    private Vector2 currentMovementInput;
    private Vector2 mouseInput;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private bool isMovementPressed;
    private bool isRunPressed;

    [Header("Jump Variables")]
    private bool isJumpPressed = false;

    [Header("Constants")]
    private float walkingSpeed = 3;
    private float runningSpeed = 8f;
    private float gravity = 9.8f;
    private float lookSpeed = 0.2f;
    private float lookXLimit = 45.0f;
    private float jumpHeight = 4f;

    [Header("Animation Optimization")]
    private int isWalkingHash;
    private int isRunningHash;


    [HideInInspector] public bool canMove = true;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();
        playerCamera = Camera.main;

        // Handle "Movement" input
        playerInput.Default.Move.started += OnMovementInput;
        playerInput.Default.Move.performed += OnMovementInput;
        playerInput.Default.Move.canceled += OnMovementInput;

        // Handle "Run" input
        playerInput.Default.Run.started += OnRunInput;
        playerInput.Default.Run.canceled += OnRunInput;

        // Handle "Jump" input
        playerInput.Default.Jump.started += OnJumpInput;
        playerInput.Default.Jump.canceled += OnJumpInput;

        // Handle "Statistics" input
        playerInput.Default.Statistics.performed += OnStatisticsInput;

        // Animator Controller optimization
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    void Start()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = canMove ? (isRunPressed ? runningSpeed : walkingSpeed) * currentMovementInput.y : 0;
        float curSpeedY = canMove ? (isRunPressed ? runningSpeed : walkingSpeed) * currentMovementInput.x : 0;
        float movementDirectionY = moveDirection.y;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (isJumpPressed && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpHeight;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        /* Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        as an acceleration (ms^-2) */

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        AnimationHandler();
        RotationHandler();
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        moveDirection.x = currentMovementInput.x;
        moveDirection.z = currentMovementInput.y;
        isMovementPressed = moveDirection.x != 0 || moveDirection.z != 0;
    }

    void OnRunInput(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

     void OnJumpInput(InputAction.CallbackContext context)
     {
        isJumpPressed = context.ReadValueAsButton();
     }

    void OnStatisticsInput(InputAction.CallbackContext context)
    {

    }

    void AnimationHandler()
    {
        // Get parameter values from Animator
        bool isWalking = playerAnimator.GetBool(isWalkingHash);
        bool isRunning = playerAnimator.GetBool(isRunningHash);

        // Start walking if move isMovementPressed is true and not already walking
        if (isMovementPressed && !isWalking)
        {
            playerAnimator.SetBool(isWalkingHash, true);
        }
        else if (!isMovementPressed && isWalking)
        {
            playerAnimator.SetBool(isWalkingHash, false);
        }

        // Start running if 
        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            playerAnimator.SetBool(isRunningHash, true);
        }
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            playerAnimator.SetBool(isRunningHash, false);
        }
    }

    void RotationHandler()
    {
        if (canMove)
        {
            mouseInput = Mouse.current.delta.ReadValue();
            float mouseX = mouseInput.x;
            float mouseY = mouseInput.y;

            rotationX += -mouseY * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, mouseX * lookSpeed, 0);
        }
    }

    void OnEnable()
    {
        playerInput.Default.Enable();
    }

    void OnDisable()
    {
        playerInput.Default.Disable();
    }
}
