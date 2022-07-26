using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour {
    private void OnEnable() {
        BGMPlayer.instance.Pause();
        Time.timeScale = 0;       
    }

    private void OnDisable() {
        BGMPlayer.instance.Resume();
        Time.timeScale = 1;
    }
}
