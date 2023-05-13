using UnityEngine;

public class GasTurbineInteractionHandler : Interactable
{
    [SerializeField] private GameObject turbineFan;
    [SerializeField] private float maxRpm = 3000f;
    // [SerializeField] private float rotationSpeed = 100f;

    private bool isRotating = false;

    public void Interact()
    {
        if (!isRotating)
        {
            isRotating = true;
            RotateTurbine();
        }
        else
        {
            StopRotation();
        }
    }

    public void StopRotation()
    {
        isRotating = false;
    }

    private void Update()
    {
        if (isRotating)
        {
            RotateTurbine();
        }
    }

    public void RotateTurbine()
    {
        // Calculate the new rotation angle based on the current RPM
        float rpm = Mathf.Min((turbineFan.transform.rotation.eulerAngles.z / 360f) * maxRpm, maxRpm);
        float angle = rpm * (Time.deltaTime / 60f) * 360f;

        // Apply the rotation to the cylinder
        turbineFan.transform.Rotate(0f, 0f, angle);

        // Stop rotating if the max RPM has been reached
        if (rpm >= maxRpm)
        {
            StopRotation();
        }
    }
}
