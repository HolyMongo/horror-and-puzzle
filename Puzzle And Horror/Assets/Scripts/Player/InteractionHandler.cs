using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionHandler : MonoBehaviour
{
    PlayerInputActions inputActions;
    [SerializeField] [Tooltip("The rach of the player interactions")] float dist = 10f;

    [SerializeField] private TextMeshProUGUI crossairTextBar;

    private string passOnString;

    private void Start()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Interact.Enable();
        inputActions.Player.Interact.performed += Interact_performed;
        if (GameObject.Find("PopupText").GetComponent<TextMeshProUGUI>())
        {
            crossairTextBar = GameObject.Find("PopupText").GetComponent<TextMeshProUGUI>();
        }
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //float dist = Vector3.Distance(transform.position, Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)));
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, dist))
        {
            if (rayHit.transform.TryGetComponent<I_Interactable>(out I_Interactable interactable))
            {
                Debug.Log("Interactable Target Aquired!");
                interactable.Interact();
            }
            else
            {
                Debug.Log("No target detected");
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

        //float dist = Vector3.Distance(transform.position, Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)));
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, dist))
        {
            if (rayHit.transform.TryGetComponent<I_Interactable>(out I_Interactable interactable))
            {
                interactable.LookAt(out passOnString);
            }
            else //If we are not looking at anything we do not want to show text so me reset its value
            {
                passOnString = "";
            }
        }
        else //If we are not looking at anything we do not want to show text so me reset its value
        {
            passOnString = "";
        }
        ChangeText(passOnString);
    }

    public void ChangeText(string popUpText)
    {
        if (crossairTextBar != null && popUpText != null)
        {
            crossairTextBar.text = popUpText;
        }
    }
}
