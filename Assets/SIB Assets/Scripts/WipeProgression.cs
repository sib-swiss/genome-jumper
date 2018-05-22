using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WipeProgression : MonoBehaviour {

    public void resetClicked()
    {
        var savePlayerName = PlayerPrefs.GetString("Pseudo");
        var script = GameObject.FindObjectOfType(typeof(isGameStartedForFirstTime)) as isGameStartedForFirstTime;
        var timeReset = GameObject.FindObjectOfType(typeof(IncrementTimePlayed)) as IncrementTimePlayed;
        var load = GameObject.FindObjectOfType(typeof(SceneLoader)) as SceneLoader;
        script.SetGenesScoresAndTimesToNull();
        timeReset.theTime = 0;
        PlayerPrefs.SetString("CurrentAvatarAndGeneUnlocked", "A1G1");
        load.LoadScene("Menu");
        PlayerPrefs.SetString("Pseudo", savePlayerName);
    }
}
