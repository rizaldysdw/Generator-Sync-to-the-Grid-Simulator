using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [Header("Script References")]
    private GTGController gtgController;
    private GeneratorSyncPanel generatorSyncPanel;

    [Header("UI Text References")]
    public Text frequencyValueText;
    public Text activePowerValueText;
    public Text reactivePowerValueText;


    [Space(5f)]
    [Tooltip("Frequency of the grid in hertz (Hz)")]
    public static float frequency = 50f;
    private float frequencyControl = 0.1f;
    


    public static float voltage = 19.04f; // in KV
    
    [Range(0, 300)]
    [Tooltip("Active power demand in megawatts (MW)")] 
    public static float realPowerDemand;
    private float realPowerDemandControl = 1f;
    

    [Tooltip("Reactive power demand in megavars (MVAR)")]
    public static float reactivePowerDemand;
    private float reactivePowerDemandControl = 1f;
    


    void Start()
    {
        gtgController = FindObjectOfType<GTGController>();
        generatorSyncPanel = FindObjectOfType<GeneratorSyncPanel>();

        activePowerValueText.text = "0 MW";
        reactivePowerValueText.text = "0 MVAR";
    }

    void Update()
    {
        UpdateUI();
    }

    public void IncreaseLoadDemand()
    {
        if (gtgController.isRunning && generatorSyncPanel.isSynchronized)
        {
            realPowerDemand += realPowerDemandControl;
        }
    }

    public void DecreaseLoadDemand()
    {
        if (gtgController.isRunning && generatorSyncPanel.isSynchronized)
        {
            realPowerDemand -= realPowerDemandControl;
        }
    }

    public void IncreaseReactivePowerDemand()
    {
        if (gtgController.isRunning && generatorSyncPanel.isSynchronized)
        {
            reactivePowerDemand += reactivePowerDemandControl;
        }
    }

    public void DecreaseReactivePowerDemand()
    {
        if (gtgController.isRunning && generatorSyncPanel.isSynchronized)
        {
            reactivePowerDemand -= reactivePowerDemandControl;
        }
    }

    public void IncreaseGridFrequency()
    {
        frequency += frequencyControl;
    }

    public void DecreaseGridFrequency()
    {
        frequency -= frequencyControl;
    }

    public void UpdateUI()
    {
        activePowerValueText.text = realPowerDemand.ToString("0.00") + " MW";
        reactivePowerValueText.text = reactivePowerDemand.ToString("0.00") + " MVAR";
        frequencyValueText.text = frequency.ToString("0.00") + " Hz";
    }
}