using System.Collections;
using System.Collections.Generic;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

public class HelloPseudo : MonoBehaviour {

    public Text helloText;    
	public LeanLocalizedTextArgs LocalizedTextArgs;

	// Use this for initialization
	void Start () {
        string PlayerPseudo = PlayerPrefs.GetString("Pseudo");
		LocalizedTextArgs.SetArg(PlayerPseudo, 0);
	}
}
