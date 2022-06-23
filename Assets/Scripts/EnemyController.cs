using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    [SerializeField] private int life;
    [SerializeField] private int restTime;
    [SerializeField] private GameObject enemySpawner;
    private Coroutine runningCoroutine;


    private void Init() {
        this.runningCoroutine = null;
    }

    private void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) {
            case "Player" :
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
        GetComponent<AIDriving>().throttle = 0;

        if (this.runningCoroutine == null) {
            this.runningCoroutine = StartCoroutine(ResetTimer());
        }
    }

    public void Reset() {
        // Car Pos. Reset
        gameObject.transform.position = this.enemySpawner.transform.position;
    }

    IEnumerator ResetTimer() {
        // Car Reset Timer
        while (this.restTime > 0) {
            this.restTime -= 1;
            yield return new WaitForSeconds(1);
        }

        gameObject.SetActive(false);    // Car Disable
        Reset();
    }
}
