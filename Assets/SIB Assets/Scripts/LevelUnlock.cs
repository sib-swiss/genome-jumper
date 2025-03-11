using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class LevelUnlock : MonoBehaviour {

    private GameObject LevelManager;
    private string combinationToString;
    private int avatarNumber;
    private int geneNumber;
    private GameObject pointsText;
    private GameObject[] headItems;
    private GameObject[] rightHandItems;
    private LevelProperties lvlProperties;
    private int currentUnlockedAvatar;
    private int currentUnlockedGene;

    void Start()
    {
        
        headItems = GameObject.FindGameObjectsWithTag("HeadItemUI");
        rightHandItems = GameObject.FindGameObjectsWithTag("RightHandItemUI");
        
        int levelScore = PlayerPrefs.GetInt("CurrentLevelScore");
        string levelTimer = PlayerPrefs.GetString("CurrentTimer");
        int levelStars = PlayerPrefs.GetInt("CurrentStars");
        pointsText = GameObject.Find("PointsText");
        LevelManager = GameObject.Find("LevelManager");
        lvlProperties = LevelManager.GetComponent<LevelProperties>();
        
        combinationToString = LevelManager.GetComponent<LevelProperties>().currentCombinationPlayed;
        avatarNumber = PlayerPrefs.GetInt("CombinationPlayAvatar");
        geneNumber = PlayerPrefs.GetInt("CombinationPlayGene");
        Debug.Log(geneNumber);
        string currentUnlocked = PlayerPrefs.GetString("CurrentAvatarAndGeneUnlocked");
        Debug.Log("currentUnlocked " + currentUnlocked);
        if (currentUnlocked[3].ToString() == "G")
        {
            if(Convert.ToInt32(currentUnlocked[1].ToString()) == 1)
            {
                currentUnlockedAvatar = (10 + Convert.ToInt32(currentUnlocked[2].ToString()));
                currentUnlockedGene = Convert.ToInt32(currentUnlocked[4].ToString());
            }
            if (Convert.ToInt32(currentUnlocked[1].ToString()) == 2)
            {
                currentUnlockedAvatar = (20 + Convert.ToInt32(currentUnlocked[2].ToString()));
                currentUnlockedGene = Convert.ToInt32(currentUnlocked[4].ToString());
            }
        }
        else
        {
            currentUnlockedAvatar = Convert.ToInt32(currentUnlocked[1].ToString());
            currentUnlockedGene = Convert.ToInt32(currentUnlocked[3].ToString());
        }
        
        int currentTimer = PlayerPrefs.GetInt("CurrentTimer");
        Debug.Log("currentTimer " + currentTimer);

        Debug.Log("avatarNumber " + avatarNumber);
        Debug.Log("currentUnlockedAvatar " + currentUnlockedAvatar);

        if (avatarNumber < currentUnlockedAvatar)
        {
            if(PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "score") == 0 || PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "score") < levelScore) {
                PlayerPrefs.SetInt("A" + avatarNumber + "G" + geneNumber + "score", levelScore);
            }

            if (PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "bestTime") == 0 || PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "bestTime") >= currentTimer) {
                PlayerPrefs.SetInt("A" + avatarNumber + "G" + geneNumber + "bestTime", currentTimer);
            }
            if(PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "collectedStars") == 0 || PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "collectedStars") <= levelStars) {
                PlayerPrefs.SetInt("A" + avatarNumber + "G" + geneNumber + "collectedStars", levelStars);
            }

            if(geneNumber == 3)
            {
                GameObject.Find("GoToNextLevelBtn").SetActive(true);
                GameObject.Find("NextLvl").SetActive(true);
            }
            
        }
        /*else if (avatarNumber == avatarNumber && geneNumber < geneNumber)
        {
            if (PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "score") == 0 || PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "score") < levelScore) {
                PlayerPrefs.SetInt("A" + avatarNumber + "G" + geneNumber + "score", levelScore);
            }
            if (PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "bestTime") == 0 || PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "bestTime") >= currentTimer) {
                PlayerPrefs.SetInt("A" + avatarNumber + "G" + geneNumber + "bestTime", currentTimer);

            }

            if (PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "collectedStars") == 0 || PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "collectedStars") <= levelStars) {
                PlayerPrefs.SetInt("A" + avatarNumber + "G" + geneNumber + "collectedStars", levelStars);
            }
        }*/
        else
        {
            if (geneNumber == 3)
            {
                Debug.Log("test1");
                if (PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "score") == 0 || PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "score") < levelScore) {
                    PlayerPrefs.SetInt("A" + avatarNumber + "G" + geneNumber + "score", levelScore);
                };
                if (PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "bestTime") == 0 || PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "bestTime") >= currentTimer) {
                    PlayerPrefs.SetInt("A" + avatarNumber + "G" + geneNumber + "bestTime", currentTimer);
                }
                if (PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "collectedStars") == 0 || PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "collectedStars") <= levelStars) {
                    PlayerPrefs.SetInt("A" + avatarNumber + "G" + geneNumber + "collectedStars", levelStars);
                }
                Debug.Log("test2");

                PlayerPrefs.SetString("CurrentAvatarAndGeneUnlocked", "A" + (currentUnlockedAvatar + 1) + "G1");
                Debug.Log("test3");
                GameObject.Find("GoToNextLevelBtn").SetActive(true);
                Debug.Log("test3");
                GameObject.Find("NextLvl").SetActive(true);
                Debug.Log("test4");
            }
            else
            {
                if(PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "score") == 0) {
                    PlayerPrefs.SetString("CurrentAvatarAndGeneUnlocked", "A" + currentUnlockedAvatar + "G" + (currentUnlockedGene + 1));
                }
                GameObject.Find("GoToNextLevelBtn").SetActive(true);
                GameObject.Find("NextLvl").SetActive(true);
                if (PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "score") == 0 || PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "score") < levelScore) {
                    PlayerPrefs.SetInt("A" + avatarNumber + "G" + geneNumber + "score", levelScore);
                }
                if (PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "bestTime") == 0 || PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "bestTime") >= currentTimer) {
                    PlayerPrefs.SetInt("A" + avatarNumber + "G" + geneNumber + "bestTime", currentTimer);
                }
                if (PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "collectedStars") == 0 || PlayerPrefs.GetInt("A" + avatarNumber + "G" + geneNumber + "collectedStars") <= levelStars) {
                    PlayerPrefs.SetInt("A" + avatarNumber + "G" + geneNumber + "collectedStars", levelStars);
                }

                
                Debug.Log(PlayerPrefs.GetString("CurrentAvatarAndGeneUnlocked"));
            }
        }
        Debug.Log("FinalScore1");
        GameObject.Find("FinalScore").GetComponent<Text>().text = levelScore.ToString();
        GameObject.Find("TimeScore").GetComponent<Text>().text = PlayerPrefs.GetString("TimerText");
        Debug.Log("FinalScore2");
        //GameObject.Find("GeneName").GetComponent<Text>().text = SceneManager.GetActiveScene().name;
    }
}
