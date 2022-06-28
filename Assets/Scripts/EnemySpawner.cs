using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private GameObject[] enemiesObj;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private int poolAmount;
    [SerializeField] private int spawnCoolTime;

    private List<GameObject> enemies;


    private void Init() {
        this.enemies = new List<GameObject>();
        EnemyPooling();
    }

    private void Awake() {
        Init();
    }

    private void Start() {
        StartCoroutine(EnemySpawn());
    }

    private void EnemyPooling() {
        for (int i = 0; i < this.enemiesObj.Length; i++) {
            for (int j = 0; j < this.poolAmount; j++) {
                GameObject obj = Instantiate(this.enemiesObj[0]);

                obj.SetActive(false);
                obj.transform.SetParent(this.transform);
                
                this.enemies.Add(obj);
            }
        }
    }

    IEnumerator EnemySpawn() {
        while (true) {
            for (int i = 0; i < this.enemies.Count; i++) {
                if (!this.enemies[i].activeInHierarchy) {
                    int pI = Random.Range(0, this.spawnPoints.Count);
                    GameObject obj = this.enemies[i];

                    obj.transform.position = this.spawnPoints[pI].transform.position;
                    obj.SetActive(true);

                    yield return new WaitForSeconds(this.spawnCoolTime);
                }
            }

            if (this.spawnCoolTime > 1) {
                this.spawnCoolTime -= 1;
            }
        }
    }


}

