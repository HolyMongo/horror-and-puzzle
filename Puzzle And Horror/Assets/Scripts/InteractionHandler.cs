using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    PlayerInputActions inputActions;
    [SerializeField] [Tooltip("The rach of the player interactions")] float dist = 10f;

    private void Start()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Interact.Enable();
        inputActions.Player.Interact.performed += Interact_performed;

    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Pew Pew. Interaction Ray!");
        //float dist = Vector3.Distance(transform.position, Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)));
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, dist))
        {
            Debug.Log("Target Aquired");
            if (rayHit.transform.TryGetComponent<I_Interactable>(out I_Interactable interactable))
            {
                Debug.Log("Interactable Target Aquired!");
                interactable.Interact();
            }
        }
        else
        {
            Debug.Log("No target detected");
        }
    }

    private void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red);
    }
}
