using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFPSCharacterController : MonoBehaviour
{
    [Header("GameObject & Component References")]
    private CharacterController characterController;
    public PlayerInput playerInput;
    private PlayerUIHandler playerUIHandler;
    private GTGController gtgController;
    private GeneratorSyncPanel generatorSyncPanel;
    private PauseMenu pauseMenu;
    private Animator playerAnimator;
    private Camera playerCamera;
    public GameObject playerCrosshair;

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
        playerUIHandler = GetComponent<PlayerUIHandler>();
        gtgController = FindObjectOfType<GTGController>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        generatorSyncPanel = FindObjectOfType<GeneratorSyncPanel>();

        // Handle "Movement" input
        playerInput.Gameplay.Move.started += OnMovementInput;
        playerInput.Gameplay.Move.performed += OnMovementInput;
        playerInput.Gameplay.Move.canceled += OnMovementInput;

        // Handle "Run" input
        playerInput.Gameplay.Run.started += OnRunInput;
        playerInput.Gameplay.Run.canceled += OnRunInput;

        // Handle "Jump" input
        playerInput.Gameplay.Jump.started += OnJumpInput;
        playerInput.Gameplay.Jump.canceled += OnJumpInput;

        // Handle "Statistics" input
        playerInput.Gameplay.Statistics.performed += OnStatisticsInput;

        // Handle "Pause" input
        // playerInput.Gameplay.Pause.performed += OnPauseInput;

        // Handle "Interact" input
        // playerInput.Gameplay.Interact.performed += OnInteractInput;

        // Handle "Console" input
        playerInput.Gameplay.Console.performed += OnConsoleInput;

        // Animator Controller optimization
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    void Start()
    {
        LockCursor();
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
    }

    void LateUpdate()
    {
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
        playerUIHandler.ToggleStatsContainer();
    }

    void OnInteractInput(InputAction.CallbackContext context)
    {
        // This method is used to call Interact input from this script
    }

    void OnConsoleInput(InputAction.CallbackContext context)
    {
        playerUIHandler.ToggleConsoleCanvas();
    }

    void OnPauseInput(InputAction.CallbackContext context)
    {
        bool isPausePressed = context.ReadValueAsButton();

        if (isPausePressed && !generatorSyncPanel.isGeneratorSyncUIActive)
        {
            pauseMenu.PauseGame();
        }

        if (isPausePressed && !generatorSyncPanel.isGeneratorSyncUIActive)
        {
            pauseMenu.ResumeButton();
        }
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
            mouseInput = playerInput.Gameplay.Look.ReadValue<Vector2>();
            float mouseX = mouseInput.x;
            float mouseY = mouseInput.y;

            rotationX += -mouseY * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, mouseX * lookSpeed, 0);
        }
    }

    public void ExitInteractionWithGeneratorSyncPanel()
    {
        generatorSyncPanel.ExitGeneratorSyncPanelUI();
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCrosshair.SetActive(true);
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerCrosshair.SetActive(false);
    }

    void OnEnable()
    {
        playerInput.Gameplay.Enable();
        playerInput.UI.Enable();
    }

    void OnDisable()
    {
        playerInput.Gameplay.Disable();
        playerInput.UI.Disable();
    }
}
