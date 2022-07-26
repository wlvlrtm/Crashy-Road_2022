using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour {
    public static BGMPlayer instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] BGMList;
    private AudioClip bgm;


    private void Init() {
        if (instance == null) {
            instance = this;
        }

        int index = Random.Range(0, BGMList.Length);
        this.bgm = BGMList[index];
        audioSource.volume = 0.3f;
        audioSource.PlayOneShot(this.bgm);
    }

    private void Awake() {
        Init();
    }

    public void Pause() {
        audioSource.Pause();
    }

    public void Resume() {
        audioSource.PlayOneShot(this.bgm);
    }
}
