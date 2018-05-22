using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class StartTestAndSound : MonoBehaviour
{

	public GameObject VideoCamera;
	public GameObject Global;
    private string difficulty;

	// Use this for initialization
	void Start () {

        if (PlayerPrefs.HasKey("Difficulty"))
            difficulty = PlayerPrefs.GetString("Difficulty");


		if ((difficulty != null &&!difficulty.Equals("easy")) || PlayerPrefs.GetString("restart").Equals("true"))
		{
			VideoCamera.SetActive(false);
			gameObject.GetComponent<CameraController>().enabled = true;
			Global.SetActive(true);
			PlayerPrefs.SetString("restart", "false");
		}

		if (PlayerPrefs.HasKey("volume") && PlayerPrefs.GetInt("volume") == 0)
		{
			AudioListener.volume = 0;
		}
	}
}
