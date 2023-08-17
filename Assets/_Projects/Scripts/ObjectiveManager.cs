using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    
    public Text objectiveText;

    private Objective[] objectives;
    private int currentObjectiveIndex;

    void Start()
    {
        objectives = new Objective[3];
        objectives[0] = new Objective("Answer the call from Pusat Pengaturan Beban");
        objectives[1] = new Objective("Start the Gas Turbine");
        objectives[2] = new Objective("Synchronize the Generator to the Grid");

        currentObjectiveIndex = 0;
        UpdateObjectiveText();
    }

    private void UpdateObjectiveText()
    {
        objectiveText.text = objectives[currentObjectiveIndex].description;
    }

    public void CompleteObjective()
    {
        objectives[currentObjectiveIndex].isCompleted = true;

        // Proceed to the next objective
        if (currentObjectiveIndex < objectives.Length)
        {
            UpdateObjectiveText();
        } else
        {
            Debug.Log("All objectives completed");
        }
    }

    public void OnGasTurbineStart()
    {
        // Perform actions specific to starting the gas turbine
    }
}
