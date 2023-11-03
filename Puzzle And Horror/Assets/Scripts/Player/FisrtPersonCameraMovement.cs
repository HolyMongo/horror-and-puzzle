using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FisrtPersonCameraMovement : MonoBehaviour
{
    [Header("Sensetivity")]
    [SerializeField] [Tooltip("sensitivity on the X-axis")] [Range(0, 100)] private float sensX;
    [SerializeField] [Tooltip("sensitivity on the Y-axis")] [Range(0, 100)] private float sensY;
    [SerializeField] [Tooltip("Invert the input on the X-axis")] private bool invertX;
    [SerializeField] [Tooltip("Invert the input on the Y-axis")] private bool invertY;

    [Header("Restrictions")]
    [SerializeField] [Tooltip("Minimum restirctions on the Y-axis")] private float minClampY = -90f;
    [SerializeField] [Tooltip("Maximum restirctions on the Y-axis")] private float maxClampY = 90f;

    PlayerInputActions inputActions;

    Vector2 mouseDelta;
    Vector2 camRotation;

    private Transform cam;

    //Assign variables values and enable action map for inputs
    private void Start()
    {
        cam = Camera.main.transform;
        inputActions = new PlayerInputActions();
        inputActions.Player.Camera.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //Calculates the difference in mouse position between the last update and this update
        mouseDelta = inputActions.Player.Camera.ReadValue<Vector2>();

        //Take them times Time.deltatime to make it independent on framerate and then multiply them with thei respective sinsetivity
        mouseDelta *= Time.deltaTime;
        mouseDelta.x *= sensX;
        mouseDelta.y *= sensY;


        //check if any of them are inverted and if so apply them in the opposite direction (- to + or + to -). 
        //Note: non inverted controlls are applied different on the x and y axis. non inverted controlls are += on the x-axis and -= on the y-axis
        if (!invertY)
            camRotation.y -= mouseDelta.y;
        else
            camRotation.y += mouseDelta.y;

        if (!invertX)
            camRotation.x += mouseDelta.x;
        else
            camRotation.x -= mouseDelta.x;


        //Clamp the y axis to stop the player from looking behind him/her by only turning on the y-axis
        camRotation.y = Mathf.Clamp(camRotation.y, minClampY, maxClampY);

        //apply the rotation to the camera
        cam.rotation = Quaternion.Euler(camRotation.y, camRotation.x, 0f);
    }
}
