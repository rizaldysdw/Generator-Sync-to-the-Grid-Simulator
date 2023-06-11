using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIHandler : MonoBehaviour
{
    public GameObject statsContainer;
    [SerializeField] private TextMeshProUGUI promptText;
    private bool isStatsShowed;

    public void ToggleStatsContainer()
    {
        if (!isStatsShowed)
        {
            isStatsShowed = true;
            statsContainer.SetActive(true);
        }
        else
        {
            isStatsShowed = false;
            statsContainer.SetActive(false);
        }
    }

    public void UpdatePromptText(string promptMessage)
    {
        promptText.text = promptMessage;
    }
}
