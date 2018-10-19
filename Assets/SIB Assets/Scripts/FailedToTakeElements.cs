using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailedToTakeElements : MonoBehaviour {

    [Header("StartStopSNPDisplay elements")]
    public GameObject StartStopSNPDisplay;
    public GameObject Text;
    public GameObject IconsContainer;
    public GameObject ButtonContainer;

    [Header("WinGameScreen elements")]
    public GameObject top;
    public GameObject middle;
    public GameObject bottom;

    private bool startState;
    private bool snpState;
    private bool stopState;

    private void Start() {

        startState = GameObject.Find("StartBubble").GetComponent<StartStopCodonBehaviour>().hasBeenTriggered;
        snpState = GameObject.Find("SnpBubble").GetComponent<SNPVariant>().hasBeenTriggered;
        stopState = GameObject.Find("StopBubble").GetComponent<StartStopCodonBehaviour>().hasBeenTriggered;

        for(int i = 0; i < IconsContainer.transform.childCount; i++) {
            IconsContainer.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int j = 0; j< ButtonContainer.transform.childCount; j++) {
            ButtonContainer.transform.GetChild(j).gameObject.SetActive(false);
        }
        
        if (!startState) {
            // Display current Start if not took
            IconsContainer.transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("StartBubble").GetComponent<SpriteRenderer>().sprite;
            IconsContainer.transform.GetChild(0).gameObject.SetActive(true);
        }
        
        if (!snpState) {
            // Display current Snp if not took
            IconsContainer.transform.GetChild(1).GetComponent<Image>().sprite = GameObject.Find("SnpBubble").GetComponent<SpriteRenderer>().sprite;
            IconsContainer.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (!stopState) {
            // Display current Stop if not took
            IconsContainer.transform.GetChild(2).GetComponent<Image>().sprite = GameObject.Find("StopBubble").GetComponent<SpriteRenderer>().sprite;
            IconsContainer.transform.GetChild(2).gameObject.SetActive(true);
        }

        if (!snpState) {
            Text.GetComponent<LeanLocalizedTextArgs>().PhraseName = "ForgotScreenTitleSNP";
            Text.GetComponent<LeanLocalizedTextArgs>().FallbackText = "You forgot to take the variant, try again to understand how it influences your avatar!";
        }

        if (snpState && !startState && !stopState) {
            Text.GetComponent<LeanLocalizedTextArgs>().PhraseName = "ForgotScreenTitleStartStop";
            Text.GetComponent<LeanLocalizedTextArgs>().FallbackText = "You forgot to take the Start and the Stop Codon. Without them, the protein can't be produced. Try to catch it the next time!";
        }

        if (snpState && !startState && stopState) {
            Text.GetComponent<LeanLocalizedTextArgs>().PhraseName = "ForgotScreenTitleStart";
            Text.GetComponent<LeanLocalizedTextArgs>().FallbackText = "You forgot to take the Start Codon. Without it, protein production can't start. Try to catch it the next time!";
        }

        if(snpState && startState && !stopState) {
            Text.GetComponent<LeanLocalizedTextArgs>().PhraseName = "ForgotScreenTitleStop";
            Text.GetComponent<LeanLocalizedTextArgs>().FallbackText = "You forgot to take the Stop Codon. Without it, incorrect proteins are produced. Try to catch it the next time!";
        }

        if(startState && snpState && stopState) {
            GoToNextScreen();
        }
        else if (snpState && (!startState || !stopState)) {
            DisplayWinScreen();
        }
        else {
            DisplayRetryScreen();
        }
    }

    public void DisplayWinScreen() {
        ButtonContainer.transform.GetChild(0).gameObject.SetActive(false);
        ButtonContainer.transform.GetChild(1).gameObject.SetActive(false);
        ButtonContainer.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void DisplayRetryScreen() {
        ButtonContainer.transform.GetChild(0).gameObject.SetActive(true);
        ButtonContainer.transform.GetChild(1).gameObject.SetActive(true);
        ButtonContainer.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void GoToNextScreen() {
        this.GetComponent<LevelUnlock>().enabled = true;
        top.SetActive(true);
        middle.SetActive(true);
        bottom.SetActive(true);
        StartStopSNPDisplay.SetActive(false);
        
    }
}
