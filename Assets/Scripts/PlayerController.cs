using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private int life;
        public int Life {
            get { return this.life; }
            set { this.life = value; }
        }
    [SerializeField] private int coolDownTimer;
        public int CoolDownTimer {
            get { return this.coolDownTimer; }
            set { this.coolDownTimer += value; }
        }
    [SerializeField] private int score;
        public int Score {
            get { return this.score; }
            set { this.score = value; }
        }
    [SerializeField] private float horsePower = 20f;
        public float HorsePower {
            get { return this.horsePower; }
            set { this.horsePower = value; }
        }
    [SerializeField] private float rotateSpeed = 45f;
        public float RotateSpeed {
            get { return this.rotateSpeed; }
            set { this.rotateSpeed = value; }
        }
    [SerializeField] private GameObject centerOfMass;
    [SerializeField] private int wheelsOnGround;    
    [SerializeField] private List<WheelCollider> wheels;
    [SerializeField] private TrailRenderer skidRL;
    [SerializeField] private TrailRenderer skidRR;

    private bool isDeath;
        public bool IsDeath {
            get { return this.isDeath; }
            set { this.isDeath = value; }
        }
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody playerRb;
    private Vector3 startPos;
    private Vector3 moveForce;
    private Coroutine runningCoroutine;

     
    private void Init() {
        this.runningCoroutine = null;
        this.playerRb = GetComponent<Rigidbody>();
        this.playerRb.centerOfMass = this.centerOfMass.transform.localPosition;
        this.startPos = gameObject.transform.position;
        this.isDeath = false;

        // Score Count Start
        StartCoroutine(ScoreCounter());

        // CoolDown
        StartCoroutine(CoolDownCounter());

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
        Crash();
        PlayerDeath();
    }

    private void OnDisable() {
        this.isDeath = true;
    }

    private void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag) {
            case "Building" :
            case "NPCCar" :
                Hit();
                break;
        }
    }

    private void PlayerDeath() {
        if (this.isDeath == true) {
            GameController.instance.GameOver();
        }
    }

    IEnumerator CoolDownCounter() {
        while (this.coolDownTimer > 0 && !this.isDeath) {
            this.coolDownTimer -= 1;
            yield return new WaitForSeconds(1);
        }

        if (this.coolDownTimer <= 0) {
            this.isDeath = true;
        }
    }

    IEnumerator ScoreCounter() {
        while (!this.isDeath) {
            this.score += 1;
            yield return new WaitForSeconds(1);
        }
    }

    private void Hit() {
        this.life -= 1;
        this.coolDownTimer -= 5;
        // SOUND FX
    }

    private void Crash() {
        if (this.life <= 0 || this.isDeath) {
            this.horsePower = 0;    // Engine OFF

            for (int i = 0; i < 4; i++) {   // Wheel Disable
                transform.GetChild(i).gameObject.SetActive(false);
            }
            
            this.isDeath = true;    // Death
        }
    }

    private void Driving() {
        this.horizontalInput = Input.GetAxis("Horizontal");
        
        // Steering wheel (4 wheels)
        if (this.playerRb.velocity.magnitude > 0.5f) {
            transform.Rotate(Vector3.up * this.horizontalInput * this.rotateSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {   // Break; Key: S, down Arrow
            this.playerRb.AddRelativeForce(Vector3.forward * 1 * (this.horsePower / 2));
        }
        else if (IsOnGround()) {    // Accel
            this.playerRb.AddRelativeForce(Vector3.forward * -1 * this.horsePower);          
        }

        // Skid VFX
        if (this.horizontalInput != 0) {
            this.skidRL.emitting = true;
            this.skidRR.emitting = true;
        }
        else {
            this.skidRL.emitting = false;
            this.skidRR.emitting = false;
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
}
