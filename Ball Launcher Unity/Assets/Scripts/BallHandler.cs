using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Rigidbody2D currentBall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            
            currentBall.isKinematic = false;
            return;
        }

        currentBall.isKinematic = true;
        Vector2 mousePos =  Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 worldPos = mainCamera .ScreenToWorldPoint(mousePos);
        currentBall.position = worldPos;
    }
}
