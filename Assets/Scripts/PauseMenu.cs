using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isGamePaused;

    public GameObject pauseMenu;
    private PlayerFPSCharacterController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerFPSCharacterController>();
    }

    void Update()
    {
        if (!isGamePaused && playerController.playerInput.Gameplay.Pause.triggered)
        {
            PauseGame();
        } else if (isGamePaused && playerController.playerInput.Gameplay.Pause.triggered)
        {
            ResumeButton();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;

        playerController.playerInput.Gameplay.Disable();
        playerController.playerInput.UI.Enable();
        playerController.UnlockCursor();
    }

    public void ResumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;

        playerController.playerInput.Gameplay.Enable();
        playerController.playerInput.UI.Disable();
        playerController.LockCursor();
    }

    public void ExitToMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        isGamePaused = false;
    }
}
