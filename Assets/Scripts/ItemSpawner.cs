using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {
    [SerializeField] private List<GameObject> items;
    [SerializeField] private List<GameObject> spawnPoints;
    
    private bool[] pointCheckers;
        public bool[] PointCheckers {
            get { return this.pointCheckers; }
            set { this.pointCheckers = value; }
        }


    private void Init() {
        this.pointCheckers = new bool[this.spawnPoints.Count];
        
        for (int i = 0; i < this.pointCheckers.Length; i++) {
            this.pointCheckers[i] = false;
        }
    }

    private void Awake() {
        Init();
    }

    private void Start() {
        StartCoroutine(ItemSet());
    }

    IEnumerator ItemSet() {
        int coolTime = 5;
        
        while(true) {
            int i = Random.Range(0, 4);
            int j = Random.Range(0, 3);

            if (!this.pointCheckers[j]) {
                Instantiate(this.items[i], this.spawnPoints[j].transform.position, Quaternion.Euler(-45, 0, 0));
                this.pointCheckers[j] = true;
            }

            yield return new WaitForSeconds(coolTime);
        }
    }
}
