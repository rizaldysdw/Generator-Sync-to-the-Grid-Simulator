using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float frequency = 50f; // in Hertz (Hz)
    public float voltage = 19.04f; // in KV
    
    [Range(15, 300)]
    [Tooltip("Active power demand in megawatts (MW)")] 
    public float activePowerDemand; // in MW

    [Tooltip("Reactive power demand in megavars (MVAR)")]
    public float reactivePowerDemand; // in MVAR
}