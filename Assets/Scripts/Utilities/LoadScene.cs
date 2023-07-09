using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Slider progressBar;

    // Start is called before the first frame update
    void Start()
    {
        LoadingData.loadingOperation = SceneManager.LoadSceneAsync(LoadingData.sceneToLoad);

        // Prevent sceneToLoad activation
        LoadingData.loadingOperation.allowSceneActivation = false;
    }

    void Update()
    {
        if (LoadingData.loadingOperation != null)
        {
            progressBar.value = Mathf.Clamp01(LoadingData.loadingOperation.progress / 0.9f);

            if (LoadingData.loadingOperation.progress >= 0.9f)
            {
                // Activate the sceneToLoad
                LoadingData.loadingOperation.allowSceneActivation = true;
                LoadingData.loadingOperation = null;
            }
        }
    }
}
