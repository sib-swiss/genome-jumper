using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalEnemyBehavior : MonoBehaviour {

    public float speed;
    private GameObject spawn;

    private void Start() {
        spawn = GameObject.Find("Spawn");
        speed = Random.Range(1.5f, 2.5f);
    }

    private void FixedUpdate() {

        // Translate object
        this.transform.parent.Translate(-speed*Time.deltaTime, 0, 0);
        this.transform.Rotate(new Vector3(0,0,speed) * 245 * Time.deltaTime, Space.World);

        // Protection to overflow of objects
        if (this.transform.position.x < spawn.transform.position.x) {
            Destroy(this.transform.parent.gameObject);
            Destroy(this.gameObject);
        }
        // Protection against object going through level

        // VOIR LE RAYCAST, SOIT LAYER, SOIT ROTATION/POSITION FOIREUSE
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, new Vector2(-1f, -1f), 1.25f);
        if (hit.collider != null && hit.collider.tag != "horizontalEnemy") {
            if (hit.collider.tag == "Exon" || hit.collider.name.Contains("fullblock") || hit.collider.name.Contains("Exon")) {
                Destroy(this.transform.parent.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
