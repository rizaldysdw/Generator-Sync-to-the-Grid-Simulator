using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParameterDisplay : MonoBehaviour
{
    private GTGController gtgController;

    public Text plantFrequencyText;
    public Text plantVoltageText;
    public Text plantCurrentText;

    // Start is called before the first frame update
    void Start()
    {
        gtgController = FindObjectOfType<GTGController>();
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD:Assets/_Projects/Scripts/Interactables/GeneratorSyncPanel/GeneratorSyncPanelUI.cs
        UpdateGeneratorSyncPanelUI();   
    }

    void UpdateGeneratorSyncPanelUI()
    {
        plantFrequencyText.text = GeneratorController.frequency.ToString("F2") + " Hz";
        plantVoltageText.text = GeneratorController.voltage.ToString("F2") + " KV";
        plantCurrentText.text = GeneratorController.current.ToString("F2") + " KA";
        plantActivePowerText.text = GeneratorController.realPowerOutput.ToString("F2") + " MW";

        gridFrequencyText.text = GridManager.frequency.ToString("F2") + " Hz";
        gridVoltageText.text = GridManager.voltage.ToString("F2") + " KV";
=======
        plantFrequencyText.text = gtgController.frequency.ToString("F2") + " Hz";
        plantVoltageText.text = gtgController.voltage.ToString("F2") + " KV";
        plantCurrentText.text = gtgController.current.ToString("F2") + " KA";
>>>>>>> parent of dc44b79 (Final Version Thesis):Assets/_Projects/Scripts/Interactables/GeneratorSyncPanel/ParameterDisplay.cs
    }
}
