using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GTGController : MonoBehaviour
{
    // Other Classes
    private GridManager gridManager;
    private GeneratorSyncPanel generatorSyncPanel;

    // Gas Turbine Variables
    private float initialRotationSpeed = 3000f;
    public float governorControlSpeed; // in RPM (Rotations Per Minute)
    public float rotationSpeed; // in RPM
    public float rotationAngle; // in degree
    public float frequency; // in Hertz (Hz)
    [Tooltip("Whether the GTG is running or not")]
    public bool isRunning;

    // Generator Variables
    public float voltage = 19.04f; // in KV
    private float runningVoltage; // in KV
    public float current; // in KA
    public float generatorPowerFactor;
    [Tooltip("Generator power output in megawatts (MW)")]
    public float powerOutput;
    [Tooltip("Generator reactive power output in megavars (MVAR)")]
    public float reactivePowerOutput;
    [Tooltip("Generator apparent power output in megavolt-amp (MVA)")]
    public float apparentPowerOutput;

    // Generator Excitation Variables
    [Range(0, 405)]
    public static bool isExcitationConnected;
    public float excitationVoltage; // in V
    private float excitationCurrent; // in A
    private const float excitationReactance = 0.1085f; // in Ohms
    private float initialExcitationVoltage = 100f;
    public static float excitationVoltageControlValue;

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
            // Set the rotation speed based on governor control speed
            rotationSpeed = initialRotationSpeed + governorControlSpeed;

            // Set the voltage to its design capacity
            runningVoltage = voltage;

            // Calculate the frequency based on the rotation speed
            frequency = rotationSpeed / 60f;

            if (generatorSyncPanel.isSynchronized)
            {
                // Get power demand from GridManager
                powerOutput = gridManager.activePowerDemand;
                reactivePowerOutput = gridManager.reactivePowerDemand;

                // Calculate the current based on power output, voltage, and power factor
                apparentPowerOutput = Mathf.Sqrt(Mathf.Pow(powerOutput, 2) + Mathf.Pow(reactivePowerOutput, 2));
                generatorPowerFactor = powerOutput / apparentPowerOutput;
                current = apparentPowerOutput / (Mathf.Sqrt(3) * runningVoltage);
            }
        }
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

    private void CalculateExcitationCurrent()
    {
        excitationCurrent = excitationVoltage / excitationReactance;
    }

    private void CalculateReactivePowerOutput()
    {
        float sinTheta;
        sinTheta = Mathf.Sqrt(1 - Mathf.Pow(generatorPowerFactor, 2f));

        reactivePowerOutput = voltage * excitationVoltage * sinTheta;
    }
}
