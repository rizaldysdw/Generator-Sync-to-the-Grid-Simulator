using UnityEngine;
using UnityEngine.UI;

public class GeneratorMainCircuitBreakerButton : MonoBehaviour
{
    public Transform needle; // Reference to the synchroscope needle GameObject
    public Button closeButton; // Reference to the "Close" button
    public Button openButton; // Reference to the "Open" button

    private float needleRotation;
    private bool isNeedleInRange;

    private void Start()
    {
        closeButton.onClick.AddListener(OnCloseButtonClick);
        openButton.onClick.AddListener(OnOpenButtonClick);
    }

    private void OnCloseButtonClick()
    {
        // Check if the needle rotation is between 11 and 12 o'clock (30 and 0 degrees)
        needleRotation = needle.rotation.eulerAngles.z;
        isNeedleInRange = needleRotation >= 330f || needleRotation <= 30f;

        if (GeneratorController.frequency > GridManager.frequency && isNeedleInRange)
        {
            GeneratorController.isGeneratorSynchronized = true;
            Debug.Log("Generator synchronized successfully!");
        }
        else
        {
            GasTurbineController.isGasTurbineRunning = false;
            Debug.Log("Synchronization failed: Needle is not in the safe range. Gas Turbine Tripped!");
        }
    }

    private void OnOpenButtonClick()
    {
        if (GeneratorController.realPowerOutput <= 0f &&
        GeneratorController.reactivePowerOutput <= 0f &&
        GeneratorController.frequency <= GridManager.frequency)
        {
            GeneratorController.isGeneratorSynchronized = false;
            Debug.Log("Generator disconnected from the grid!");
        }
    }
}
