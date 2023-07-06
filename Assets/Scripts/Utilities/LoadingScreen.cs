using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Slider progressBar;

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
