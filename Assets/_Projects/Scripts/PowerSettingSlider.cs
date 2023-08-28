using UnityEngine;
using UnityEngine.UI;

public class PowerSettingSlider : MonoBehaviour
{
    public Slider activePowerSlider;
    public Slider reactivePowerSlider;

    public Text activePowerValueText;
    public Text reactivePowerValueText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUIInteractivity();

        activePowerValueText.text = "0";
        reactivePowerValueText.text = "0";

        activePowerSlider.onValueChanged.AddListener(OnActivePowerValueChanged);
        reactivePowerSlider.onValueChanged.AddListener(OnReactivePowerValueChanged);
    }

    void Update()
    {
        UpdateUIInteractivity();
    }

    private void UpdateUIInteractivity()
    {
        if (GeneratorController.isGeneratorSynchronized)
        {
            activePowerSlider.interactable = true;
            reactivePowerSlider.interactable = true;
        }
        else
        {
            activePowerSlider.interactable = false;
            reactivePowerSlider.interactable = false;
        }
    }

    private void OnActivePowerValueChanged(float value)
    {
        activePowerValueText.text = value.ToString("0.00");
        GridManager.realPowerDemand = value;
    }

    private void OnReactivePowerValueChanged(float value)
    {
        reactivePowerValueText.text = value.ToString("0.00");
        GridManager.reactivePowerDemand = value;
    }
}
