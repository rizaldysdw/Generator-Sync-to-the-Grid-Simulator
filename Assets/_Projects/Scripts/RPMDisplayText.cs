using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RPMDisplayText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rpmText;
    [SerializeField] private GameObject turbineFan;
    [SerializeField] private Rotator turbineRotator;

    // Start is called before the first frame update
    void Start()
    {
        turbineRotator = turbineFan.GetComponent<Rotator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (turbineRotator.enabled == false)
        {
            rpmText.SetText("0 RPM");
        } else
        {
            rpmText.SetText(turbineRotator.rpm + " RPM");
        }
    }
}
