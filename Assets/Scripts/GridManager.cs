using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private GTGController gtgController;
    private GeneratorSyncPanel generatorSyncPanel;

    public float frequency = 50f; // in Hertz (Hz)
    public float voltage = 19.04f; // in KV
    
    [Range(15, 300)]
    [Tooltip("Active power demand in megawatts (MW)")] 
    public float activePowerDemand; // in MW
    public float activePowerDemandControl = 1f;

    [Tooltip("Reactive power demand in megavars (MVAR)")]
    public float reactivePowerDemand; // in MVAR

    void Start()
    {
        gtgController = FindObjectOfType<GTGController>();
        generatorSyncPanel = FindObjectOfType<GeneratorSyncPanel>();
    }

    void Update()
    {

    }

    public void IncreaseLoadDemand()
    {
        if (gtgController.isRunning && generatorSyncPanel.isSynchronized)
        {
            activePowerDemand += activePowerDemandControl;
        }
    }

    public void DecreaseLoadDemand()
    {
        if (gtgController.isRunning && generatorSyncPanel.isSynchronized)
        {
            activePowerDemand -= activePowerDemandControl;
        }
    }
}