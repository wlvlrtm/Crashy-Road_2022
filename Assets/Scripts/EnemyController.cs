using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    [SerializeField] private int life;
    [SerializeField] private int restTime;
    private Coroutine runningCoroutine;
    private AIDriving aIDriving;


    private void Init() {
        this.runningCoroutine = null;
        this.aIDriving = GetComponent<AIDriving>();
    }

    private void Awake() {
        Init();        
    }

    private void OnDisable() {
        PosReset();
    }

    private void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) {
            case "Player" :
            case "Building" :
            case "Enemy" :
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
        this.aIDriving.throttle = 0;
        StartCoroutine(Disable());

    }

    public void PosReset() {
        // Car Pos. Reset
        transform.Translate(0, 0, 0);
    }

    IEnumerator Disable() {
        // Car Reset Timer
        while (this.restTime > 0) {
            this.restTime -= 1;
            yield return new WaitForSeconds(1);
        }

        gameObject.SetActive(false);    // Car Disable
    }
}
