using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehavior : MonoBehaviour
{
    //Object and Component References
    Rigidbody rb;
    [SerializeField] Transform cam;
    [SerializeField] Transform camPos;
    public suitVoice voice;

    //Script References
    public pauseMenu menu;
    playerData data;
    public CameraAnimation cameraAnimation;

    //Input Variables
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDireciton;

    //Movement Variables
    public float movementSpeed;
    public float currVelocity;
    public float groundDrag;
    public float maxGroundDrag;
    public float minGroundDrag;
    public float dragDecay;
    public float holdTime;
    public float jumpForce;
    public float airMultiplier;
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    /*public float camSpeed;
    public float camPosDefault;
    public float movingOffset;
    public float tiltOffset;*/



    //Weapon Variables
    public bool readyToFire;


    private void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToFire = true;
        holdTime = 0;
        data = gameObject.GetComponent<playerData>();

    }

    private void Update()
    {
        currVelocity = getVelocity();
        movePlayer();
        if (Input.GetButtonDown("Weapon 1") | Input.GetButtonDown("Weapon 2")| Input.GetButtonDown("Weapon 3") | Input.GetButtonDown("Weapon 4"))
        {
            string weapInput = Input.inputString;
            switch(weapInput)
            {
                case "1":
                    if(data.isWeaponListIndexValid(0))data.switchWeapon(0);
                    break;
                
                case "2":
                    if (data.isWeaponListIndexValid(1)) data.switchWeapon(1);
                    break;

                case "3":
                    if(data.isWeaponListIndexValid(2)) data.switchWeapon(2);
                    break;
                case "4":
                    if(data.isWeaponListIndexValid(3)) data.switchWeapon(3);
                    break;

                default:
                    if(data.isWeaponListIndexValid(0)) data.switchWeapon(0);
                    break;
            }
        }
        if (Input.GetButton("Reload")) data.reload();
        if (Input.GetButton("Fire1") && !menu.pauseUI.activeInHierarchy) fireGun();
        if (Input.GetButton("Melee")) meleeAttack();
        

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

        if(horizontalInput != 0 || verticalInput != 0)
        {
            camMovement(true, horizontalInput, verticalInput);
            holdTime += 1 * Time.deltaTime;
            groundDrag -= holdTime * dragDecay;
            groundDrag = Mathf.Clamp(groundDrag, minGroundDrag, maxGroundDrag);
        }
        else
        {
            camMovement(false, horizontalInput, verticalInput);
            holdTime -= 1 * Time.deltaTime;
            holdTime = Mathf.Clamp(holdTime, 0, holdTime);
            groundDrag += holdTime * dragDecay;
            groundDrag = Mathf.Clamp(groundDrag, minGroundDrag, maxGroundDrag);
        }

        

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

        SpeedControl();
    }

    private void camMovement(bool moving, float x, float y)
    {
        /*
        if(moving)
        {
           
           if(camPos.localPosition.y > camPosDefault - movingOffset)
            {
                //print("Moving down");
                camPos.localPosition = Vector3.MoveTowards(camPos.localPosition, new Vector3(0, camPosDefault - movingOffset, 0), camSpeed);

            }



            
        }
        else
        {
            
            if(camPos.localPosition.y < camPosDefault)
            {
                camPos.localPosition = Vector3.MoveTowards(camPos.localPosition, new Vector3(0, camPosDefault, 0), camSpeed);
            }
            else if(camPos.localPosition.y > camPosDefault)
            {
                camPos.localPosition = new Vector3(0,camPosDefault,0);
            }

        }*/

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flatVel.magnitude > movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void fireGun()
    {
        Gun.AmmoType ammoType = data.currGun.ammoType;
        if(data.currGun != null)
        {
            if (readyToFire)
            {
                if(data.isFrozen && ammoType == Gun.AmmoType.light)
                {

                }
                else if (!(data.currAmmo <= 0))
                {
                    //print("Fired");
                    data.spendAmmo();
                    readyToFire = false;
                    data.currGun.fire();
                    rb.AddForce((cam.forward * -1) * data.currGun.kickback);
                    StartCoroutine(gunRecharge());
                }
                else data.currGun.noAmmo(); readyToFire = false; StartCoroutine(gunRecharge());
            }
        }
        
    }

    private void meleeAttack()
    {
        if (readyToFire)
        {
            voice.playMelee();
            readyToFire = false;
            data.currGun.melee(data.meleeDamageScaled, data.meleeRange);
            StartCoroutine((gunRecharge()));
        }

    }


    public float getVelocity()
    {
        List<float> velValues = new List<float>();
        velValues.Add(Mathf.Abs(rb.velocity.x));
        velValues.Add(Mathf.Abs(rb.velocity.y));
        velValues.Add(Mathf.Abs(rb.velocity.z));
        float highestValue = 0;
        for(int i = 0; i < velValues.Count; i++)
        {
            if (velValues[i] > highestValue) highestValue = velValues[i];
        }
        return highestValue;
    }


    IEnumerator gunRecharge()
    {
        yield return new WaitForSeconds(data.currGun.fireRate);
        if(this.gameObject.GetComponent<playerData>().canReload)
        {
            readyToFire = true;
        }
        
    }


}
