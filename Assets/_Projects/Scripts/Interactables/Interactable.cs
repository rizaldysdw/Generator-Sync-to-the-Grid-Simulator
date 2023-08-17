using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [Tooltip("Display message to Player when looking at an Interactable.")]
    public string promptMessage;

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        // Template for interaction method from subclasses
    }
}
