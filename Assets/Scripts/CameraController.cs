using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    

    private void FixedUpdate() {
        
        transform.position = (this.target.position + this.offset);
    }
}
