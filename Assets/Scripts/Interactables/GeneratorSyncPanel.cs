using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorSyncPanel : Interactable
{
    [SerializeField] private GameObject generatorSyncUI;
    private PlayerFPSCharacterController playerFPSCharacterController;
    private PlayerUIHandler playerUIHandler;
    private PlayerInteractionHandler playerInteractionHandler;
    private bool isGeneratorSyncUIActive;

    // Start is called before the first frame update
    void Start()
    {
        playerFPSCharacterController = FindObjectOfType<PlayerFPSCharacterController>();
        playerUIHandler = FindObjectOfType<PlayerUIHandler>();
        playerInteractionHandler = FindObjectOfType<PlayerInteractionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        if (!isGeneratorSyncUIActive)
        {
            generatorSyncUI.SetActive(true);
            playerInteractionHandler.enabled = false;
            playerUIHandler.UpdatePromptText(string.Empty);
            playerFPSCharacterController.playerInput.Default.Disable();
            isGeneratorSyncUIActive = true;
        }
    }
}
