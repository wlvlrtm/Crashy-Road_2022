using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public PlayerController playerController;
    public GameObject gameOverWindow;
    public TMP_Text scoreText;
    public TMP_Text coolTimeText;
    private bool isGameOver;
    

    private void Init() {
        if (instance == null) {
            instance = this;
        }

        isGameOver = false;
    }

    private void Awake() {
        Init();
        StartCoroutine(ScoreCounter());
        StartCoroutine(CoolTimeCounter());
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
}