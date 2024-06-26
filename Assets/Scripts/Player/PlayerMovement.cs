using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement variables")]
    public float moveSpeed;
    public float groundDrag;

    
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("References")]
    public Transform playerOrientation;
    public Rigidbody rb;

    [Header("Key Bindings")]
    public KeyCode jumpKej = KeyCode.Space;

    //Misc
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    private void Start()
    {
        //Get rb refference and freez its rotation
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        //Get pleyer orientatnion reference
        playerOrientation = this.transform.Find("orientation");
    }

    private void Update()
    {
        //Ground check
        grounded = Physics.Raycast(new Vector3(transform.position.x, transform.position.y + playerHeight * 0.5f, transform.position.z), 
                                   Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        GetInput();
        SpeedControl();

        //Handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        Debug.Log(readyToJump);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        //Set movement inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Check for jump
        if(Input.GetKey(jumpKej) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    private void MovePlayer()
    {
        //Calc move dir
        moveDirection = playerOrientation.forward * verticalInput + playerOrientation.right * horizontalInput;

        //Add movement force
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if(!grounded) // in air
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }
    private void SpeedControl()
    {
        //Get horizontal velocity
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //Limit vel if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        //Reset y vel
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //Add force
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red; // Set the color of the raycast

    //    // Draw the raycast
    //    Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Vector3.down * (playerHeight * 0.5f + 0.2f));
    //}
}
