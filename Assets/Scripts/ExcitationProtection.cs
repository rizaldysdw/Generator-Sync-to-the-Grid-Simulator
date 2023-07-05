using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcitationProtection : MonoBehaviour
{
    private GTGController gtgController;
    private GeneratorSyncPanel generatorSyncPanel;

    private float overexcitationThreshold = 0.85f;
    private float underexcitationThreshold = 0.95f;

    // Start is called before the first frame update
    void Start()
    {
        gtgController = GetComponent<GTGController>();
        generatorSyncPanel = GetComponent<GeneratorSyncPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (generatorSyncPanel.isSynchronized)
        {
            if (gtgController.generatorPowerFactor > overexcitationThreshold)
            {
                // Overexcitation protection here
                Debug.Log("Generator is overexcited! Triggering protection mechanism...");
            } else if (gtgController.generatorPowerFactor < underexcitationThreshold)
            {
                // Underoverexcitation protection here
                Debug.Log("Generator is underexcited! Triggering protection mechanism...");
            }
        }
    }
}
