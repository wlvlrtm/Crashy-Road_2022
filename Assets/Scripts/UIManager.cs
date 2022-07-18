using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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

    public void ItemNext() {
        ItemList.instance.Change(1);    
    }

    public void ItemPrev() {
        ItemList.instance.Change(-1);
    }

    public void SelectHide() {
        GameObject selectBtn = GameObject.Find("Canvas").transform.Find("SelectButton").gameObject;

        if (ItemList.instance.items[ItemList.instance.Index].name == "ComingSoon") {
            selectBtn.SetActive(false);
        }
        else {
            selectBtn.SetActive(true);
        }
    }

    public void GameInfoSave(string infoType) {
        switch (infoType) {
            case "Id" :
                GameInfo.instance.id = GetComponent<TMP_InputField>().text;
                break;
            case "Map" :
                foreach (GameObject target in ItemList.instance.items) {
                    if (target.activeInHierarchy) {
                        GameInfo.instance.map = target.name;
                    }
                }
                break;
            case "Car" :
                foreach (GameObject target in ItemList.instance.items) {
                    if (target.activeInHierarchy) {
                        GameInfo.instance.car = target.name;
                    }
                }
                break;
        }
    }
}

