using UnityEngine;
using UnityEngine.UI;

public class GeneratorMainCircuitBreakerButton : MonoBehaviour
{
    public Transform needle; // Reference to the synchroscope needle GameObject
    public Button closeButton; // Reference to the "Close" button

    private float needleRotation;
    private bool isNeedleInRange;

    private void Start()
    {
        closeButton.onClick.AddListener(OnCloseButtonClick);
<<<<<<< HEAD
        openButton.onClick.AddListener(OnOpenButtonClick);
=======
        gtgController = FindObjectOfType<GTGController>();
        gridManager = FindObjectOfType<GridManager>();
        generatorSyncPanel = FindObjectOfType<GeneratorSyncPanel>();
>>>>>>> parent of dc44b79 (Final Version Thesis)
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
<<<<<<< HEAD
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
=======
            Debug.Log("Synchronization failed: Needle is not in the safe range.");
        }
    } 
>>>>>>> parent of dc44b79 (Final Version Thesis)
}
