using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTurbineController : MonoBehaviour
{
    public static bool isGasTurbineRunning;
    public static float turbineRatedSpeed = 3000f;
    public static float rpmDrop;
    
    public static float governorControlSpeed;
    private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGasTurbineRunning)
        {
            // Calculate the target rotation speed based on the load demand and RPM drop
            float targetRotationSpeed = turbineRatedSpeed + rpmDrop + governorControlSpeed;

            // Gradually update the rotation speed towards the target rotation speed
            rotationSpeed = Mathf.Lerp(rotationSpeed, targetRotationSpeed, 0.1f);
        }
    }
}
