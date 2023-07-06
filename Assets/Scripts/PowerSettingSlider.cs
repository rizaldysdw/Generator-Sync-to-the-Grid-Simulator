using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSettingSlider : MonoBehaviour
{
    private GridManager gridManager;

    public Slider activePowerSlider;
    public Slider reactivePowerSlider;

    public Text activePowerValueText;
    public Text reactivePowerValueText;

    // Start is called before the first frame update
    void Start()
    {
        activePowerSlider.onValueChanged.AddListener(OnActivePowerValueChanged);
        reactivePowerSlider.onValueChanged.AddListener(OnReactivePowerValueChanged);

        gridManager = FindObjectOfType<GridManager>();

        activePowerValueText.text = "15";
        reactivePowerValueText.text = "0";
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
