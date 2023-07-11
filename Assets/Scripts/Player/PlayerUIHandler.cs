using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIHandler : MonoBehaviour
{
    private PlayerFPSCharacterController playerFPSCharacterController;

    [SerializeField] private GameObject statsContainer;
    [SerializeField] private GameObject consoleCanvas;
    public TextMeshProUGUI promptText;
    private bool isStatsShowed;
    private bool isConsoleActive;

    private void Start()
    {
        playerFPSCharacterController = GetComponent<PlayerFPSCharacterController>();    
    }

    public void ToggleStatsContainer()
    {
        if (!isStatsShowed)
        {
            isStatsShowed = true;
            statsContainer.SetActive(true);
        }
        else
        {
            isStatsShowed = false;
            statsContainer.SetActive(false);
        }
    }

    public void ToggleConsoleCanvas()
    {
        isConsoleActive = !isConsoleActive;

        if (isConsoleActive)
        {
            consoleCanvas.SetActive(true);
            playerFPSCharacterController.playerInput.Gameplay.Disable();
            playerFPSCharacterController.playerInput.Gameplay.Console.Enable();
            playerFPSCharacterController.playerInput.UI.Enable();
            playerFPSCharacterController.UnlockCursor();
            
        } else
        {
            consoleCanvas.SetActive(false);
            playerFPSCharacterController.playerInput.Gameplay.Enable();
            playerFPSCharacterController.playerInput.UI.Disable();
            playerFPSCharacterController.LockCursor();
        }
    }

    public void UpdatePromptText(string promptMessage)
    {
        promptText.text = promptMessage;
    }
}
