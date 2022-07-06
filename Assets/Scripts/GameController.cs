using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance;

    
    private void Init() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Awake() {
        Init();
    }

    public void GameOver() {
        Debug.Log("GameOver");
    }
}