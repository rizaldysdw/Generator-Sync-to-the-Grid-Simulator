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

        UpdateUIInteractivity();

        activePowerValueText.text = "0";
        reactivePowerValueText.text = "0";

        activePowerSlider.onValueChanged.AddListener(OnActivePowerValueChanged);
        reactivePowerSlider.onValueChanged.AddListener(OnReactivePowerValueChanged);
    }

    private void UpdateUIInteractivity()
    {
        if (generatorSyncPanel != null && generatorSyncPanel.isSynchronized)
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
        gridManager.activePowerDemand = value;
    }

    private void OnReactivePowerValueChanged(float value)
    {
        reactivePowerValueText.text = value.ToString("0.00");
        gridManager.reactivePowerDemand = value;
    }

    private void Update()
    {
        if (generatorSyncPanel != null && generatorSyncPanel.isSynchronized)
        {
            UpdateUIInteractivity();
        }
    }
}
