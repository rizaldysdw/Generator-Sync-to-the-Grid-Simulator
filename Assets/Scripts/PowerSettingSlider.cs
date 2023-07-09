using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSettingSlider : MonoBehaviour
{
    private GridManager gridManager;
    private GeneratorSyncPanel generatorSyncPanel;

    public Slider activePowerSlider;
    public Slider reactivePowerSlider;

    public Text activePowerValueText;
    public Text reactivePowerValueText;

    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        generatorSyncPanel = FindObjectOfType<GeneratorSyncPanel>();

        activePowerValueText.text = "0";
        reactivePowerValueText.text = "0";
    }

    void Update()
    {
        if (generatorSyncPanel.isSynchronized)
        {
            activePowerSlider.onValueChanged.AddListener(OnActivePowerValueChanged);
            reactivePowerSlider.onValueChanged.AddListener(OnReactivePowerValueChanged);
        } else
        {
            activePowerSlider.onValueChanged.RemoveListener(OnActivePowerValueChanged);
            reactivePowerSlider.onValueChanged.RemoveListener(OnReactivePowerValueChanged); 
        }
    }

    private void OnActivePowerValueChanged(float value)
    {
        activePowerValueText.text = value.ToString("0.00");
        gridManager.activePowerDemand = value;
    }

    private void OnReactivePowerValueChanged(float value)
    {
        reactivePowerValueText.text = value.ToString("0.00");
        gridManager.reactivePowerDemand = value;
    }
}
