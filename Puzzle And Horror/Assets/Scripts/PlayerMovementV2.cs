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

    [Header("Ground Regristration")]
    [SerializeField] [Tooltip("The layers that the player registers as ground")] private LayerMask groundMask;
    [SerializeField] [Tooltip("The Transform of the GameObject located at the players feet")] private Transform groundcheck;
    [SerializeField] [Tooltip("True if the player is grounded")] private bool isGrounded;
    private float groundDistance = 0.3f;


    [Header("Testing things")]
    [SerializeField] [Tooltip("Testing purposes as of now")] private Vector3 moveDir;
    [SerializeField] [Tooltip("Testing purposes as of now")] private Vector3 relativeMoreDir;
    [SerializeField] [Tooltip("Testing purposes as of now")] private Transform cam;
    private float gravity = -9.81f;

    void Start()
    {
        cC = gameObject.GetComponent<CharacterController>();
       
        cam = Camera.main.transform;
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Jumping.performed += Jump;
    }

    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isGrounded)
        {
            moveDir.y = Mathf.Sqrt(jumpPower * -2f * gravity);
            Debug.Log(obj.phase);
        }
    }


    void FixedUpdate()
    {
        Vector2 inputVector = inputActions.Player.Walking.ReadValue<Vector2>().normalized;

        Vector3 camRight = cam.right;
        Vector3 camForward = cam.forward;

        camRight.y = 0;
        camForward.y = 0;

        Vector3 relativeForward = inputVector.y * camForward;
        Vector3 relativeRight = inputVector.x * camRight;

        relativeMoreDir = relativeForward + relativeRight;

        //moveDir.x = inputVector.x;
        //moveDir.z = inputVector.y;


        if (inputActions.Player.Sprint.IsPressed())
        {
            //moveDir.x *= sprintMuliplier;
            //moveDir.z *= sprintMuliplier;
            relativeMoreDir.x *= sprintMuliplier;
            relativeMoreDir.z *= sprintMuliplier;
        }

        Gravity();

        //cC.Move(new Vector3(moveDir.x * walkSpeed, moveDir.y, moveDir.z * walkSpeed) * Time.deltaTime);
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


    private bool IsGrounded()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);
        return isGrounded;
    }
}
