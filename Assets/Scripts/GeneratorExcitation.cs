using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorExcitation : MonoBehaviour
{
    private GTGController gtgController;
    public Button increaseButton;
    public Button decreaseButton;

    private float valueAdjustment = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        gtgController = FindObjectOfType<GTGController>();
    }

    // Update is called once per frame
    void Update()
    {
        increaseButton.interactable = GTGController.isExcitationConnected;
        decreaseButton.interactable = GTGController.isExcitationConnected;    
    }

    public void IncreaseExcitationVoltage()
    {
        if (gtgController.isRunning && GTGController.isExcitationConnected)
        {
            gtgController.excitationVoltage += valueAdjustment;
        }
    }

    public void DecreaseExcitationVoltage()
    {
        if (gtgController.isRunning && GTGController.isExcitationConnected)
        {
            gtgController.excitationVoltage -= valueAdjustment;
        }
    }
}
