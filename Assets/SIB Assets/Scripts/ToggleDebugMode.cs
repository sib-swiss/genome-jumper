using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleDebugMode : MonoBehaviour {

    public Button ActivateDebugModeButton;
    public InputField DebugModeInput;
    public GameObject WrongCodeText;
    public GameObject CorrectCodeText;

    private string DebugModeCode = "SIB20"; // Password for DEBUG MODE

    public void EnableDebugMode() {
        if(DebugModeInput.text == DebugModeCode) {
            WrongCodeText.SetActive(false);
            CorrectCodeText.SetActive(true);
            PlayerPrefs.SetString("CurrentAvatarAndGeneUnlocked", "A26G1");
            PlayerPrefs.SetInt("TutorialCompleted", 1);
        }
        else {
            DebugModeInput.text = "";
            WrongCodeText.SetActive(true);
        }
    }

    public void Start() {
        WrongCodeText.SetActive(false);
        CorrectCodeText.SetActive(false);
    }

    public void Update() {
        if(DebugModeInput.text != "") {
            if (WrongCodeText.activeInHierarchy) {
                WrongCodeText.SetActive(false);
            }
        }
    }

}
