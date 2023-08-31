using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwicherCamera : MonoBehaviour
{
    [Header("Camera to Assign")]
    public GameObject AimCanVas;//ngam ban
    public GameObject AimCam;
    public GameObject TPCam;
    public GameObject TPCanvas;// khong ngam
    public PlayerMoverment player;

    [Header("Camera Animator")]
    public Animator animator;

    private void Update()
    {
        if(Input.GetButton("Fire2") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("Walk", true);
            animator.SetBool("WalkAim", true);

            AimCanVas.SetActive(true);
            AimCam.SetActive(true);
            TPCam.SetActive(false);
            TPCanvas.SetActive(false);
        }
        else if(Input.GetButton("Fire2"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("Walk", false);
            animator.SetBool("WalkAim", false);

            AimCanVas.SetActive(true);
            AimCam.SetActive(true);
            TPCam.SetActive(false);
            TPCanvas.SetActive(false);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("IdleAim", false);
            animator.SetBool("WalkAim", false);

            AimCanVas.SetActive(false);
            AimCam.SetActive(false);
            TPCam.SetActive(true);
            TPCanvas.SetActive(true);
        } 
    }
}
