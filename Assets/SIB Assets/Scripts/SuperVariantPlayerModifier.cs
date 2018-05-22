using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperVariantPlayerModifier : MonoBehaviour {

	public GameObject superHeart;
	// Use this for initialization
	void Start () {

    }

	// Update is called once per frame
	void Update () {

	}

	public void enableSuperHeart(){
		superHeart.SetActive(true);
	}
}
