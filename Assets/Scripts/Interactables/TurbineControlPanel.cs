using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineControlPanel : Interactable
{
    private GTGController gtgController;

    // Start is called before the first frame update
    void Start()
    {
        gtgController = FindObjectOfType<GTGController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gtgController.isRunning)
        {
            promptMessage = "Press E to stop the turbine";
        } else
        {
            promptMessage = "Press E to start the turbine";
        }
    }

    protected override void Interact()
    {
        gtgController.isRunning = !gtgController.isRunning;
    }
}
