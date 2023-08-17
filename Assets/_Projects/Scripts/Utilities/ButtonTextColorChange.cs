using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTextColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color highlightColor;
    private Color originalColor;
    private Text buttonText;

    void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        originalColor = buttonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = originalColor;
    }
}
