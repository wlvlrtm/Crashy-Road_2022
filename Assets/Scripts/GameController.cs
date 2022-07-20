using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public PlayerController playerController;
    public GameObject gameOverWindow;
    public Image HPBar;
    public TMP_Text scoreText;
    public TMP_Text coolTimeText;
    private bool isGameOver;
    

    private void Init() {
        if (instance == null) {
            instance = this;
        }

        this.isGameOver = false;
        this.HPBar.fillAmount = 1;
    }

    private void Awake() {
        Init();
        StartCoroutine(ScoreCounter());
        StartCoroutine(CoolTimeCounter());
    }

    private void Update() {
        HPBarControl();
    }

    public void GameOver() {
        if (!this.isGameOver) {
            this.gameOverWindow.SetActive(true);
        }

        this.isGameOver = true;
    }

    IEnumerator ScoreCounter() {
        while (!this.isGameOver) {
            this.scoreText.SetText(playerController.Score+"p");
            yield return null;
        }
    }

    public void ItemGetpoint(int score) {
        playerController.Score += score;
    }

    IEnumerator CoolTimeCounter() {
        while (!this.isGameOver) {
            this.coolTimeText.SetText(playerController.CoolDownTimer+"s");
            yield return null;
        }
    }

    private void HPBarControl() {
        this.HPBar.fillAmount = playerController.Life * 0.1f;
    }




}