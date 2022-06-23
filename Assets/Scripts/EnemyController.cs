using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    [SerializeField] private int life;
    public int Life { 
        get { return life; }
        set { this.life = value; } 
    }
    [SerializeField] private int restTime;
    [SerializeField] private GameObject enemySpawner;

    private Coroutine runningCoroutine;


    private void Init() {
        runningCoroutine = null;
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
        this.Life -= 1;
        if (this.Life <= 0) {
            Death();
        }
    }

    private void Death() {
        GetComponent<AIDriving>().throttle = 0;
        if (runningCoroutine == null) {
            runningCoroutine = StartCoroutine(CarReset());
        }
    }

    IEnumerator CarReset() {
        // Car Reset Timer
        while (restTime > 0) {
            restTime -= 1;
            yield return new WaitForSeconds(1);
        }

        // Car Pos. Reset; Car Disable
        gameObject.SetActive(false);
        gameObject.transform.position = enemySpawner.transform.position;
    }
}
