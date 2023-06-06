using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorContoller : MonoBehaviour
{
    private GasTurbineController gasTurbineController;
    private float rotationAngle;

    // Start is called before the first frame update
    void Start()
    {
        gasTurbineController = FindObjectOfType<GasTurbineController>();
    }

    // Update is called once per frame
    void Update()
    {
        GeneratorRotation();
    }

    private void GeneratorRotation()
    {
            // Calculate the rotation angle per frame based on the rotation speed
        rotationAngle = gasTurbineController.rotationAngle * 360f / 60f * Time.deltaTime;

        // Rotate the gas turbine shaft object around its local Z-axis
        transform.Rotate(Vector3.forward, rotationAngle);     
    }
}
