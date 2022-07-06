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

    /*
    [SerializeField] private int arrestGauge;
        public int ArrestGauge {
            get { return this.arrestGauge; }
            set { this.arrestGauge = value; }
        }
    */
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

    
    /*
    private int arrestGaugeStep;
        public int ArrestGaugeStep {
            get { return this.arrestGaugeStep; }
            set { this.arrestGaugeStep = value; }
        }
    */
    private bool isDeath;
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
        //this.arrestGaugeStep = 10;

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
        //GameController.instance.GameOver();    
    }

    private void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag) {
            //case "Enemy" :
            case "Building" :
                Hit();
                break;
        }
    }

    /*
    private void OnTriggerEnter(Collider other) {
        switch (other.gameObject.tag) {
            case "Enemy" :
                Arrest(true); 
                break;
        }
    }

    private void OnTriggerExit(Collider other) {
        switch (other.gameObject.tag) {
            case "Enemy" :
                Arrest(false);
                break;
        }
    }
    */

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
            //GameController.instance.GameOver();
        }
    }

    IEnumerator ScoreCounter() {
        while (!this.isDeath) {
            this.score += 1;
            yield return new WaitForSeconds(1);
        }
    }

    /*
    private void Arrest(bool type) {
        if (this.runningCoroutine != null) {
            StopCoroutine(this.runningCoroutine);
        }
        
        this.runningCoroutine = StartCoroutine(__Arrest(type));
    }

    IEnumerator __Arrest(bool isArresting) {
        if (isArresting) {  // Gauge Decrease
            while (this.arrestGauge > 0) {
                this.arrestGauge -= this.arrestGaugeStep;
                yield return new WaitForSeconds(1);
            }
        }
        else if (!isArresting) {    // Gauge Increase
            while (this.arrestGauge < 100) {
                this.arrestGauge += this.arrestGaugeStep;
                yield return new WaitForSeconds(1);
            }
        }
        
        if (this.arrestGauge <= 0) {    // GameOver
            this.isDeath = true;
            this.gameController.GameOver();
        }
    }
    */

    private void Hit() {
        this.life -= 1;
        // SOUND FX
    }

    private void Crash() {
        if (this.life <= 0 || this.isDeath) {
            this.horsePower = 0;    // Engine OFF

            for (int i = 0; i < 4; i++) {   // Wheel Disable
                transform.GetChild(i).gameObject.SetActive(false);
            }
            
            this.isDeath = true;    // Death
            //GameController.instance.GameOver();
        }
    }

    private void Driving() {
        this.horizontalInput = Input.GetAxis("Horizontal");
        //this.verticalInput = Input.GetAxis("Vertical");
        
        // Accel
        if (IsOnGround()) {
            this.playerRb.AddRelativeForce(Vector3.forward * -1 * this.horsePower);          
        }
        
        //transform.position += this.moveForce;

        // Steering wheel (4 wheels)
        if (this.playerRb.velocity.magnitude > 0.5f) {
            transform.Rotate(Vector3.up * this.horizontalInput * this.rotateSpeed * Time.deltaTime);
            
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
        
        
        // this.wheels[0].steerAngle = this.horizontalInput * this.rotateSpeed;
        // this.wheels[1].steerAngle = this.horizontalInput * this.rotateSpeed;
        // this.wheels[2].steerAngle = -this.horizontalInput * (this.rotateSpeed / 3);
        // this.wheels[3].steerAngle = -this.horizontalInput * (this.rotateSpeed / 3);
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
