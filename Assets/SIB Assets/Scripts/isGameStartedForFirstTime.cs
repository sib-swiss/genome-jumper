using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isGameStartedForFirstTime : MonoBehaviour {

    private bool isPlayerPrefsFound = false;
    public int numberOfAvatars;
    public int numberOfGenes;

	void Start () {
        if ( PlayerPrefs.GetString("Pseudo") != "" ) {
            isPlayerPrefsFound = true;
        }	
        else {
            isPlayerPrefsFound = false;
            SetGenesScoresAndTimesToNull();
            PlayerPrefs.SetString("Pseudo", "Test");
            //PlayerPrefs.SetString("CurrentAvatarAndGeneUnlocked", "A26G1");//A26G1 = unlock all, A1G1 all locked see UnlockedAvatars
			PlayerPrefs.SetString("CurrentAvatarAndGeneUnlocked", "A1G1");//A26G1 = unlock all, A1G1 all locked see UnlockedAvatars
        }
    }

    public void SetGenesScoresAndTimesToNull()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("timePlayed", 0);
        PlayerPrefs.SetString("CurrentGeneSkinModification", "");
        //PlayerPrefs.SetInt("A1G1isPlayable", 1);
        for (int i = 1; i <= numberOfAvatars; i++)
        {
            for(int k = 1; k <= numberOfGenes; k++)
            {
                PlayerPrefs.SetInt("A" + i + "G" + k + "score", 0);
                PlayerPrefs.SetInt("A" + i + "G" + k + "collectedStars", 0);
                PlayerPrefs.SetInt("A" + i + "G" + k + "bestTime", 0);
                //PlayerPrefs.SetInt("A" + i + "G" + k + "isPlayable", 0);
            }
        }
    }
}
