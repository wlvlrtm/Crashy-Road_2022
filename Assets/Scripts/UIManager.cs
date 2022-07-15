using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public static UIManager instance;
    private string playerName;
        public string PlayerName {
            get { return this.playerName; }
            set { this.playerName = value; }
        }


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

    public void SceneCall(string sceneName) {
        SceneLoader.instance.LoadScene(sceneName);
    }

    public void NameSave(string name) {
        this.playerName = name;
        Debug.Log(this.playerName);
    }

    public void ObjActive(GameObject target) {
        target.SetActive(true);
    }

    public void ObjDeActive(GameObject target) {
        target.SetActive(false);
    }

    public void GameQuit() {
        Application.Quit();
    }












}

