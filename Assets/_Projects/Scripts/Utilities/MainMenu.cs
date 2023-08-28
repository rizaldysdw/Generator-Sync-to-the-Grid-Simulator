using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    // TextMeshPro Input Fields
    [SerializeField] private TMP_InputField turbineRatedSpeedInputField;
    [SerializeField] private TMP_InputField generatorPolesInputField;
    [SerializeField] private TMP_InputField generatorRatedVoltageInputField;
    [SerializeField] private TMP_InputField generatorRatedCurrentInputField;
    [SerializeField] private TMP_InputField generatorMELInputField;
    [SerializeField] private TMP_InputField generatorOELInputField;


    // TextMeshPro Text
    [SerializeField] private TextMeshProUGUI turbineRatedSpeedText;
    [SerializeField] private TextMeshProUGUI generatorPolesText;
    [SerializeField] private TextMeshProUGUI generatorRatedVoltageText;
    [SerializeField] private TextMeshProUGUI generatorRatedCurrentText;
    [SerializeField] private TextMeshProUGUI generatorMELText;
    [SerializeField] private TextMeshProUGUI generatorOELText;

    // Variables to Store Values
    private float turbineRatedSpeed;
    private float generatorPoles;
    private float generatorRatedVoltage;
    private float generatorRatedCurrent;
    private float generatorMEL;
    private float generatorOEL;


    // Default Values
    private const float defaultTurbineRatedSpeed = 3000f; // RPM
    private const float defaultGeneratorPoles = 2f;
    private const float defaultGeneratorRatedVoltage = 19f; // KV
    private const float defaultGeneratorRatedCurrent = 11.243f; // KA
    private const float defaultGeneratorMEL = -115.5f; // Minimum Excitation Limit, MVAR
    private const float defaultGeneratorOEL = 285.5f; // Over Excitation Limit, MVAR 

    // Start is called before the first frame update
    void Start()
    {
        ResetToDefaultSettings();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUIText();
    }

    private void UpdateUIText()
    {
        turbineRatedSpeedText.text = turbineRatedSpeed.ToString();
        generatorPolesText.text = generatorPoles.ToString();
        generatorRatedVoltageText.text = generatorRatedVoltage.ToString();
        generatorRatedCurrentText.text = generatorRatedCurrent.ToString();
        generatorMELText.text = generatorMEL.ToString();
        generatorOELText.text = generatorOEL.ToString();
    }

    private void ResetToDefaultSettings()
    {
        turbineRatedSpeed = defaultTurbineRatedSpeed;
        generatorPoles = defaultGeneratorPoles;
        generatorRatedVoltage = defaultGeneratorRatedVoltage;
        generatorRatedCurrent = defaultGeneratorRatedCurrent;
        generatorMEL = defaultGeneratorMEL;
        generatorOEL = defaultGeneratorOEL;
    }

    public void ResetToDefaultSettingsButton()
    {
        turbineRatedSpeed = defaultTurbineRatedSpeed;
        generatorPoles = defaultGeneratorPoles;
        generatorRatedVoltage = defaultGeneratorRatedVoltage;
        generatorRatedCurrent = defaultGeneratorRatedCurrent;
        generatorMEL = defaultGeneratorMEL;
        generatorOEL = defaultGeneratorOEL;

        LoadingData.turbineRatedSpeed = turbineRatedSpeed;
        LoadingData.generatorPoles = generatorPoles;
        LoadingData.generatorRatedVoltage = generatorRatedVoltage;
        LoadingData.generatorRatedCurrent = generatorRatedCurrent;
        LoadingData.generatorMEL = generatorMEL;
        LoadingData.generatorOEL = generatorOEL;
    }

    public void ApplySettingsButton()
    {
        if (float.TryParse(turbineRatedSpeedInputField.text, out float newTurbineRatedSpeed))
        {
            turbineRatedSpeed = newTurbineRatedSpeed;
            turbineRatedSpeedInputField.text = "";
            
            LoadingData.turbineRatedSpeed = turbineRatedSpeed;
        }

        if (int.TryParse(generatorPolesInputField.text, out int newGeneratorPoles))
        {
            generatorPoles = (float)newGeneratorPoles;
            generatorPolesInputField.text = "";

            LoadingData.generatorPoles = generatorPoles;
        }

        if (float.TryParse(generatorRatedVoltageInputField.text, out float newGeneratorRatedVoltage))
        {
            generatorRatedVoltage = newGeneratorRatedVoltage;
            generatorRatedVoltageInputField.text = "";

            LoadingData.generatorRatedVoltage = generatorRatedVoltage;
        }

        if (float.TryParse(generatorRatedCurrentInputField.text, out float newGeneratorRatedCurrent))
        {
            generatorRatedCurrent = newGeneratorRatedCurrent;
            generatorRatedCurrentInputField.text = "";

            LoadingData.generatorRatedCurrent = generatorRatedCurrent;
        }

        if (float.TryParse(generatorMELInputField.text, out float newGeneratorMEL))
        {
            generatorMEL = -newGeneratorMEL;
            generatorMELInputField.text = "";

            LoadingData.generatorMEL = generatorMEL;
        }

        if (float.TryParse(generatorOELInputField.text, out float newGeneratorOEL))
        {
            generatorOEL = newGeneratorOEL;
            generatorOELInputField.text = "";

            LoadingData.generatorOEL = generatorOEL;
        }
    }
}
