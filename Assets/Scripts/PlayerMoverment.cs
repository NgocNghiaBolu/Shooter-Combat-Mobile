using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.InputSystem;


public class PlayerMoverment : MonoBehaviour
{
    [Header("Player Moverment")]
    public float playerSpeed = 2f;
    public float currentPlayerSpeed = 0f;
    public float playerRun = 3f;
    public float currentPlayerRun = 0f;
    

    [Header("CameraPlayer")]
    public Transform CameraPlayer;

    [Header("Player Animaton anh Gravity")]
    public CharacterController characControl;
    public float gravity = -10f;
    public Animator animator;

    [Header("Jump and Velocity")]
    public float jumpRange = 1f;
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;
    Vector3 velocity;
    public Transform checkSurface;
    bool OnSurface;
    public float surfacedistanc = 0.5f;
    public LayerMask surfaceMask;

    public bool mobileJoystick;
    public FixedJoystick joystick;
    public FixedJoystick Runjoystick;
    //public UICanvasControllerInput button;
    //UIVirtualButton bt;

    public bool jump;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//khoa con tro
    }

    private void Update()
    {
        if(currentPlayerSpeed > 0)
        {
            Runjoystick = null;
        }
        else
        {
            FixedJoystick runJS = GameObject.Find("RunJoystick").GetComponent<FixedJoystick>();
            Runjoystick = runJS;
        }

        OnSurface = Physics.CheckSphere(checkSurface.position, surfacedistanc, surfaceMask);

        if(OnSurface && velocity.y < 0)//kiem tra xem player co dang tren mat dat k
        {
            velocity.y = -2f;
        }
        //Gravity
        velocity.y += gravity * Time.deltaTime;
        characControl.Move(velocity * Time.deltaTime);//neu khong thi rot tu tu xuong

        PlayerMove();

        PlayerRun();

        Jump();
    }

    void PlayerMove()
    {
        if (mobileJoystick == true)
        {
            float horizontalAxis = joystick.Horizontal;//khai bao cac phuong huong di
            float verticalAxis = joystick.Vertical;

            Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;//cho cac phuong vao bien direc

            if (direction.magnitude >= 0.1f)//neu do lon va huong di >= 0.1
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Walk", true);
                animator.SetBool("IdleAim", false);
                animator.SetBool("WalkAim", false);
                animator.SetBool("Run", false);
                animator.SetTrigger("Jump");

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + CameraPlayer.eulerAngles.y;//xoay theo huong chon
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characControl.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);//cho dchuyen 
                currentPlayerSpeed = playerSpeed;
            }
            else
            {
                animator.SetBool("Idle", true);
                animator.SetBool("Walk", false);
                animator.SetBool("Run", false);
                animator.SetBool("WalkAim", false);
                animator.SetTrigger("Jump");
                currentPlayerSpeed = 0f;
            }
        }
        else
        {
            float horizontalAxis = Input.GetAxisRaw("Horizontal");//khai bao cac phuong huong di
            float verticalAxis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;//cho cac phuong vao bien direc

            if (direction.magnitude >= 0.1f)//neu do lon va huong di >= 0.1
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Walk", true);
                animator.SetBool("IdleAim", false);
                animator.SetBool("WalkAim", false);
                animator.SetBool("Run", false);
                animator.SetTrigger("Jump");

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + CameraPlayer.eulerAngles.y;//xoay theo huong chon
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characControl.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);//cho dchuyen 
                currentPlayerSpeed = playerSpeed;
            }
            else
            {
                animator.SetBool("Idle", true);
                animator.SetBool("Walk", false);
                animator.SetBool("Run", false);
                animator.SetBool("WalkAim", false);
                animator.SetTrigger("Jump");
                currentPlayerSpeed = 0f;
            }
        }
    }

    void PlayerRun()
    {
        if (mobileJoystick == true)
        {
            float horizontalAxis = Runjoystick.Horizontal; //khai bao cac phuong huong di
            float verticalAxis = Runjoystick.Vertical;

            Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;//cho cac phuong vao bien direc

            if (direction.magnitude >= 0.1f)//neu do lon va huong di >= 0.1
            {
                animator.SetBool("Run", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Walk", false);
                animator.SetBool("IdleAim", false);

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + CameraPlayer.eulerAngles.y;//xoay theo huong chon
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characControl.Move(moveDirection.normalized * playerRun * Time.deltaTime);//cho dchuyen 
                currentPlayerRun = playerRun;
            }
            else
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Walk", false);
                currentPlayerRun = 0f;
            }
        }
        else
        {
            float horizontalAxis = Input.GetAxisRaw("Horizontal");//khai bao cac phuong huong di
            float verticalAxis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;//cho cac phuong vao bien direc

            if (direction.magnitude >= 0.1f)//neu do lon va huong di >= 0.1
            {
                animator.SetBool("Run", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Walk", false);
                animator.SetBool("IdleAim", false);

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + CameraPlayer.eulerAngles.y;//xoay theo huong chon
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characControl.Move(moveDirection.normalized * playerRun * Time.deltaTime);//cho dchuyen 
                currentPlayerRun = playerRun;
            }
            else
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Walk", false);
                currentPlayerRun = 0f;
            }
        }
    }

    void Jump()
    {
             
        if (Input.GetButtonDown("Jump") && OnSurface)
            {
                animator.SetBool("Walk", false);
                animator.SetTrigger("Jump");
                velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
            }
            else
            {
                animator.ResetTrigger("Jump");
            }
        
    }
}
