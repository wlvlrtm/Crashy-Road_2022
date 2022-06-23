using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemyController enemyController;


    private void OnTriggerEnter(Collider other) {
        other.gameObject.SetActive(false);
        
        switch (other.gameObject.tag) {
            case "Player" :
                playerController.Death();
                break;
            case "Enemy" :
                enemyController.Reset();
                break;

        }

    }
}
