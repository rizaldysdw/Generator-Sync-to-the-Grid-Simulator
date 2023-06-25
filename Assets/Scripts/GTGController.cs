using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GTGController : MonoBehaviour
{
    private float initialRotationSpeed = 3000f;
    public float governorControlSpeed; // in RPM (Rotations Per Minute)
    public float rotationSpeed; // in RPM
    public float rotationAngle;
    public float frequency; // in Hertz (Hz)

    public bool isRunning;

    private const float baseFrequency = 50f; // Base frequency in Hz

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning)
        {
            rotationSpeed = 0f;
        } else if (isRunning)
        {
            rotationSpeed = initialRotationSpeed + governorControlSpeed;
        }

        rotationAngle = rotationSpeed * 360f / 60f * Time.deltaTime;

        // Calculate the frequency based on the rotation speed
        frequency = rotationSpeed / 60f;

        // Debug.Log("Current rotation speed: " + rotationSpeed);
        // Debug.Log("Current frequency is: " + frequency);
    }

    private void RotateTurbine()
    {
        // Rotate the gas turbine shaft object around its local Z-axis
        // transform.Rotate(Vector3.forward, rotationAngle);        
    }

    public void ToggleTurbineOperation()
    {
        isRunning = !isRunning;

        if (isRunning)
        {
            rotationSpeed = 3000f;
        }
    }
}
