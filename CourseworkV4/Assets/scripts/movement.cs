using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour

//code inspiration for movement and camera adapted from  - https://www.youtube.com/watch?v=4HpC--2iowE
//code inspiration for gravity and jumping adapted from - https://www.youtube.com/watch?v=_QajrabyTJc
//animation tutorial - https://www.youtube.com/watch?v=2_Hn5ZsUIXM
//some of the lines of code involving some complex math and calculations were taken from these links and adpated to fit my game.

//Player and zombie models + animations from miximo.
{
    //varaibles - animator, controller, camera
    private Animator animator;
    public CharacterController controller;
    public Transform camera;

    //gravity and jump height
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    //to be implemented 
    public float lowGravMode;
    public float doubleJumpHeight; 

    //used to check if the player is on the ground.
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    //variables for animation states 
    bool isGrounded;
    bool IsGrounded;
    bool isJumping;

    //player speed
    public float speed = 6f;
    //to be implemented
    public float sprintSpeed;
    public float crouchSpeed;

    //to make the player turn smoothly 
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Start()
    { 
        //access the animator component
        animator = GetComponent<Animator>();
 
    }

    // Update is called once per frame
    void Update()
    {
        //check if the player is grounded by generating a sphere at the base of the player
        //and check if it is touching the ground 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //if the player is on the ground reset the velocity because velcoity accelerates
        if (velocity.y < 0 && isGrounded)
        { 
            velocity.y = -1f;
            //animation states
            animator.SetBool("isGrounded", true);
            IsGrounded = true;
            animator.SetBool("isJumping", false);
            isJumping = false;
            animator.SetBool("isFalling", false);
        }
        else
        {
            //if the player is not on the ground play falling animation
            animator.SetBool("isGrounded", false);
            IsGrounded = false;

            //if the players velocity is negative = they are falling - play fall animation
            if ((isJumping && velocity.y < 0 || velocity.y < -2f))
            {
                animator.SetBool("isFalling", true);
            }
        }


        //gets the input for moving the player
        float leftRight = Input.GetAxisRaw("Horizontal");
        float upDown = Input.GetAxisRaw("Vertical");
        //calculate the direction in 3d space using above then normalize so that moving diagonal
        //doesnt make player move faster. 
        Vector3 direction = new Vector3(leftRight, 0f, upDown).normalized;

        //if the jump button is pressed and the player is on the ground then jump.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //math to calculate velocity increase with gravity. 
            //sqrt = square root 
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            //animation states 
            animator.SetBool("isJumping", true);
            isJumping = true;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //if the player is moving...
        if (direction.magnitude >= 0.05f)
        { 
            //animation state
            animator.SetBool("isMoving", true);

            //math for calculating player rotation so the model faces the correct direction that the player is moving
            //atan2 function calculates the angle between x and z direction then convert to degrees.
             
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            //makes rotations smooth 
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //calculates the move direction using above variables and moves the player
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else 
        {
            //animator - player no longer moving
            animator.SetBool("isMoving", false);
        }

        
    }

    //method to catch when the player collides with a zombie, reset them and reduce score.
    void OnTriggerEnter(Collider other)
        {
            //collision is with a zombie object
            if (other.gameObject.tag == "zombies")
            {
                //disable the player controller
                //Without this the movement script takes over and the transform will not be complete
                controller.enabled = false;
                //debugging
                Debug.Log("zombie collide");
                //reset the player position
                transform.position = new Vector3(4.15999985f ,4.9000001f ,41.7999992f);
                //re-enable the player controller once they have been reset.
                controller.enabled = true;
                //call lose score method
                scoreBehaviour.LoseScore();
            }
        }

}
