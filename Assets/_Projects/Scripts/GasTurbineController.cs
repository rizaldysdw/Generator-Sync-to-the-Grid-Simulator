using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTurbineController : MonoBehaviour
{
    private AudioSource audioSource;

    private GTGController controller;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = FindObjectOfType<GTGController>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAudioState();
    }

    void SetAudioState()
    {
        if (controller.isRunning)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
