using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehavior : MonoBehaviour
{
    //Object and Component References
    Rigidbody rb;
    [SerializeField] Transform cam;

    //Script References
    playerData data;

    //Input Variables
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDireciton;

    //Movement Variables
    public float movementSpeed;
    public float groundDrag;
    public float jumpForce;
    public float airMultiplier;
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    //Weapon Variables
    bool readyToFire;



    private void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToFire = true;

        data = gameObject.GetComponent<playerData>();

    }

    private void Update()
    {
        movePlayer();
        if (Input.GetButton("Fire1")) fireGun();
        if (Input.GetButton("Reload")) data.reload();

    }

    private void movePlayer()
    {
        //Check if Grounded
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        //Jump
        if ((Input.GetButtonDown("Jump")) && grounded) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        //Get Inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Set Directions
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = horizontalInput;

        Vector3 forwardRelative = verticalInput * camForward;
        Vector3 rightRelative = horizontalInput * camRight;

        Vector3 moveDir = forwardRelative + rightRelative;
        Vector3 newForce = new Vector3(moveDir.x, 0, moveDir.z);

        //Check if grounded and move
        if (grounded) rb.drag = groundDrag;
        else rb.drag = 0;

        if (grounded) rb.AddForce((newForce * movementSpeed) * Time.deltaTime, ForceMode.Force);
        else if (!grounded) rb.AddForce((newForce * movementSpeed * airMultiplier) * Time.deltaTime, ForceMode.Force);
    }

    private void fireGun()
    {
        if (readyToFire)
        {
            if (!(data.currAmmo <= 0))
            {
                data.spendAmmo();
                readyToFire = false;
                StartCoroutine(gunRecharge());
            }
        }
    }

    IEnumerator gunRecharge()
    {
        yield return new WaitForSeconds(data.currGun.fireRate);
        readyToFire = true;
        
    }


}
