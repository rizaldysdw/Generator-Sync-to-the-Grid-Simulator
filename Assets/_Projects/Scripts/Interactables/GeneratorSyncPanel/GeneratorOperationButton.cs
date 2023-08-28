using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorOperationButton : MonoBehaviour
{
    private GTGController gtgController;

    [SerializeField] private Button startButton;
    [SerializeField] private Button stopButton;
    [SerializeField] private Text generatorStateText;
    
    // Start is called before the first frame update
    void Start()
    {
        gtgController = FindObjectOfType<GTGController>();

        startButton.onClick.AddListener(OnStartButtonClick);
        stopButton.onClick.AddListener(OnStopButtonClick);
    }

    void Update()
    {
        if (!gtgController.isRunning)
        {
            generatorStateText.text = "<color=red>Tripped</color>";
        } else
        {
            generatorStateText.text = "<color=green>Running</color>";
        }
    }

    private void OnStartButtonClick()
    {
        if (!gtgController.isRunning)
        {
            // Generator is tripped
            // Start Generator
            gtgController.isRunning = true;
            Debug.Log("GeneratorOperationButton script has changed isRunning to true!");
        } else
        {
            Debug.Log("GTG is already running!");
        }
    }

    private void OnStopButtonClick()
    {
        if (gtgController.isRunning)
        {
            // Generator is running
            // Stop Generator
            gtgController.isRunning = false;
            Debug.Log("GeneratorOperationButton script has changed isRunning to false!");
        } else
        {
            Debug.Log("GTG is already not running!");
        }
    }
}
