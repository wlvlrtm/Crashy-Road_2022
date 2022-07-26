using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour {
    public static VFXController instance;


    private void Init() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Awake() {
        Init();
    }

    public void Play(Vector3 position) {
        transform.position = position;
        GetComponent<ParticleSystem>().Play();
    }
}
