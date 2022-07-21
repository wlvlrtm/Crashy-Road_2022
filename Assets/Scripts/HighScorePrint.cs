using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class HighScorePrint : MonoBehaviour {
    [System.Serializable]
    class SaveData {
        public string playerName;
        public int highScore;
    }


    private int highScore;
    private string playerName;


    private void Init() {
        LoadHighScore();
        GetComponent<TMP_Text>().SetText(this.playerName + " - " + this.highScore);
    }

    private void Awake() {
        Init();
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
}
