using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour {
    [SerializeField] private float rotateSpeed;

    private void Update() {
        transform.Rotate(Vector3.up * rotateSpeed);
    }

}
