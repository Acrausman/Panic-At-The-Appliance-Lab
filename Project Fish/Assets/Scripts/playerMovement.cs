using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float groundDrag;

    public float jumpForce;
    //public float jumpCooldown;
    public float airMultiplier;
    //bool readyToJump;

    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDireciton;

    Rigidbody rb;

    [SerializeField] Transform cam;

    private void Start()
    {
        //readyToJump = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        //Check if Grounded
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        //Jump
        if ((Input.GetButtonDown("Jump")) && grounded) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        //Get Inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = horizontalInput;

        Vector3 forwardRelative = verticalInput * camForward;
        Vector3 rightRelative = horizontalInput * camRight;

        Vector3 moveDir = forwardRelative + rightRelative;
        Vector3 newForce = new Vector3(moveDir.x, 0, moveDir.z);

        if (grounded) rb.drag = groundDrag;
        else rb.drag = 0;


        if (grounded) rb.AddForce((newForce * movementSpeed) * Time.deltaTime, ForceMode.Force);
        else if (!grounded) rb.AddForce((newForce * movementSpeed * airMultiplier) * Time.deltaTime, ForceMode.Force);
    }


}
