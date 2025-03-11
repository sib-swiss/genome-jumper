using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Tools;
using MoreMountains.CorgiEngine;

public class CharacterState : MonoBehaviour {

    public GameObject GameWinScreen;
    public GameObject GameOverScreen;
    private GameObject player;
    private Camera mainCamera;
    private GameObject mainCameraObj;
    private GameObject uiCameraObj;
    private GameObject uiIcons;
    private GameObject endPoint;

    private bool hasTriggeredScreen = false;

    void Start()
    {
        hasTriggeredScreen = false;
        // Getting the main components of the Character state : Player & Cameras
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;
        mainCameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        uiCameraObj = GameObject.Find("HUD");
        //uiIcons = GameObject.Find("StartSnpStopIcons");
        endPoint = GameObject.FindGameObjectWithTag("endPoint");
    }

    void Update()
    {
        if(player && player.gameObject)
        {
            if (player.GetComponent<Health>().CurrentHealth <= 0)
            {
                ToggleDeathScreen();
            }
        } 
        if(endPoint.GetComponent<EndLevelTrigger>().characterSurvivedLevel == true)
        {
            ToggleWinScreen();
        }
    }

    void ToggleWinScreen()
    {
        if (!hasTriggeredScreen)
        {
            hasTriggeredScreen = true;
            GameWinScreen.SetActive(true);
            mainCameraObj.SetActive(false);
            uiCameraObj.SetActive(false);
            //uiIcons.SetActive(false);
            player.SetActive(false);
            this.GetComponent<CharacterState>().enabled = false;
            player.SetActive(true);
            if(GameObject.Find("horizontalSpawner") != null) {
                GameObject.Find("horizontalSpawner").SetActive(false);
            }
            GameObject.Find("Global").GetComponent<AudioSource>().Stop();
        }
    }
    void ToggleDeathScreen()
    {
        if (!hasTriggeredScreen)
        {
            hasTriggeredScreen = true;
            mainCameraObj.SetActive(false);
            PlayerPrefs.SetInt("PreviousScore", GameObject.Find("PointsText").GetComponent<ScoreDisplay>().score);
            PlayerPrefs.SetInt("PreviousTime", GameObject.Find("TimerText").GetComponent<Timer>().playTime);
            uiCameraObj.SetActive(false);
            //uiIcons.SetActive(false);
            GameOverScreen.SetActive(true);
            //player.SetActive(false);
            if (GameObject.Find("horizontalSpawner") != null) {
                GameObject.Find("horizontalSpawner").SetActive(false);
            }
            GameObject.Find("Global").GetComponent<AudioSource>().Stop();
        }
    }
}

