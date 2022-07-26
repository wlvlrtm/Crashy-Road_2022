using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public PlayerController playerController;
    public GameObject gameOverWindow;
    public Image HPBar;
    public TMP_Text scoreText;
    public TMP_Text coolTimeText;
    public TMP_Text highScoreText;
    private bool isGameOver;
    private int highScore;
        public int HighScore {
            get { return this.highScore; }
        }
    private int __hightScore;
    private string playerName;
        public string PlayerName {
            get { return this.playerName; }
            set { this.playerName = value; }
        }

    [System.Serializable]
    class SaveData {
        public string playerName;
        public int highScore;
    }


    private void Init() {
        if (instance == null) {
            instance = this;
        }

        this.isGameOver = false;
        this.HPBar.fillAmount = 1;
        
        LoadHighScore();
    }

    private void Awake() {
        Init();
        StartCoroutine(ScoreCounter());
        StartCoroutine(CoolTimeCounter());
    }

    private void Update() {
        HPBarControl();
        HighScoreControl();
    }

    private void SaveHighSore() {
        SaveData saveData = new SaveData();

        if (saveData.highScore < this.__hightScore) {
            saveData.highScore = this.__hightScore;
            saveData.playerName = GameInfo.instance.id;

            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    }

    private void LoadHighScore() {
        string path = Application.persistentDataPath + "/savefile.json";
        
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            this.highScore = saveData.highScore;
            this.playerName = saveData.playerName;
        }
    }

    public void GameOver() {
        // TODO: High-Score Save; json
        SaveHighSore();

        if (!this.isGameOver) {
            BGMPlayer.instance.Pause();
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

    private void HighScoreControl() {
        if (this.highScore < playerController.Score) {
            this.highScoreText.SetText("High-Score: " + playerController.Score);
            this.__hightScore = playerController.Score;
        }
        else {
            this.highScoreText.SetText("High-Score: " + this.highScore);
        }
    }
}