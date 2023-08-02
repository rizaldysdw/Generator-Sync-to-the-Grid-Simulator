using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GovernorControlButton : MonoBehaviour
{
    private GTGController gtgController;
    public Button increaseButton;
    public Button decreaseButton;

    private float increaseValue = 0.25f;
    private float decreaseValue = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        gtgController = FindObjectOfType<GTGController>();
    }

    // Update is called once per frame
    void Update()
    {
        increaseButton.interactable = gtgController.isRunning;
        decreaseButton.interactable = gtgController.isRunning;
    }

    public void IncreaseGovernorControl()
    {
        if (gtgController.isRunning)
        {
            // Increase the governor control value by increaseValue
            gtgController.governorControlSpeed += increaseValue;
        }
    }

    public void DecreaseGovernorControl()
    {
        if (gtgController.isRunning)
        {
            // Decrease the governor control value by decreaseValue
            gtgController.governorControlSpeed -= decreaseValue;
        }
    }
}
