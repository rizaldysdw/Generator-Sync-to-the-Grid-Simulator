using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIHandler : MonoBehaviour
{
    public GameObject statsContainer;
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
}
