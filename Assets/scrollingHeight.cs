using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingHeight : MonoBehaviour {


    private int numberOfAvatars = 0;
    // Use this for initialization
    void Start() {
        GameObject[] test = GameObject.FindObjectsOfType<GameObject>();
        GetComponent<RectTransform>().sizeDelta = new Vector2(100,  ((60 * (GameObject.Find("Content").transform.childCount + 2)/3)));
	}
}
