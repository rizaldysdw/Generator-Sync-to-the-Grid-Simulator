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
        plantFrequencyText.text = gtgController.frequency + " Hz";
        plantVoltageText.text = gtgController.voltage + " KV";
        plantCurrentText.text = gtgController.current + " KA";
    }
}
