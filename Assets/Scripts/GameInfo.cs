using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour {
    public static GameInfo instance;

    public string id;
    public string map;
    public string car;


    private void Init() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Awake() {
        Init();
    }
}
