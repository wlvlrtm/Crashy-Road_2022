using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private GameController gameController;
    [SerializeField] private int life;
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
    private Vector3 startPos;
     

    private void Init() {
        this.playerRb = GetComponent<Rigidbody>();
        this.playerRb.centerOfMass = this.centerOfMass.transform.localPosition;
        this.startPos = gameObject.transform.position;
    }

    private void Awake() {
        Init();
    }

    private void FixedUpdate() {
        Driving();
    }

    private void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag) {
            case "Enemy" :
            case "Building" :
                Hit();
                break;
        }
    }

    private void Hit() {
        this.life -= 1;

        if (this.life <= 0) {
            Crash();
        }
    }

    private void Crash() {
        this.horsePower = 0;    // Engine OFF

        for (int i = 0; i < 4; i++) {   // Wheel Disable
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Death() {
        gameController.GameOver();  // GameOver!!
    }

    private void Driving() {
        this.horizontalInput = Input.GetAxis("Horizontal");
        this.verticalInput = Input.GetAxis("Vertical");

        if (IsOnGround()) {
            // Accel
            playerRb.AddRelativeForce(Vector3.forward * -this.verticalInput * this.horsePower);
        }

        // Steering wheel (4 wheel)
        this.wheels[0].steerAngle = this.horizontalInput * this.rotateSpeed;
        this.wheels[1].steerAngle = this.horizontalInput * this.rotateSpeed;
        this.wheels[2].steerAngle = -this.horizontalInput * (this.rotateSpeed / 3);
        this.wheels[3].steerAngle = -this.horizontalInput * (this.rotateSpeed / 3);
    }

    private bool IsOnGround() {
        this.wheelsOnGround = 0;

        foreach (WheelCollider wheel in this.wheels) {
            if (wheel.isGrounded) {
                this.wheelsOnGround++;
            }
        }

        if (this.wheelsOnGround == 4) {
            return true;
        }
        else {
            return false;
        }
    }
}
