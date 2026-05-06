using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float sprintMultiplier; //what will be used to sprint
    public float rotateSpeed;

    public float gravity = 9.8f; //player gravity
    public float groundCheckRadius = 0.15f; //how far off the ground is grounded?
    public LayerMask groundLayer;

    private bool isGrounded;
    public bool canMove = true;
    private Vector3 velocity;
    private Transform feet;
    public Animator anim;

    private CharacterController controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        feet = transform.Find("feet");
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;

        Move();
        CheckIsGrounded();
        ApplyGravity();
    }

    public void Move()
    {
        float verticalInput = Input.GetAxis("Vertical");  //for anim
        float horizontalInput = Input.GetAxis("Horizontal");
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);  //checks if the player is sprinting
         // if sprinting multiply speed, otherwise use normal speed
        float currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;  //the ? is a shorthand else/if to check if the plyer is sprinting or not

        //both forward/backward and left/right rotations trigger movement
        float moveAmount = Mathf.Abs(verticalInput);
        float turnAmount = Mathf.Abs(horizontalInput);
        //float totalActivity = Mathf.Max(moveAmount, turnAmount);

        // this sends a value of 2.0 if sprinting, otherwise it sends the raw vertical input (0 to 1)
        float animationValue = (isSprinting && verticalInput > 0) ? 2.0f : moveAmount;
     
        if (anim != null)
        {
            anim.SetFloat("Speed", animationValue);  //changes anim based on speed
            anim.SetFloat("Turn", horizontalInput); //changes based on turn
        }
        Debug.Log($"movement: {anim.GetFloat("Speed")}");
        Debug.Log($"turn: {anim.GetFloat("Turn")}");

        float rotate = horizontalInput * rotateSpeed * Time.deltaTime;
        float move = verticalInput * currentSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * rotate);
        controller.Move(transform.forward * move);
    }


    private void CheckIsGrounded()
    {

        isGrounded = Physics.CheckSphere(feet.position, groundCheckRadius, groundLayer, QueryTriggerInteraction.Ignore);
        //fix the physics
    }
    private void ApplyGravity()
    {
       //should check is grounded first

        if (isGrounded)
        {
            velocity = Vector3.zero;
        }
        else
        {
            velocity += Vector3.down * gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }
}
