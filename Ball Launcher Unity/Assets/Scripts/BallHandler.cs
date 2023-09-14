using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Rigidbody2D currentBall;
    [SerializeField]
    private SpringJoint2D currentSpringJoint;

    private bool isDragging;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBall == null) { return; }

        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (isDragging)
            {
                LaunchBall();
            }
            isDragging = false;

            return;
        }
        
        currentBall.isKinematic = true;
        Vector2 mousePos =  Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 worldPos = mainCamera .ScreenToWorldPoint(mousePos);
        currentBall.position = worldPos;
        isDragging = true;
    }

    private void LaunchBall()
    {
        currentBall.isKinematic = false;
        currentBall = null;
        Invoke("DetachBall", 0.15f);
    }

    private void DetachBall()
    {
        currentSpringJoint.enabled = false;
        currentSpringJoint = null;
    }
}
