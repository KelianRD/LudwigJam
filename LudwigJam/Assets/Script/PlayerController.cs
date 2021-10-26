using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// FirstPerson RigidBody CharacterController with new InputSystem
/// 
/// gravityMultipliers() This function will make the player fall faster when he is doing a long jump giving the impression of a phyisically accurate jump
/// The multipliers can be tweaked to change the feel of the jump. Setting both of them to 1 will make the jump fell like a normal (floating) unity jump
/// </summary>

[RequireComponent(typeof(Rigidbody), typeof(InputManager))]
public class PlayerController : MonoBehaviour
{
    #region References

    private Rigidbody rb;
    private InputManager im;
    public Camera playerCam;
    public Animator anim;

    #endregion


    #region Variables

    [Header(header: "Walk / Run settings")]
    public int playerWalkSpeed = 5; //The speed of the player when it walks

    [Header(header: "Jump settings")]
    public Transform feetTransform; //the transform used to check if player is grounded
    [Range(.1f, .9f)]
    public float groundCheckRadius = .1f;
    public float jumpForce = 4; //The force of the player's jump
    private bool isJumping; //If the player is jumping
    public float fallMultiplier = 2.5f, lowJumpMultiplier = 2f; //Different multipliers to change the gravity according to the player jumps
    public LayerMask whatIsGround; //The layermask which mark the ground

    private bool canDoubleJump = true;

    [Header(header: "Current Speed")]
    public float currentSpeed = 0; //The current speed of the player


    private float xAxis, zAxis; //Variable of the speed of the player on the axis
    private CapsuleCollider playerCollider; //The collider of the player


    [Header(header: "Mouse look")]
    public float sensitivity = .5f; //The sensitivity of the mouselook
    public int lookSmooth = 2; //How smooth the camera movements are
    private Vector2 lookDirection = new Vector2(); //lookDirection

    [Header(header: "Ledge Grabing")]
    private bool isHanging = false;
    private Ledge activeLedge;
    public GameObject ledgeChecker;

    public Transform finishPos;

    public GameObject pauseMenuObject;
    private bool isInMenu = false;

    public bool isPaused = false;
    #endregion



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        im = GetComponent<InputManager>();
        playerCollider = GetComponent<CapsuleCollider>();

        rb.velocity = Vector3.zero;

        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuObject.SetActive(false);
    }

    private void Update()
    {
        if(!isPaused)
            MouseLook();

        if (xAxis != 0 || zAxis != 0)
            currentSpeed = playerWalkSpeed;
        else
            currentSpeed = 0;

        //Get the movement of the player thanks to the InputManager
        xAxis = im.movement.x; //D - Q
        zAxis = im.movement.y; //Z - S

        isJumping = im.jump == 1 ? true : false; //Check if the player is jumping

        if (isJumping || IsGrounded())
            ledgeChecker.SetActive(false);
        else
            ledgeChecker.SetActive(true);


        if (im.finish == 1)
            transform.position = finishPos.position;


        anim.SetInteger("currentSpeed", Mathf.RoundToInt(currentSpeed)); //Modify the animation according to the speed (walk animation but if over a certain value then run animation)
        

        //if the player is hanging we dont want to check if he is grounded
        if (isHanging)
        {
            anim.SetBool("isGrounded", true);
            anim.SetInteger("zAxis", 0);
            anim.SetInteger("xAxis", 0);
        }
        else
        {
            anim.SetBool("isGrounded", IsGrounded());
            anim.SetInteger("zAxis", Mathf.RoundToInt(zAxis));
            anim.SetInteger("xAxis", Mathf.RoundToInt(xAxis));
        }

        if (im.bugReload == 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (im.escape == 1)
            ShowMenu();

            
    }

    private void FixedUpdate() //Because we are using a rigidBody the movement calculations are done in the fixed update
    {
        GravityMultipliers();

        if (!isHanging) //if the player is not hanging then he can move
        {
            Vector3 movement = transform.TransformDirection(new Vector3(xAxis, 0, zAxis) * currentSpeed);
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z); //Adjust the velocity of the player to make it move

            if (im.movement == Vector2.zero) //If the player is not moving
            {
                currentSpeed = 0;//We set its speed to 0
            }
            else
            {
                currentSpeed = playerWalkSpeed;//Else its speed is the walk speed
            }

            if (isJumping) //If the player is pressing the jump key
            {

                if (IsGrounded())
                {
                    Jump();
                    StartCoroutine(DoubleJumpTimer());
                }
                else
                {
                    if (canDoubleJump)
                    {
                        canDoubleJump = false;
                        Jump();
                    }
                }
            }
        }
        else
        {
            if (isJumping)
            {
                anim.SetBool("isClimbing", true);
            }
        }
    }

    public void ShowMenu()
    {
        pauseMenuObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        isPaused = true;
    }

    #region Basic Movements
    IEnumerator DoubleJumpTimer()
    {
        yield return new WaitForSeconds(.35f);
        canDoubleJump = true;
    }

    private void GravityMultipliers() //Function used to make the jump feels good and not floating
    {
        if (rb.velocity.y < 0) //If we are falling
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !isJumping) //if we are jumping up
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private bool IsGrounded() //Function made to check if the player is on the ground
    {
        bool value = false;
        value = Physics.CheckSphere(feetTransform.position, groundCheckRadius, whatIsGround);
        return value;
    }

    private void Jump() //Function that makes the player jump
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + jumpForce, rb.velocity.z);
        isJumping = false;
    }

    #endregion

    private void MouseLook() //The function allowing the player to look around with its mouse
    {
        Vector2 mouseDir = im.lookValue; //Get the mouse movements

        mouseDir = Vector2.Scale(mouseDir, new Vector2(sensitivity / 5, sensitivity / 5)); //apply a sensitivity

        //This part allow a smooth movement of the camera
        Vector2 lookDelta = new Vector2();
        lookDelta.x = Mathf.Lerp(lookDelta.x, mouseDir.x, 1f / lookSmooth);
        lookDelta.y = Mathf.Lerp(lookDelta.y, mouseDir.y, 1f / lookSmooth);
        lookDirection += lookDelta;


        lookDirection.y = Mathf.Clamp(lookDirection.y, -45f, 45f); //Prevent the player from doing rollover vertically with the camera

        playerCam.transform.localRotation = Quaternion.AngleAxis(-lookDirection.y, Vector3.right); //change the vertical rotation of the camera

        transform.localRotation = Quaternion.AngleAxis(lookDirection.x, this.transform.up); //change the horizontal rotation of the player

        //if(xAxis == 0)
        //{
        //    float targetAngle = Mathf.Atan2(im.movement.x, im.movement.y) * Mathf.Rad2Deg + playerCam.transform.eulerAngles.y;
        //    Quaternion rotation = Quaternion.Euler(new Vector3(0, targetAngle, 0));
        //    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * sensitivity);
        //}
    }


    #region LedgeGrabing stuff

    public void LedgeGrab(Vector3 snapPoint, Ledge currentLedge)
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        GetComponent<CapsuleCollider>().enabled = false;
        transform.position = snapPoint;
        // Everything below is fluff and polish

        anim.SetFloat("currentSpeed", 0.0f);
        isHanging = true;
        anim.SetBool("isHanging", isHanging);
        activeLedge = currentLedge;
    }

    public void FinishedClimbing()
    {
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
        GetComponent<CapsuleCollider>().enabled = true;
        transform.position = activeLedge.GetStandPos();
        // Everything below is fluff and polish

        anim.SetFloat("currentSpeed", 0.0f);
        isHanging = false;
        anim.SetBool("isHanging", false);
        anim.SetBool("isClimbing", false);
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(feetTransform.position, groundCheckRadius);
    }

}
