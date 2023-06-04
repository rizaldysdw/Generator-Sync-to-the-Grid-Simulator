using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTurbineController : MonoBehaviour
{
    public GameObject gasTurbineShaft;
    public float rotationSpeed;
    public bool isRunning = false;

    // Update is called once per frame
    void Update()
    {
        // Calculate the rotation angle per frame based on the rotation speed
        float rotationAngle = GetRotationSpeed(rotationSpeed) * 360f / 60f * Time.deltaTime;

        // Rotate the gas turbine shaft object around its local Y-axis
        transform.Rotate(Vector3.up, rotationAngle);
    }

    private float GetRotationSpeed(float rotationSpeed)
    {
        if (isRunning == false)
        {
            return 0f;
        } else if (isRunning == true)
        {
            return 3000f;
        }

        // Return a default rotation speed if none of the conditions are met
        return 0f;
    }
}
