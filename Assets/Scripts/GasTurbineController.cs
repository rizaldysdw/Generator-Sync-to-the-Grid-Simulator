using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTurbineController : MonoBehaviour
{
    private float rotationSpeed;
    public float rotationAngle;
    public bool isRunning = false;

    // Update is called once per frame
    void Update()
    {
        RotateTurbine();
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

    private void RotateTurbine()
    {
        // Calculate the rotation angle per frame based on the rotation speed
        rotationAngle = GetRotationSpeed(rotationSpeed) * 360f / 60f * Time.deltaTime;

        // Rotate the gas turbine shaft object around its local Z-axis
        transform.Rotate(Vector3.forward, rotationAngle);        
    }

    public void ToggleTurbineOperation()
    {
        if (!isRunning)
        {
            isRunning = true;
        } else if (isRunning)
        {
            isRunning = false;
        }
    }
}
