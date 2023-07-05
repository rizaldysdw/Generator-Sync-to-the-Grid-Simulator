using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltageCurrentSineWave : MonoBehaviour
{
    private GTGController gtgController;

    public LineRenderer voltageLineRenderer;
    public LineRenderer currentLineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        gtgController = FindObjectOfType<GTGController>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;

        // Get the voltage and current values from the GTGController
        float voltage = gtgController.voltage;
        float current = gtgController.current;

        // Set the positions of the LineRenderer components
        voltageLineRenderer.SetPosition(0, new Vector3(time, voltage, 0f));
        currentLineRenderer.SetPosition(0, new Vector3(time, current, 0f));
    }
}
