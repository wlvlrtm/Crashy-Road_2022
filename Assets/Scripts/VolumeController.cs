using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour {
    public GameObject volumeButton;
    public GameObject muteButton;
    public AudioMixer mixer;


    public void VolumeOff() {
        this.muteButton.SetActive(true);
        this.mixer.SetFloat("masterVol", -80);
        gameObject.SetActive(false);
    }

    public void VolumeOn() {
        this.volumeButton.SetActive(true);
        this.mixer.SetFloat("masterVol", 0);
        this.gameObject.SetActive(false);
    }
}