using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void PlayButton(string sceneNameToLoad)
    {
        LoadingData.sceneToLoad = sceneNameToLoad;
        SceneManager.LoadScene("LoadingScreen");
    }

    public void ExitButton()
    {
        Application.Quit();
    }    
}