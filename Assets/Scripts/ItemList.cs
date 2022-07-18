using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour {
    public static ItemList instance;
    public GameObject[] items;

    private int index;
        public int Index {
            get { return this.index; }
        }


    private void Init() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        this.index = 0;
    }

    private void Awake() {
        Init();
    }

    private void ListReset() {
        foreach(GameObject obj in this.items) {
            obj.SetActive(false);
        }
    }

    public void Change(int step) {
        ListReset();

        switch (step) {
            case 1 :
                this.index += step; 
                
                if (this.index >= this.items.Length) {
                    this.index = 0;
                }
                
                this.items[this.index].SetActive(true);
                break;
            
            case -1 :
                this.index -= 1;

                if (this.index <= -1) {
                    this.index = (this.items.Length - 1);
                }

                this.items[this.index].SetActive(true);
                break;
        }
    }

}
