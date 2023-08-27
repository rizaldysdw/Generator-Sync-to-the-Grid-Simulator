using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorSyncPanelUI : MonoBehaviour
{
    private GTGController gtgController;
    private GridManager gridManager;

    public Text plantFrequencyText;
    public Text plantVoltageText;
    public Text plantCurrentText;
    public Text plantActivePowerText;

    public Text gridFrequencyText;
    public Text gridVoltageText;

    // Start is called before the first frame update
    void Start()
    {
        gtgController = FindObjectOfType<GTGController>();
        gridManager = FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGeneratorSyncPanelUI();   
    }

    void UpdateGeneratorSyncPanelUI()
    {
        plantFrequencyText.text = gtgController.frequency.ToString("F2") + " Hz";
        plantVoltageText.text = gtgController.voltage.ToString("F2") + " KV";
        plantCurrentText.text = gtgController.current.ToString("F2") + " KA";
        plantActivePowerText.text = gtgController.powerOutput.ToString("F2") + " MW";

        gridFrequencyText.text = gridManager.frequency.ToString("F2") + " Hz";
        gridVoltageText.text = gridManager.voltage.ToString("F2") + " KV";
    }
}
