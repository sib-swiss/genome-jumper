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
    private GameObject endPoint;

    void Start()
    {

        // Getting the main components of the Character state : Player & Cameras
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;
        mainCameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        uiCameraObj = GameObject.Find("HUDModifiable");
        endPoint = GameObject.FindGameObjectWithTag("endPoint");
    }

    void Update()
    {
        
        if(player.GetComponent<Health>().CurrentHealth <= 0)
        {
            ToggleDeathScreen();
        }
        if(endPoint.GetComponent<EndLevelTrigger>().characterSurvivedLevel == true)
        {
            ToggleWinScreen();
        }
    }

    void ToggleWinScreen()
    {
        player.SetActive(false);
        mainCameraObj.SetActive(false);
        uiCameraObj.SetActive(false);
        GameWinScreen.SetActive(true);
    }
    void ToggleDeathScreen()
    {
        player.SetActive(false);
        mainCameraObj.SetActive(false);
        uiCameraObj.SetActive(false);
        GameOverScreen.SetActive(true);
    }
}

