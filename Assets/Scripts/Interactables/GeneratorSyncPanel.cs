using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorSyncPanel : Interactable
{
    [Header("References from Player-related Scripts")]
    private PlayerFPSCharacterController playerFPSCharacterController;
    private PlayerInput playerInput;
    private PlayerInteractionHandler playerInteractionHandler;
    private PlayerUIHandler playerUIHandler;
    private PauseMenu pauseMenu;

    [Header("References from Generator and Grid Scripts")]
    private GTGController gtgController;

    [Header("References for Generator Sync Panel")]
    [SerializeField] private GameObject generatorSyncUI;
    public bool isGeneratorSyncUIActive;
    public bool isSynchronized;
    public Text plantFrequencyText;
    public Text plantVoltageText;
    public Text plantCurrentText;

    // Start is called before the first frame update
    void Start()
    {
        playerFPSCharacterController = FindObjectOfType<PlayerFPSCharacterController>();
        playerUIHandler = FindObjectOfType<PlayerUIHandler>();
        playerInteractionHandler = FindObjectOfType<PlayerInteractionHandler>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        gtgController = FindObjectOfType<GTGController>();

        playerInput = playerFPSCharacterController.playerInput;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.isGamePaused && isGeneratorSyncUIActive && playerInput.UI.Pause.triggered)
        {
            ExitGeneratorSyncPanelUI();
        }

        UpdateGeneratorSyncPanelUI();
    }

    protected override void Interact()
    {
        if (!pauseMenu.isGamePaused && !isGeneratorSyncUIActive)
        {
            // Set bool to true
            isGeneratorSyncUIActive = true;

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

    private void UpdateGeneratorSyncPanelUI()
    {
        float generatorFrequency = gtgController.frequency;
        float generatorVoltage = gtgController.voltage;
        float generatorCurrent = gtgController.current;

        plantFrequencyText.text =  generatorFrequency.ToString("F2") + " Hz";
        plantVoltageText.text = generatorVoltage.ToString("F2") + " KV";
        plantCurrentText.text = generatorCurrent.ToString("F2") + " KA";
    }
}
