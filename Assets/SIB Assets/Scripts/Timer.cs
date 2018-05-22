using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public int playTime = 0;
	private int seconds = 0;
	private int minutes = 0;
	private int hours = 0;
	private string textH = "";
	private string textM = "";
	private string textS = "";
	private Text text;

	// Use this for initialization
	private void Awake()
	{
        playTime += PlayerPrefs.GetInt("PreviousTime");
		StartCoroutine ("PlayTimer");
	}

	void Start () {
		text = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {

		//DEBG
		/*
		if (seconds >= 4 ) {
			GameObject go = GameObject.FindGameObjectWithTag("Player");
			go.transform.position = GameObject.Find("EndPoint").transform.position;
		}

		if (seconds < 4 && seconds > 1) {
			GameObject go = GameObject.FindGameObjectWithTag("Player");
			go.transform.position = GameObject.Find("SnpBubble").transform.position;
		}*/

		if (hours > 0) {
			if (hours < 10) {
				textH = "0" + hours+" : ";
			} else
				textH = hours + " : ";
		}

		if (minutes < 10) {
			textM = "0" + minutes + " : ";
		} else
			textM = minutes + " : ";

		if (seconds < 10) {
			textS = "0" + seconds;
		} else
			textS = seconds.ToString();
		text.text = textH + textM + textS;
	}

	private IEnumerator PlayTimer(){
		while (true) {
			yield return new WaitForSeconds(1);
			playTime +=1;
			seconds = playTime % 60;
			minutes = (playTime / 60) % 60;
			hours = (playTime / 3600) % 24;
		}
	}
		
}
