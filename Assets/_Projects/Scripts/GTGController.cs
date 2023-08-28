using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GTGController : MonoBehaviour
{
    // Other Classes
    private GridManager gridManager;
    private GeneratorSyncPanel generatorSyncPanel;

    // Gas Turbine Variables
    private float initialRotationSpeed;
    public float governorControlSpeed; // in RPM (Rotations Per Minute)
    public float rotationSpeed; // in RPM
    public float rotationAngle; // in degree
    public float frequency; // in Hertz
    [Tooltip("Whether the GTG is running or not")]
    public bool isRunning;

    // Generator Variables
    private float generatorPoles;
    public float voltage; // in KV
    public float current; // in KA
    private float ratedCurrent; // in KA
    public float generatorPowerFactor;
    [Tooltip("Generator power output in megawatts (MW)")]
    public float powerOutput;
    private float previousPowerOutput;
    [Tooltip("Generator reactive power output in megavars (MVAR)")]
    public float reactivePowerOutput;
    [Tooltip("Generator apparent power output in megavolt-amp (MVA)")]
    public float apparentPowerOutput;

    // Generator Excitation Variables
    public float excitationCurrent; // in A
    private float initialExcitationCurrent = 1386f;
    private float minExcitationCurrent = 0.0f; // Minimum excitation current value
    private const float FieldCurrentChangePerMVAR = 5.23f; // Change in field current for every 1 MVAR increase
    private float generatorMEL; // Minimum Excitation Limit in MVAR
    private float generatorOEL; // Over Excitation Limit in MVAR

    // Load Control Variables
    private float targetRotationSpeed; // Target rotation speed for adjusting the RPM
    private float rpmChangeRate = 30f / 75f; // 30 RPM decrease for every 75 MW increase
    private float rpmDrop;

    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        generatorSyncPanel = FindObjectOfType<GeneratorSyncPanel>();

        previousPowerOutput = powerOutput;
        rpmDrop = 0f;

        SetupValuesFromPlayerSettings();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            // Calculate the target rotation speed based on the load demand and RPM drop
            targetRotationSpeed = initialRotationSpeed + rpmDrop + governorControlSpeed;

            // Gradually update the rotation speed towards the target rotation speed
            rotationSpeed = Mathf.Lerp(rotationSpeed, targetRotationSpeed, 0.1f);

            // Calculate frequency
            frequency = (rotationSpeed * generatorPoles) / 120f;

            // Retrieve voltage value from Player Settings
            voltage = LoadingData.generatorRatedVoltage;

            if (generatorSyncPanel.isSynchronized)
            {
                CalculateGeneratorOperation();
                SetExcitationCurrent(reactivePowerOutput);
            }
            else if (!generatorSyncPanel.isSynchronized)
            {
                ResetGeneratorVariables(voltage, 0f, frequency);
            }
        }
        else
        {
            generatorSyncPanel.isSynchronized = false;
            frequency = 0f;
            voltage = 0f;
            powerOutput = 0f;
            reactivePowerOutput = 0f;
            current = 0f;
        }
    }

    private void SetupValuesFromPlayerSettings()
    {
        initialRotationSpeed = LoadingData.turbineRatedSpeed;
        ratedCurrent = LoadingData.generatorRatedCurrent;
        generatorPoles = LoadingData.generatorPoles;
        generatorMEL = LoadingData.generatorMEL;
        generatorOEL = LoadingData.generatorOEL;
    }

    private void CalculateGeneratorOperation()
    {
        // Get values from GridManager script
        powerOutput = gridManager.activePowerDemand;
        reactivePowerOutput = gridManager.reactivePowerDemand;

        apparentPowerOutput = Mathf.Sqrt(Mathf.Pow(powerOutput, 2) + Mathf.Pow(reactivePowerOutput, 2));
        generatorPowerFactor = powerOutput / apparentPowerOutput;
        current = apparentPowerOutput / (Mathf.Sqrt(3) * voltage);

        // Calculate the difference between the previous power output and the current power output
        float powerOutputDifference = powerOutput - previousPowerOutput;

        // Adjust governorControlSpeed based on power output change direction
        if (powerOutputDifference > 0)
        {
            // Power output increased, decrease governorControlSpeed to decrease RPM
            rpmDrop -= rpmChangeRate * powerOutputDifference;
        }
        else if (powerOutputDifference < 0)
        {
            // Power output decreased, increase governorControlSpeed to increase RPM
            rpmDrop += rpmChangeRate * Mathf.Abs(powerOutputDifference);
        }

        // Store the current power output as the previous power output for the next frame
        previousPowerOutput = powerOutput;

        // Excitation Limit
        if (reactivePowerOutput >= generatorOEL || reactivePowerOutput <= generatorMEL)
        {
            isRunning = false;
        }
    }

    private void SetExcitationCurrent(float reactivePowerDemand)
    {
        // Calculate the change in field current based on the reactive power demand
        float fieldCurrentChange = reactivePowerDemand * FieldCurrentChangePerMVAR;

        // Calculate the new excitation current
        float newExcitationCurrent = initialExcitationCurrent + fieldCurrentChange;

        // Set the excitation current (limiting it to a minimum value)
        excitationCurrent = Mathf.Max(newExcitationCurrent, minExcitationCurrent);

        string fieldCurrent = $"Field Current: {excitationCurrent} A";
        Debug.Log(fieldCurrent);
    }

    private void ResetGeneratorVariables(float targetVoltage, float targetCurrent, float targetFrequency)
    {
        voltage = targetVoltage;
        current = targetCurrent;
        frequency = targetFrequency;
    }

    public void ToggleTurbineOperation()
    {
        isRunning = !isRunning;
    }
}