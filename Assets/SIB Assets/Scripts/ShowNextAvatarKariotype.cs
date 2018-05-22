using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNextAvatarKariotype : MonoBehaviour {

    private int numberOfAvatars;
	// Use this for initialization
	void Start () {

        numberOfAvatars = GetComponent<isGameStartedForFirstTime>().numberOfAvatars;

		//Debg
		//PlayerPrefs.SetInt("NextAvatar",4);

		if (PlayerPrefs.HasKey ("NextAvatar")) {
			
			int nextAvatar = PlayerPrefs.GetInt ("NextAvatar");
			Debug.Log ("ShowNextAvatarKariotype Next avatar : " + nextAvatar);
			if (nextAvatar > numberOfAvatars) {
				Debug.Log ("Next avatar " + nextAvatar + " numberOfAvatars " + numberOfAvatars + " can't open cariotype");
				return;
			}
                
			string avatarName = "Avatar";

			if (nextAvatar < 10) {
				avatarName = avatarName + "0" + nextAvatar;
			} else {
				avatarName = avatarName + nextAvatar;
			}
			Debug.Log ("ShowNextAvatarKariotype Activating : " + avatarName);

			GameObject avatarPopup = GameObject.Find ("PopupContainer/"+avatarName).transform.GetChild (0).gameObject;
			if (avatarPopup) {
				avatarPopup.SetActive (true);
				Debug.Log ("Activating avatarPopup of "+avatarName);
			} else {
				Debug.Log ("Can't find avatarPopup of "+avatarName);
			}

			PlayerPrefs.DeleteKey ("NextAvatar");
			PlayerPrefs.Save ();
		} else {
			Debug.Log ("No next avatar key ...");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
