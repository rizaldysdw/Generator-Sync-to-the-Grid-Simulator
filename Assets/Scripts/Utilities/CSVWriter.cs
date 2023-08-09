using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CSVWriter : MonoBehaviour
{
    private string folderPath;
    private string filePath;
    private string fileName;

    private bool isRecording = false;

    private string startTimeFormatted;
    private string endTimeFormatted;

    private float recordInterval = 1f; // Interval in seconds
    private float timer = 0f;

    // References to components or scripts to get the data
    private GTGController gtgController;

    // Start is called before the first frame update
    void Start()
    {
        gtgController = FindObjectOfType<GTGController>();

        // Set filename, folder path, and file path to save CSV file
        fileName = $"Data_{DateTime.Now.ToString("yyyyMMddHHmmss")}.csv";
        folderPath = Path.Combine(Application.dataPath, "Recordings", "Data");
        filePath = Path.Combine(folderPath, fileName);

        // Uncomment this line to start recording immediately when the scene starts
        // StartRecording();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if recording is enabled and the interval has passed
        if (isRecording && timer >= recordInterval)
        {
            // Write data to the CSV file
            RecordData();

            // Reset the timer
            timer = 0f;
        }

        // Increment the timer
        timer += Time.deltaTime;
    }

    // Method to start recording data
    public void StartRecording()
    {
        isRecording = true;
        startTimeFormatted = DateTime.Now.ToString("yyyyMMddHHmmss");

        // Create the CSV file and write the header
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine("Time,RPM,Generator Frequency,Generator Output,Generator Power Factor");
        }

        Debug.Log("Recording started...");
    }

    // Method to stop recording data
    public void StopRecording()
    {
        isRecording = false;
        endTimeFormatted = DateTime.Now.ToString("yyyyMMddHHmmss");

        // Rename the file with the formatted start and end time
        File.Move(filePath, Path.Combine(folderPath, $"Data_{startTimeFormatted}_{endTimeFormatted}.csv"));

        Debug.Log("Recording stopped!");
    }

    // Method to record data to the CSV file
    private void RecordData()
    {
        // Get the current data values
        float rpm = gtgController.rotationSpeed;
        float frequency = gtgController.frequency;
        float output = gtgController.powerOutput;
        float powerFactor = gtgController.generatorPowerFactor;

        // Get the current time in dd/mm/yyyy hh:mm:ss format
        string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        // Write the data to the CSV file
        using (StreamWriter sw = new StreamWriter(filePath, true))
        {
            sw.WriteLine($"{time},{rpm},{frequency},{output},{powerFactor}");
        }
    }

    public void HandleRecordingToggle(bool isRecording)
    {
        if (isRecording)
        {
            StartRecording();
        }
        else
        {
            StopRecording();
        }
    }
}
