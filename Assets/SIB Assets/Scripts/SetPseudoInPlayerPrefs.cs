using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPseudoInPlayerPrefs : MonoBehaviour {

    public InputField pseudoInput;

	void Start()
	{
		if (PlayerPrefs.HasKey("Pseudo") && !PlayerPrefs.GetString("Pseudo").Equals(""))
		{
			pseudoInput.text = PlayerPrefs.GetString("Pseudo");
		}

	}

	public void setPlayerPseudo () {
        PlayerPrefs.SetString("Pseudo", pseudoInput.text);
	}
}
