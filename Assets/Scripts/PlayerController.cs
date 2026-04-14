using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    public float gravity = 9.8f; //player gravity
    public float groundCheckRadius = 0.15f; //how far off the ground is grounded?
    public LayerMask groundLayer;

    private bool isGrounded;
    private Vector3 velocity;
    private Transform feet;

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
        Move();
        CheckIsGrounded();
        ApplyGravity();
    }

    public void Move()
    {
        float rotate = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        float move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * rotate);

        controller.Move(transform.forward * move);

    }

    private void CheckIsGrounded()
    {
        isGrounded = Physics.CheckSphere(feet.position, groundCheckRadius, groundLayer, QueryTriggerInteraction.Ignore);
    }
    private void ApplyGravity()
    {
        velocity += Vector3.down * gravity * Time.deltaTime;

        if (isGrounded)
        {
            velocity = Vector3.zero;
        }

        controller.Move(velocity);
    }
}
