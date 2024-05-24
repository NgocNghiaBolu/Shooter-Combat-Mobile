using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool run;
    public bool shoot;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    public void OnRun(InputValue value)
    {
        RunInput(value.isPressed);
    }

    public void RunInput(bool newSprintState)
    {
        run = newSprintState;
    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void JumpInput(bool newJumpState)
    {

        jump = newJumpState;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
