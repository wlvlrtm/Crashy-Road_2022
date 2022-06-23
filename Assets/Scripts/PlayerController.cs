using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
// SerializeField: You can change that variables value in the Inspector, but you cannot do it in other scripts.
    [SerializeField] private float horsePower = 20f;
    [SerializeField] private float rotateSpeed = 45f;
    [SerializeField] private float speed;
    [SerializeField] private float rpm;
    [SerializeField] private GameObject centerOfMass;
    [SerializeField] private int wheelsOnGround;    
    [SerializeField] private List<WheelCollider> wheels;

    private float horizontalInput;
    private float verticalInput;
    private Rigidbody playerRb;
     

    private void Init() {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.localPosition;
    }

    private void Awake() {
        Init();
    }

    private void FixedUpdate() {
        Driving();
    }

    private void Driving() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (IsOnGround()) {
            // Accel
            playerRb.AddRelativeForce(Vector3.forward * -verticalInput * horsePower);
        }

        // Steering wheel (4 wheel)
        wheels[0].steerAngle = horizontalInput * rotateSpeed;
        wheels[1].steerAngle = horizontalInput * rotateSpeed;
        wheels[2].steerAngle = -horizontalInput * (rotateSpeed/3);
        wheels[3].steerAngle = -horizontalInput * (rotateSpeed/3);
    }

    private bool IsOnGround() {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in wheels) {
            if (wheel.isGrounded) {
                wheelsOnGround++;
            }
        }
        if (wheelsOnGround == 4) {
            return true;
        }
        else {
            return false;
        }
    }
}
