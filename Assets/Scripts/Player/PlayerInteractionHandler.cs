using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    private PlayerFPSCharacterController playerFPSCharacterController;
    private PlayerUIHandler playerUIHandler;
    private Camera playerCamera;
    [SerializeField] private LayerMask layerMask;

    private float maxDistance = 3f;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
        playerUIHandler = GetComponent<PlayerUIHandler>();
        playerFPSCharacterController = GetComponent<PlayerFPSCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUIHandler.UpdatePromptText(string.Empty);

        // Create a Raycast from the center of MainCamera
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        // Variable to store RaycastHit information
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask))
        {
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                playerUIHandler.UpdatePromptText(interactable.promptMessage);

                if (playerFPSCharacterController.playerInput.Default.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
