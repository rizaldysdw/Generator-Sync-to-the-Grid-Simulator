using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchroscopeController : MonoBehaviour
{
    // Reference to the synchroscope needle GameObject
    [SerializeField] private Transform needle; 
    
    // Speed at which the needle rotates
    private float rotationSpeed = 100f; 

    void Update()
    {
        UpdateNeedleRotation();
    }

    private void UpdateNeedleRotation()
    {
        // Synchroscope behavior when Generator is not synchronized
        if (GasTurbineController.isGasTurbineRunning)
        {
            // Compare the generator frequency with the grid frequency
            if (!GeneratorController.isGeneratorSynchronized && GeneratorController.frequency > GridManager.frequency)
            {
                // Generator frequency is higher than grid frequency
                // Rotate the synchroscope needle clockwise
                needle.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
            }
            else if (!GeneratorController.isGeneratorSynchronized && GeneratorController.frequency < GridManager.frequency)
            {
                // Generator frequency is lower than grid frequency
                // Rotate the synchroscope needle counterclockwise
                needle.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
            }
            else
            {
                // Generator frequency is equal to grid frequency
                // Rotate the synchroscope needle towards the initial position (0 rotation)
                float currentRotation = needle.eulerAngles.z;
                float targetRotation = 0f;

                // Calculate the rotation direction
                float rotationDirection = (targetRotation > currentRotation) ? 1f : -1f;

                // Calculate the rotation amount based on the remaining rotation distance
                float rotationAmount = Mathf.Abs(targetRotation - currentRotation);

                // Rotate the synchroscope needle
                needle.Rotate(0f, 0f, rotationSpeed * rotationDirection * Time.deltaTime);

                // Check if the remaining rotation amount is smaller than the rotation speed
                if (rotationAmount <= rotationSpeed * Time.deltaTime)
                {
                    // Set the rotation directly to the target rotation
                    needle.rotation = Quaternion.Euler(0f, 0f, targetRotation);
                }
            }
        }
        else
        {
            // Stop needle rotation due to GTG is tripped
            // Rotate the synchroscope needle towards the initial position (0 rotation)
            float currentRotation = needle.eulerAngles.z;
            float targetRotation = 0f;

            // Calculate the rotation direction
            float rotationDirection = (targetRotation > currentRotation) ? 1f : -1f;

            // Calculate the rotation amount based on the remaining rotation distance
            float rotationAmount = Mathf.Abs(targetRotation - currentRotation);

            // Rotate the synchroscope needle
            needle.Rotate(0f, 0f, rotationSpeed * rotationDirection * Time.deltaTime);

            // Check if the remaining rotation amount is smaller than the rotation speed
            if (rotationAmount <= rotationSpeed * Time.deltaTime)
            {
                // Set the rotation directly to the target rotation
                needle.rotation = Quaternion.Euler(0f, 0f, targetRotation);
            }
        }
    }
}
