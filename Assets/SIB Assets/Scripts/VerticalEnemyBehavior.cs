using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalEnemyBehavior : MonoBehaviour {

    private Vector3 initialTransform;
    public float speed;
    private bool dirUp = true;

    private void Start() {
        initialTransform = this.transform.localPosition;
        speed = Random.Range(0.85f, 2f);
    }

    private void FixedUpdate() {
        if (dirUp) {
            this.transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else {
            this.transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        if(this.transform.localPosition.y >= initialTransform.y + 1.75f) {
            dirUp = false;
        }
        if(this.transform.localPosition.y <= initialTransform.y) {
            dirUp = true;
        }
    }
}
