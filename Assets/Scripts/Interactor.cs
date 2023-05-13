using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayerMask = 6;
    [SerializeField] private float maxDistance = 2f;

    UnityEvent onInteract;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, interactableLayerMask))
        {
            if (hit.collider.GetComponent<Interactable>() != false)
            {
                onInteract = hit.collider.GetComponent<Interactable>().onInteract;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    onInteract.Invoke();
                    Debug.Log("You interacted with " + hit.collider.name);
                }
            }
        }
    }
}
