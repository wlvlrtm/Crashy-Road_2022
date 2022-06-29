using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChasing : MonoBehaviour {
    [SerializeField] private float speed;
        public float Speed {
            set { this.speed = value; }
        }
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameObject target;
    [SerializeField] private Transform right;
    [SerializeField] private Transform left;
    [SerializeField] private float detectDistance;
    [SerializeField] private float chaseAngle;
    [SerializeField] private int wheelsOnGround;
    [SerializeField] private List<WheelCollider> wheels;
    [SerializeField] private GameObject red;
    [SerializeField] private GameObject blue;

    private Rigidbody enemyRb;
    private Vector2 chaseAngleRange = new Vector2(0, 30);
    private float speedVariation = 2;
    private float currentRotation = 0;
    private float lightTime;
    private bool lightChange;

    private void Init() {
        this.enemyRb = GetComponent<Rigidbody>();
        this.chaseAngle = Random.Range(this.chaseAngleRange.x, this.chaseAngleRange.y);
        this.speed += Random.Range(0, this.speedVariation);

        // Engine ON
        foreach (WheelCollider wheel in wheels) {
            wheel.motorTorque = 0.000001f;
        }
    }

    private void Awake() {
        Init();    
    }

    private void FixedUpdate() {
        Driving();
        SirenLightControl();
    }

    private void Driving() {
        // transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);  // Car Move; forward
        
        if (IsOnGround()) {
            this.enemyRb.AddRelativeForce(Vector3.forward * this.speed);
            Ray rayR = new Ray(this.right.position, this.right.forward);
            Ray rayL = new Ray(this.left.position, this.left.forward);

            RaycastHit hit;

            if (Physics.Raycast(rayL, out hit, detectDistance)) {       // Obstacle detected on left side
                Rotate(1);
            }
            else if (Physics.Raycast(rayR, out hit, detectDistance)) {  // Obstacle detected on right side
                Rotate(-1);
            }
            else {
                if (Vector3.Angle(transform.forward, this.target.transform.position - transform.position) > this.chaseAngle) {
                    Rotate(ChaseAngle(transform.forward, this.target.transform.position - transform.position, Vector3.up));
                }
                else {
                    Rotate(0);
                }
            }
        }
    }

    private void Rotate(float rotateDirection) {
        if (rotateDirection != 0) {
            transform.localEulerAngles += Vector3.up * rotateDirection * rotateSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
    
    private float ChaseAngle(Vector3 forward, Vector3 targetDirection, Vector3 up) {
        float approachAngle = Vector3.Dot(Vector3.Cross(up, forward), targetDirection);

        if (approachAngle > 0f) {
            return 1f;
        }
        else if (approachAngle < 0f) {
            return -1f;
        }
        else {
            return 0f;
        }
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

    private void SirenLightControl() {
        // Simple script to flash the lights, probably a better way to do this
        this.lightTime += 1;
        if (this.lightTime > 10) {
            this.lightTime = 0;

            if (this.lightChange == false) {
                this.lightChange = true;
                this.blue.SetActive(true);
                this.red.SetActive(false);
            }
            else {
                this.lightChange = false;
                this.blue.SetActive(false);
                this.red.SetActive(true);
            }
        }
    }
}
