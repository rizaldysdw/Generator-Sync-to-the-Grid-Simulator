using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GTGController : MonoBehaviour
{
    private GridManager gridManager;
    private GeneratorSyncPanel generatorSyncPanel;

    private float initialRotationSpeed = 3000f;
    public float governorControlSpeed; // in RPM (Rotations Per Minute)
    public float rotationSpeed; // in RPM
    public float rotationAngle; // in degree
    public float frequency; // in Hertz (Hz)
    public float voltage; // in KV
    public float current; // in KA
    public float generatorPowerFactor;

    [Tooltip("Generator power output in megawatts (MW)")]
    public float powerOutput;

    [Tooltip("Generator reactive power output in megavars (MVAR)")]
    public float reactivePowerOutput;

    [Tooltip("Generator apparent power output in megavolt-amp (MVA)")]
    public float apparentPowerOutput;

    [Tooltip("Whether the GTG is running or not")]
    public bool isRunning;

    private const float baseFrequency = 50f; // Base frequency in Hz
    private const float baseVoltage = 19.04f; // Base frequency in Hz

    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        generatorSyncPanel = FindObjectOfType<GeneratorSyncPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning)
        {
            rotationSpeed = 0f;
            frequency = 0f;
            voltage = 0f;
            current = 0f;
            powerOutput = 0f;
            reactivePowerOutput = 0f;
            apparentPowerOutput = 0f;
        } else if (isRunning)
        {
            // Set the rotation speed to its maximum rotation speed
            rotationSpeed = initialRotationSpeed + governorControlSpeed;

            // Set the voltage to its design capacity
            voltage = baseVoltage;

            // Calculate the frequency based on the rotation speed
            frequency = rotationSpeed / 60f;

            if (generatorSyncPanel.isSynchronized)
            {
                // Get power demand from GridManager
                powerOutput = gridManager.activePowerDemand;
                reactivePowerOutput = gridManager.reactivePowerDemand;

                // Calculate the current based on power output, voltage, and power factor
                // float activePowerOutputMW = powerOutput; // Convert power output to megawatts
                // float reactivePowerOutputMVAR = reactivePowerOutput; // Convert reactive power to megavars
                apparentPowerOutput = Mathf.Sqrt(Mathf.Pow(powerOutput, 2) + Mathf.Pow(reactivePowerOutput, 2));
                generatorPowerFactor = powerOutput / apparentPowerOutput;
                current = apparentPowerOutput / (Mathf.Sqrt(3) * voltage);

                // Debug.Log("Generator Power Output: " + activePowerOutputMW + " MW");
                // Debug.Log("Generator Reactive Power: " + reactivePowerOutputMVAR + " MVAR");
                // Debug.Log("Generator Power Factor: " + generatorPowerFactor);
                // Debug.Log(voltage + " KV");
                // Debug.Log(current + " KA");
            }
        }

        // Debug.Log("Current rotation speed: " + rotationSpeed);
        // Debug.Log("Current frequency is: " + frequency);
    }

    public void ToggleTurbineOperation()
    {
        isRunning = !isRunning;

        if (isRunning)
        {
            rotationSpeed = 3000f;
        }
    }

    private void CalculateRotationAngle()
    {
        rotationAngle = rotationSpeed * 360f / 60f * Time.deltaTime;
    }
}
