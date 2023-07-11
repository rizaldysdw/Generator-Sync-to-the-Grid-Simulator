using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatiscticsDisplay : MonoBehaviour
{
    private GTGController gtgController;

    public TextMeshProUGUI powerOutputText;
    public TextMeshProUGUI reactivePowerOutputText;
    public TextMeshProUGUI apparentPowerOutputText;
    public TextMeshProUGUI generatorPowerFactorText;
    public TextMeshProUGUI voltageText;
    public TextMeshProUGUI currentText;
    public TextMeshProUGUI frequency;

    // Start is called before the first frame update
    void Start()
    {
        InitializeStatsDisplay();
    }

    void Update()
    {
        UpdateStatsDisplay();    
    }

    private void InitializeStatsDisplay()
    {
        gtgController = FindObjectOfType<GTGController>();
    }

    private void UpdateStatsDisplay()
    {
        powerOutputText.text = "Active Power Output: " + gtgController.powerOutput + " MW";
        reactivePowerOutputText.text = "Reactive Power Output: " + gtgController.reactivePowerOutput + " MVAR";
        apparentPowerOutputText.text = "Apparent Power Output: " + gtgController.apparentPowerOutput + " MVA";
        generatorPowerFactorText.text = "Power Factor: " + gtgController.generatorPowerFactor;
        voltageText.text = "Voltage: " + gtgController.voltage.ToString("F2") + " KV";
        currentText.text = "Current: " + gtgController.current.ToString("F2") + " KA";
        frequency.text = "Frequency: " + gtgController.frequency.ToString("F2") + " Hz";
    }
}
