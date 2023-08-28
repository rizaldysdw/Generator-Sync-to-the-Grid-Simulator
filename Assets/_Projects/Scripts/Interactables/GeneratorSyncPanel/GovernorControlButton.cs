using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GovernorControlButton : MonoBehaviour
{
    [SerializeField] private Button increaseButton;
    [SerializeField] private Button decreaseButton;

    private float increaseValue = 1f;
    private float decreaseValue = 1f;

    // Update is called once per frame
    void Update()
    {
        UpdateButtonInteractableState();   
    }

    private void UpdateButtonInteractableState()
    {
        increaseButton.interactable = !GasTurbineController.isGasTurbineRunning;
        decreaseButton.interactable = !GasTurbineController.isGasTurbineRunning;
    }

    public void IncreaseGovernorControl()
    {
        if (GasTurbineController.isGasTurbineRunning)
        {
            // Increase the governor control value by increaseValue
            GasTurbineController.governorControlSpeed += increaseValue;
        }
    }

    public void DecreaseGovernorControl()
    {
        if (GasTurbineController.isGasTurbineRunning)
        {
            // Decrease the governor control value by decreaseValue
            GasTurbineController.governorControlSpeed -= decreaseValue;
        }
    }
}
