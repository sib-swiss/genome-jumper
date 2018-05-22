using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntronRemover : MonoBehaviour {

	// Use this for initialization
	private BoxCollider2D bc2d;

	private int incWaiting = 0;
	private int maxIncWaiting = 10;
	private bool collided = false;

	void Start () {
		bc2d = gameObject.AddComponent<BoxCollider2D> ();
		bc2d.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (collided) {
			incWaiting++;
			if (incWaiting > maxIncWaiting) {
				gameObject.SetActive (false);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		//Debug.Log ("On trigger enter 2d"+col.tag);
		if (col.tag == "Intron") {
			//Debug.Log ("Tag intron");
			Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
			rb2d.bodyType = RigidbodyType2D.Dynamic;
			//HingeJoint2D [] hinges = GetComponentsInChildren<HingeJoint2D> ();
			//foreach (HingeJoint2D hinge in hinges) {
			//	hinge.enabled = false;
			//}
			collided = true;
		}
	}
}
