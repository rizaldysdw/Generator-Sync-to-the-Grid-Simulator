using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    public static bool isGeneratorSynchronized;

    public static float voltage = 19f;
    public static float current;
    public static float frequency;
    private float poles = 2f;
    private float generatorPowerFactor;

    public static float realPowerOutput;
    public static float reactivePowerOutput;
    public static float apparentPowerOutput;

    private float previousRealPowerOutput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float turbineRPM = GasTurbineController.turbineRatedSpeed;
        frequency = (turbineRPM * poles) / 120f;

        if (GasTurbineController.isGasTurbineRunning)
        {
            if (isGeneratorSynchronized)
            {
                // Get values from GridManager script
                realPowerOutput = GridManager.realPowerDemand;
                reactivePowerOutput = GridManager.reactivePowerDemand;

                apparentPowerOutput = Mathf.Sqrt(Mathf.Pow(realPowerOutput, 2) + Mathf.Pow(reactivePowerOutput, 2));
                generatorPowerFactor = realPowerOutput / apparentPowerOutput;
                current = apparentPowerOutput / (Mathf.Sqrt(3) * voltage);

                // Calculate the difference between the previous power output and the current power output
                float powerOutputDifference = realPowerOutput - previousRealPowerOutput;

                // Calculate RPM Change Rate based on Speed Droop theory
                float rpmChangeRate = 30f / 75f;

                // Adjust governorControlSpeed based on power output change direction
                if (powerOutputDifference > 0)
                {
                    // Power output increased, decrease governorControlSpeed to decrease RPM
                    GasTurbineController.rpmDrop -= rpmChangeRate * powerOutputDifference;
                }
                else if (powerOutputDifference < 0)
                {
                    // Power output decreased, increase governorControlSpeed to increase RPM
                    GasTurbineController.rpmDrop += rpmChangeRate * Mathf.Abs(powerOutputDifference);
                }

                // Store the current power output as the previous power output for the next frame
                previousRealPowerOutput = realPowerOutput;
            }
        }

        // Print to Console Below
        Debug.Log(isGeneratorSynchronized);
    }
}
