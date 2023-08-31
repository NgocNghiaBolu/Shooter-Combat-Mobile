    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookMouse : MonoBehaviour
{
    [Header("Min & Max Camera View")]
    private const float Ymin = -50f;
    private const float Ymax = 50f;

    [Header("Camera View")]
    public Transform lookAt;
    public Transform player;

    [Header("Camera Position")]
    public float CamDistance = 2f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float CameraSensitivity = 4f;

    public FloatingJoystick floatJoystick;

    private void LateUpdate()
    {
        currentX += floatJoystick.Horizontal * CameraSensitivity * Time.deltaTime;
        currentY -= floatJoystick.Vertical * CameraSensitivity * Time.deltaTime;

        currentY = Mathf.Clamp(currentY, Ymin, Ymax);

        Vector3 direction = new Vector3(0, 0, -CameraSensitivity);//vi de player ben trai nen khi zoom lai se thien ve ben trai hon
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = lookAt.position + rotation * direction;   
        transform.LookAt(lookAt.position);
    }