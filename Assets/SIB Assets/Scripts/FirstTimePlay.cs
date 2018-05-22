using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTimePlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey("FirstTimeLaunch"))
        {
            PlayerPrefs.SetInt("FirstTimeLaunch", 0);
            GetComponent<SceneLoader>().LoadScene("Options");
        }
	}
}
