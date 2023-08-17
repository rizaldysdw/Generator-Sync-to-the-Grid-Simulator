using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isGamePaused;
    private bool isOnSettingsPage;

    private string sceneNameToLoad;

    public GameObject pauseMenu;
    public GameObject settingsPage;

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

        if (isOnSettingsPage && playerController.playerInput.Gameplay.Pause.triggered)
        {
            ExitSettingsPage();
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

    public void ExitToMainMenuButton(string sceneNameToLoad)
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        LoadingData.sceneToLoad = sceneNameToLoad;
        SceneManager.LoadScene("LoadingScreen");
    }

    public void EnterSettingsPage()
    {
        pauseMenu.SetActive(false);
        settingsPage.SetActive(true);

        isOnSettingsPage = true;
    }

    public void ExitSettingsPage()
    {
        pauseMenu.SetActive(true);
        settingsPage.SetActive(false);

        isOnSettingsPage = false;
    }
}
