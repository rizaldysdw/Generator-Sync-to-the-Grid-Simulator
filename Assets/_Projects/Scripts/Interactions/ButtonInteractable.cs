using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonInteractable : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject turbineFan;
    [SerializeField] private Rotator turbineRotator;

    private bool isRotating;

    // Start is called before the first frame update
    void Start()
    {
        turbineRotator = turbineFan.gameObject.GetComponent<Rotator>();
    }

    private void StartRotate()
    {
        turbineRotator.enabled = true;
    }

    private void StopRotate()
    {
        turbineRotator.enabled = false;
    }

    private void ToggleRotate()
    {
        isRotating = !isRotating;
        if (isRotating)
        {
            StartRotate();
        } else
        {
            StopRotate();
        }
    }

    public void PushButton()
    {
        ToggleRotate();
    }
}
