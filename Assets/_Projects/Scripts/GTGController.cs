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
    private bool isProtectionActive;

    // Generator Variables
    [Range(0, 19.04f)]
    public float voltage = 19.04f; // in KV
    private float runningVoltage; // in KV
    public float current; // in KA
    public float generatorPowerFactor;
    [Tooltip("Generator power output in megawatts (MW)")]
    public float powerOutput;
    private float previousPowerOutput;
    [Tooltip("Generator reactive power output in megavars (MVAR)")]
    public float reactivePowerOutput;
    [Tooltip("Generator apparent power output in megavolt-amp (MVA)")]
    public float apparentPowerOutput;

    // Generator Excitation Variables
    [Range(0, 405)]
    public float excitationVoltage; // in V
    public float excitationCurrent; // in A
    private float initialExcitationCurrent = 1386f;
    private float minExcitationCurrent = 0.0f; // Minimum excitation current value
    private const float FieldCurrentChangePerMVAR = 5.23f; // Change in field current for every 1 MVAR increase

    // Load Control Variables
    private float targetRotationSpeed; // Target rotation speed for adjusting the RPM
    private float rpmChangeRate = 30f / 75f; // 30 RPM decrease for every 75 MW increase
    private float rpmDrop;

    // Frequency Protection
    private float lowFrequencyThreshold = 47f; // Hz
    private float lowFrequencyAlarmThreshold = 47.5f; // Hz
    private float highFrequencyThreshold = 52f; // Hz
    private float highFrequencyAlarmThreshold = 51.5f; // Hz
    public bool isGeneratorTripped = false;

    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        generatorSyncPanel = FindObjectOfType<GeneratorSyncPanel>();

        previousPowerOutput = powerOutput;
        rpmDrop = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        frequency = rotationSpeed / 60f;

        if (isRunning)
        {
            // Calculate the target rotation speed based on the load demand and RPM drop
            targetRotationSpeed = initialRotationSpeed + rpmDrop + governorControlSpeed;

            // Gradually update the rotation speed towards the target rotation speed
            rotationSpeed = Mathf.Lerp(rotationSpeed, targetRotationSpeed, 0.1f);

            runningVoltage = voltage;

            if (rotationSpeed >= 2900f)
            {
                isProtectionActive = true;
            }

            if (generatorSyncPanel.isSynchronized)
            {
                CalculateGeneratorOperation();
                SetExcitationCurrent(reactivePowerOutput);
            }
            else if (!generatorSyncPanel.isSynchronized)
            {
                ResetGeneratorVariables(0f, 0f, 0f);
            }

            TurbineSpeedProtection();
            FrequencyProtection();
        }
    }

    private void SimpleCalculation()
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
        }
        else if (isRunning)
        {
            // Set the rotation speed based on governor control speed
            rotationSpeed = initialRotationSpeed + rpmDrop + governorControlSpeed;

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
    private void CalculateGeneratorOperation()
    {
        // Get values from GridManager script
        powerOutput = gridManager.activePowerDemand;
        reactivePowerOutput = gridManager.reactivePowerDemand;

        apparentPowerOutput = Mathf.Sqrt(Mathf.Pow(powerOutput, 2) + Mathf.Pow(reactivePowerOutput, 2));
        generatorPowerFactor = powerOutput / apparentPowerOutput;
        current = apparentPowerOutput / (Mathf.Sqrt(3) * runningVoltage);

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
    }

    private void CalculateGeneratorCurrent()
    {
        // Calculate apparent power based on active power and reactive power
        apparentPowerOutput = Mathf.Sqrt(Mathf.Pow(powerOutput, 2) + Mathf.Pow(reactivePowerOutput, 2));

        // Calculate current using apparent power formula
        current = apparentPowerOutput / (Mathf.Sqrt(3) * runningVoltage);
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

    private void TurbineSpeedProtection()
    {
        if (isRunning && isProtectionActive && rotationSpeed >= 3300f)
        {
            TripGasTurbine();
            Debug.Log("GT is tripped by Overspeed Protection");
        }
    }

    private void FrequencyProtection()
    {
        float currentFrequency = frequency;

        // Check for low frequency conditions
        if (currentFrequency <= lowFrequencyThreshold && isRunning && isProtectionActive)
        {
            // Trip the gas turbine if frequency is too low
            StartCoroutine(CountdownTripGasTurbine(0.1f));
            Debug.Log("GT Tripped!");
        }
        else if (currentFrequency <= lowFrequencyAlarmThreshold && isProtectionActive)
        {
            // Low frequency alarm
            Debug.Log("Alarm: Low Frequency");

            // Trip Generator after 20 seconds
            StartCoroutine(CountdownTripGenerator(20f));
        }
        else
        {
            // Reset trip flags
            isGeneratorTripped = false;
            ResetGeneratorVariables(19.04f, current, frequency);
        }

        // Check for high frequency conditions
        if (currentFrequency >= highFrequencyThreshold && isProtectionActive)
        {
            // Generator is offline due to high frequency
            if (!isGeneratorTripped)
            {
                StartCoroutine(CountdownTripGenerator(0.1f));
                Debug.Log("Generator Tripped");
            }
        }
        else if (currentFrequency >= highFrequencyAlarmThreshold && isProtectionActive)
        {
            Debug.Log("Alarm: High Frequency");
        }
        else
        {
            // Reset trip flag
            isGeneratorTripped = false;
            ResetGeneratorVariables(19.04f, current, frequency);
        }
    }

    private IEnumerator CountdownTripGasTurbine(float delay)
    {
        // Wait for the specified delay before tripping the gas turbine
        yield return new WaitForSeconds(delay);

        // Trip the gas turbine
        TripGasTurbine();
    }

    private IEnumerator CountdownTripGenerator(float delay)
    {
        // Wait for the specified delay before tripping the gas turbine
        yield return new WaitForSeconds(delay);

        TripGenerator();
    }

    private void TripGasTurbine()
    {
        isRunning = false;
        isProtectionActive = false;

        rotationSpeed = 0f;
        governorControlSpeed = 0f;
    }

    private void TripGenerator()
    {
        isGeneratorTripped = true;

        ResetGeneratorVariables(0f, 0f, 0f);
        Debug.Log("Generator Tripped!");
    }

    public void ToggleTurbineOperation()
    {
        isRunning = !isRunning;

        if (isRunning)
        {
            ResetTurbineVariables(3000f);
            isProtectionActive = false;
        }
        else
        {
            isProtectionActive = false;
            ResetTurbineVariables(0f);
        }
    }

    private void ResetTurbineVariables(float RPM)
    {
        isProtectionActive = false;

        initialRotationSpeed = RPM;
        governorControlSpeed = 0f;
        rotationSpeed = initialRotationSpeed + governorControlSpeed;
    }

    private void ResetGeneratorVariables(float targetVoltage, float targetCurrent, float targetFrequency)
    {
        if (!isGeneratorTripped)
        {
            voltage = targetVoltage;
        }
        else
        {
            generatorSyncPanel.isSynchronized = false;
            voltage = targetVoltage;
            current = targetCurrent;
            frequency = targetFrequency;
        }
    }

    private void CalculateRotationAngle()
    {
        rotationAngle = rotationSpeed * 360f / 60f * Time.deltaTime;
    }
}