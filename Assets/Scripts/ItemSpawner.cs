using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {
    public static ItemSpawner instance;

    [SerializeField] private List<GameObject> items;
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private int itemCoolTime;
    
    private bool[] pointCheckers;
        public bool[] PointCheckers {
            get { return this.pointCheckers; }
            set { this.pointCheckers = value; }
        }


    private void Init() {
        if (instance == null) {
            instance = this;
        }

        this.pointCheckers = new bool[this.spawnPoints.Count];
        
        for (int i = 0; i < this.pointCheckers.Length; i++) {
            this.pointCheckers[i] = false;
        }

       ItemSpawn();
    }

    private void Awake() {
        Init();
    }

    public void ItemSpawn() {
        int i = Random.Range(0, this.items.Count);
        int j = Random.Range(0, this.spawnPoints.Count);

        if (!this.pointCheckers[j]) {
            Instantiate(this.items[i], this.spawnPoints[j].transform.position, Quaternion.Euler(-45, 0, 0));
            this.pointCheckers[j] = true;
        }        
    }
}
