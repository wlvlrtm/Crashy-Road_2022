using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public void ObjActive(GameObject obj) {
        obj.SetActive(true);
    } 

    public void ObjDeActive(GameObject obj) {
        obj.SetActive(false);
    }

    public void GameQuit() {
        Application.Quit();
    }

    public void LoadScene(string sceneName) {
        SceneLoader.instance.LoadScene(sceneName);
    }
}

