using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FisrtPersonCameraMovement : MonoBehaviour
{
    [Header("Sensetivity")]
    [SerializeField] [Tooltip("sensitivity on the X axis")] [Range(0, 100)] private float sensX;
    [SerializeField] [Tooltip("Invert the input on the X axis")] private bool invertX;
    [SerializeField] [Tooltip("sensitivity on the Y axis")] [Range(0, 100)] private float sensY;
    [SerializeField] [Tooltip("Invert the input on the Y axis")] private bool invertY;

    [Header("Restrictions")]
    [SerializeField] [Tooltip("Minimum restirctions on the Y axis")] private float minClampY = -90f;
    [SerializeField] [Tooltip("Maximum restirctions on the Y axis")] private float maxClampY = 90f;

    PlayerInputActions inputActions;

    Vector2 mouseDelta;
    Vector2 camRotation;

    private Transform cam;
    private void Start()
    {
        cam = Camera.main.transform;
        inputActions = new PlayerInputActions();
        inputActions.Player.Camera.Enable();
    }

    private void Update()
    {
        mouseDelta = inputActions.Player.Camera.ReadValue<Vector2>();

        mouseDelta *= Time.deltaTime;
        mouseDelta.x *= sensX;
        mouseDelta.y *= sensY;

        if (!invertY)
            camRotation.y -= mouseDelta.y;
        else
            camRotation.y += mouseDelta.y;

        if (!invertX)
            camRotation.x += mouseDelta.x;
        else
            camRotation.x -= mouseDelta.x;

        camRotation.y = Mathf.Clamp(camRotation.y, minClampY, maxClampY);
        cam.rotation = Quaternion.Euler(camRotation.y, camRotation.x, 0f);

        //cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //cam.Rotate(Vector3.up * mouseDelta.x * sensX * Time.deltaTime);
    }
}
