using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadingData.loadingOperation = SceneManager.LoadSceneAsync(LoadingData.sceneToLoad);

        // Prevent sceneToLoad activation
        LoadingData.loadingOperation.allowSceneActivation = false;
    }
}
