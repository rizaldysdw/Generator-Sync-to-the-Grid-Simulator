using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchroscopeController : MonoBehaviour
{
    public Transform needle; // Reference to the synchroscope needle GameObject
    private GTGController gtgController; // Reference to the GTGController script
    private GridManager gridManager; // Reference to the GridManager script
    
    private float rotationSpeed = 100f; // Speed at which the needle rotates

    private void Start()
    {
        gtgController = FindObjectOfType<GTGController>();
        gridManager = FindObjectOfType<GridManager>();
    }

    private void Update()
    {
        // Compare the generator frequency with the grid frequency
        if (!gtgController.isSynchronized && gtgController.frequency > gridManager.frequency)
        {
            // Generator frequency is higher than grid frequency
            // Rotate the synchroscope needle clockwise
            needle.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
        }
        else if (!gtgController.isSynchronized && gtgController.frequency < gridManager.frequency)
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
}
