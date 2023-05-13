using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rpm = 3000f;

    // Update is called once per frame
    void Update()
    {
        float angle = rpm * 360f * Time.deltaTime / 60f;
        transform.Rotate(Vector3.forward, angle);
    }
}
