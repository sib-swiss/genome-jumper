using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieTest : MonoBehaviour {


	private GameObject player;   
	private bool isDead = false;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");

		} else if(!isDead) {
			
			Animator animator = player.GetComponentInChildren<Animator> ();
			animator.SetTrigger("Die");

			isDead = true;
		}

	}
}
