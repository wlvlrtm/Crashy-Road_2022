using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {
    public PlayerController playerController;
    public CameraController cameraController;


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            playerController.IsDeath = true; 
            cameraController.Target = null;
        }
        else {
            other.gameObject.SetActive(false);
        }
    }
}
