using UnityEngine;
using UnityEngine.UI;

public class GeneratorMainCircuitBreakerButton : MonoBehaviour
{
    private GeneratorSyncPanel generatorSyncPanel; // Reference to the GeneratorSyncPanel script
    private GridManager gridManager; // Reference to the GridManager script
    private GTGController gtgController; // Reference to the GTGController script

    public Transform needle; // Reference to the synchroscope needle GameObject
    public Button closeButton; // Reference to the "Close" button

    private float needleRotation;
    private bool isNeedleInRange;

    private void Start()
    {
        closeButton.onClick.AddListener(OnCloseButtonClick);
        gtgController = FindObjectOfType<GTGController>();
        gridManager = FindObjectOfType<GridManager>();
        generatorSyncPanel = FindObjectOfType<GeneratorSyncPanel>();
    }

    private void OnCloseButtonClick()
    {
        // Check if the needle rotation is between 11 and 12 o'clock (30 and 0 degrees)
        needleRotation = needle.rotation.eulerAngles.z;
        isNeedleInRange = needleRotation >= 330f || needleRotation <= 30f;

        if (gtgController.frequency > gridManager.frequency && isNeedleInRange)
        {
            generatorSyncPanel.isSynchronized = true;
            Debug.Log("Generator synchronized successfully!");
        }
        else
        {
            Debug.Log("Synchronization failed: Needle is not in the safe range.");
        }
    } 
}
