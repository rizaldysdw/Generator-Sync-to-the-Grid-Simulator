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
    public TextMeshProUGUI voltageText;
    public TextMeshProUGUI currentText;
    public TextMeshProUGUI frequency;

    // Start is called before the first frame update
    void Start()
    {
        gtgController = FindObjectOfType<GTGController>();
    }

    // Update is called once per frame
    void Update()
    {
        powerOutputText.text = "Active Power Output: " + gtgController.powerOutput + " MW";
        reactivePowerOutputText.text = "Reactive Power Output: " + gtgController.reactivePowerOutput + " MVAR";
        apparentPowerOutputText.text = "Apparent Power Output: " + gtgController.apparentPowerOutput + " MVA";
        voltageText.text = "Voltage: " + gtgController.voltage.ToString("F2") + " KV";
        currentText.text = "Current: " + gtgController.current.ToString("F2") + " KA";
        frequency.text = "Frequency: " + gtgController.frequency.ToString("F2") + " Hz";
    }
}
