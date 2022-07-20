using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIndicator : MonoBehaviour {
    public GameObject targetPoint;
    [SerializeField] private float rotateSpeed;

    private void Update() {
        targetPoint = ItemSpawner.instance.ItemPoint;

        transform.LookAt(new Vector3(targetPoint.transform.position.x, transform.position.y, targetPoint.transform.position.z));
    }
}
