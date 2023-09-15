using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Rigidbody2D pivot;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float respawnDelay;

    private Rigidbody2D currentBall;
    private SpringJoint2D currentSpringJoint;
    private bool isDragging;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewBall();
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
        Invoke(nameof(SpawnNewBall), respawnDelay);
       // Invoke(nameof(DestroyBall),2f);
    }

    private void DestroyBall()
    {
        Destroy(currentBall.gameObject);
    }

    private void SpawnNewBall()
    {
       GameObject ballInstance = Instantiate(ballPrefab,pivot.transform.position, Quaternion.identity);
        currentBall = ballInstance.GetComponent<Rigidbody2D>();
        currentSpringJoint = ballInstance.GetComponent<SpringJoint2D>();
        currentSpringJoint.connectedBody = pivot;
    }
}
