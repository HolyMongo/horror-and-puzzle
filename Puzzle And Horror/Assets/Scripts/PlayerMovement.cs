using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Transform cam;
    PlayerInputActions inputActions;
    private Rigidbody rb;

    [SerializeField] private float walkSpeed = 7;
    [SerializeField] private float sprintMuliplier = 1.5f;
    [SerializeField] private float jumpPower = 4;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Jumping.performed += Jump;
    }

    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        rb.velocity = Vector3.up * jumpPower;
        Debug.Log(obj.phase);
        //rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }


    void FixedUpdate()
    {
        Vector2 inputVector = inputActions.Player.Walking.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        moveDir = moveDir.normalized;

        if (inputActions.Player.Sprint.IsPressed())
            moveDir *= sprintMuliplier;


        moveDir = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);
        rb.velocity = new Vector3(moveDir.x * walkSpeed, moveDir.y, moveDir.z * walkSpeed);
    }
}
