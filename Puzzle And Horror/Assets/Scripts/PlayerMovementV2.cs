using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementV2 : MonoBehaviour
{

    PlayerInputActions inputActions;
    private CharacterController cC;

    [Header("Speed values of the movement")]
    [SerializeField] [Tooltip("Walking speed of the player")] private float walkSpeed = 7;
    [SerializeField] [Tooltip("Multiplier when the player is sprinting")] private float sprintMuliplier = 1.5f;
    [SerializeField] [Tooltip("With how much force the player jumps")] private float jumpPower = 4;
    private float gravity = -9.81f;

    [Header("Ground Regristration")]
    [SerializeField] [Tooltip("The layers that the player registers as ground")] private LayerMask groundMask;
    [SerializeField] [Tooltip("The Transform of the GameObject located at the players feet")] private Transform groundcheck;
    [SerializeField] [Tooltip("True if the player is grounded")] private bool isGrounded;
    private float groundDistance = 0.3f; //radius on the ground checking sphere


    [Header("Testing things")]
    //it is the SerializeField that is in testing and not the variables
    [SerializeField] [Tooltip("Testing purposes as of now")] private Vector3 moveDir;
    [SerializeField] [Tooltip("Testing purposes as of now")] private Vector3 relativeMoreDir;
    [SerializeField] [Tooltip("Testing purposes as of now")] private Transform cam;


    //Assign variables values and enable action map for inputs
    void Start()
    {
        cC = gameObject.GetComponent<CharacterController>();
        cam = Camera.main.transform;
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Jumping.performed += Jump;
    }

    //Sorta self explanitory
    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //If the character is grounded we set the y axis in the move direction to a positive value. aka set give the character an upward force
        if (isGrounded)
        {
            moveDir.y = Mathf.Sqrt(jumpPower * -2f * gravity);
            Debug.Log(obj.phase);
        }
    }


    void FixedUpdate()
    {
        //checks inputs, stores them in a Vector2, does some calculations to determine whick direction to move the character relative to the camera
        Vector2 inputVector = inputActions.Player.Walking.ReadValue<Vector2>().normalized;

        Vector3 camRight = cam.right;
        Vector3 camForward = cam.forward;

        camRight.y = 0;
        camForward.y = 0;

        Vector3 relativeForward = inputVector.y * camForward;
        Vector3 relativeRight = inputVector.x * camRight;

        relativeMoreDir = relativeForward + relativeRight;

        //applies a multiplier if the sprint key is pressed/held down
        if (inputActions.Player.Sprint.IsPressed())
        {
            relativeMoreDir.x *= sprintMuliplier;
            relativeMoreDir.z *= sprintMuliplier;
        }

        //calls the gravity function to update the vertical force
        Gravity();

        //applies the respective forces in the character controllers own method "Move" to move the character
        cC.Move(new Vector3(relativeMoreDir.x * walkSpeed, moveDir.y, relativeMoreDir.z * walkSpeed) * Time.deltaTime);
    }

    //applies gravity to the character controller
    private void Gravity()
    {
        if (IsGrounded())
        {
            if (moveDir.y < 0)
                moveDir.y = 0;
        }
        else
            moveDir.y += gravity * Time.deltaTime;
    }


    //Checks if the character is grounded by checking a sphere beneath the character (or wherever you placed the "groundcheck" object). if if collides/hits with anything with the correct layermask it changes the variable "isGrounded" to true, else it changes it to false. It then returns "isGrounded".
    private bool IsGrounded()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);
        return isGrounded;
    }
}
