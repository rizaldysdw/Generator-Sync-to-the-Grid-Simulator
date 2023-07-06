using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorSyncPanel : Interactable
{
    [Header("References from Player-related Scripts")]
    private PlayerFPSCharacterController playerFPSCharacterController;
    private PlayerInput playerInput;
    private PlayerInteractionHandler playerInteractionHandler;
    private PlayerUIHandler playerUIHandler;
    private PauseMenu pauseMenu;
    [SerializeField] private GameObject playerCrosshair;

    [Header("References from Generator and Grid Scripts")]
    private GTGController GTGController;
    private PlantManager plantManager;

    [Header("References for Generator Sync Panel")]
    [SerializeField] private GameObject generatorSyncUI;
    public bool isGeneratorSyncUIActive;
    public bool isSynchronized;

    // Start is called before the first frame update
    void Start()
    {
        playerFPSCharacterController = FindObjectOfType<PlayerFPSCharacterController>();
        playerUIHandler = FindObjectOfType<PlayerUIHandler>();
        playerInteractionHandler = FindObjectOfType<PlayerInteractionHandler>();
        pauseMenu = FindObjectOfType<PauseMenu>();

        playerInput = playerFPSCharacterController.playerInput;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.isGamePaused && isGeneratorSyncUIActive && playerInput.UI.Pause.triggered)
        {
            ExitGeneratorSyncPanelUI();
        }
    }

    protected override void Interact()
    {
        if (!pauseMenu.isGamePaused && !isGeneratorSyncUIActive)
        {
            // Set bool to true
            isGeneratorSyncUIActive = true;

            // Deactivaate Player Crosshair UI
            playerCrosshair.SetActive(false);

            // Activate Generator Sync UI
            generatorSyncUI.SetActive(true);

            // Disable PlayerInteractionHandler script
            playerInteractionHandler.enabled = false;

            // Update Prompt Text to empty
            playerUIHandler.UpdatePromptText(string.Empty);

            // Disable Gameplay Action Map
            playerFPSCharacterController.playerInput.Gameplay.Disable();

            // Enable UI Action Map
            playerFPSCharacterController.playerInput.UI.Enable();

            // Unlock cursor
            playerFPSCharacterController.UnlockCursor();
        }
    }

    public void ExitGeneratorSyncPanelUI()
    {
            // Set bool to false
            isGeneratorSyncUIActive = false;

            // Activate Player Crosshair UI
            playerCrosshair.SetActive(true);

            // Deactivate Generator Sync UI
            generatorSyncUI.SetActive(false);

            // Enable PlayerInteractionHandler script
            playerInteractionHandler.enabled = true;

            // Disable Gameplay Action Map
            playerFPSCharacterController.playerInput.Gameplay.Enable();

            // Enable UI Action Map
            playerFPSCharacterController.playerInput.UI.Disable();
            
            // Lock cursor
            playerFPSCharacterController.LockCursor();
    }

    public void ToggleGeneratorCircuitBreaker()
    {
        isSynchronized = !isSynchronized;
    }
}
