using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class CircularEnemyBehavior : MonoBehaviour {

    public float circleRadius = 1.0f;
    private GameObject enemyChild;

    private float angle;
    private Vector2 center;
    public float RotationSpeed = 30.0f;
    private float speed;

#if UNITY_EDITOR
    // Draw Gizmos when gameobject is selected (Should represent the way the object moves around).
    public void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }
#endif

    public void Start() {
        enemyChild = transform.GetChild(0).gameObject;
        enemyChild.transform.localPosition = new Vector3(circleRadius, 0, 0);
        center = this.transform.position;
        speed = Random.Range(1.3f, 2.6f);
    }

    public void FixedUpdate() {
        angle = RotationSpeed * Time.deltaTime;
        enemyChild.transform.RotateAround(center, new Vector3(0, 0, 1), angle);
        enemyChild.transform.Rotate(new Vector3(0, 0, 1), angle * speed);
    }
}
