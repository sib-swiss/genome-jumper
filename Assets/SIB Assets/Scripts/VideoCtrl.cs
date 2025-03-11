using System;
using System.Collections;
using System.Collections.Generic;
using FlyingWormConsole3;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using MoreMountains.CorgiEngine;
using UnityEngine.SceneManagement;

public class VideoCtrl : MonoBehaviour {

    private VideoPlayer player;
    public GameObject videoCamera;
    public GameObject global;

    private Camera main;
    private double videoLength;
    private bool haveAlreadyFade;
    private bool havePlayed = false;

    void Start() {
        player = GetComponent<VideoPlayer>();
        haveAlreadyFade = false;
        videoLength = player.clip.length;
        main = Camera.main;


        int isRestartingLevel = PlayerPrefs.GetInt("IsRestartingLevel");
        if (isRestartingLevel == 1)
        {
            videoCamera.SetActive(false);
            global.SetActive(true);
            main.GetComponent<CameraController>().enabled = true;
            havePlayed = false;
        }
    }

    // Update is called once per frame
    void Update() {

        if (player.isPlaying && !havePlayed)
            havePlayed = true;


        if (player.time >= (videoLength - 1) && !haveAlreadyFade) {
            GetComponent<VideoFadeEffect>().FadeOut();
            haveAlreadyFade = true;
        }

        if (!player.isPlaying && !player.clip.name.Equals("outro") && havePlayed) {
            videoCamera.SetActive(false);
            global.SetActive(true);
            main.GetComponent<CameraController>().enabled = true;
            havePlayed = false;
        }
        else if (!player.isPlaying && player.clip.name.Equals("outro") && havePlayed) {
            if (SceneManager.GetActiveScene().name == "Tutorial") {
                GetComponent<VideoFadeEffect>().FadeOut();
                MoreMountains.Tools.MMSceneLoadingManager.LoadScene("LevelSelection");

            }
            videoCamera.SetActive(false);
            GameObject.FindGameObjectWithTag("endPoint").GetComponent<EndLevelTrigger>().ActionOnTrigger();
            havePlayed = false;
        }
    }
}
