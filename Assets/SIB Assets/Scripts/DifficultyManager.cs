using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour {

	private GameObject[] intronFlags;
	private GameObject[] exonFlags;
    private GameObject[] Coins;
    private GameObject[] verticalEnemies;
    private GameObject[] horizontalEnemies;
    private GameObject[] circularEnemies;
    public GameObject VideoOutroPrefab;


	// Use this for initialization
	void Start ()
    {
        //DEBUG : 
        //PlayerPrefs.SetString("Difficulty","easy");

        string difficulty = PlayerPrefs.GetString ("Difficulty");


        EnemyCollideEffect[] ece = transform.parent.GetComponentsInChildren<EnemyCollideEffect> ();
		intronFlags = GameObject.FindGameObjectsWithTag("Intron Flag");
		exonFlags = GameObject.FindGameObjectsWithTag("Exon Flag");
        Coins = GameObject.FindGameObjectsWithTag("Coins");
        verticalEnemies = GameObject.FindGameObjectsWithTag("verticalEnemy");
        horizontalEnemies = GameObject.FindGameObjectsWithTag("horizontalEnemy");
        circularEnemies = GameObject.FindGameObjectsWithTag("circularEnemy");


		if (difficulty.Equals ("hard")) {
			Debug.Log ("Difficulty set to hard");
			//hard
			//Damages set to 100% on ennemies
			foreach (EnemyCollideEffect effect in ece) {
				effect.damagesToDeal = (int) (effect.damagesToDeal * 1.00f); //Default
			}

            // We destroy 1 coin on 2
            for(int i = 0; i < Coins.Length; i++) {
                Coins[i].transform.localPosition = new Vector3(Coins[i].transform.localPosition.x, Coins[i].transform.localPosition.y + Random.Range(0, 0.45f), Coins[i].transform.localPosition.z);
                if (i%2 == 0) {
                    Destroy(Coins[i]);
                }
            }
				
			DisableFlagsDisplay ();
            setStartAndStopAndSnpY(1.3f);

        } else if (difficulty.Equals ("medium")) {
			Debug.Log ("Difficulty set to medium");
			//medium
			//Damage set to 75% on ennemies +  flying ennemies damage set to 0
			foreach (EnemyCollideEffect effect in ece) {
				if (effect.gameObject.name.Contains ("enemyFlying")) {
					//Flying ennemies, disable damages
					effect.disableTrigger2D = true;
					ParticleSystem[] ps = effect.gameObject.GetComponentsInChildren<ParticleSystem> ();
					foreach (ParticleSystem particleSystem in ps) {
						particleSystem.Stop ();
					}
				} else {
					effect.damagesToDeal = (int) (effect.damagesToDeal * 0.75f);
				}
			}

            // We destroy 1 coin on 4
            for (int i = 0; i < Coins.Length; i++) {
                if (i % 4 == 0) {
                    Destroy(Coins[i]);
                }
            }
            setStartAndStopAndSnpY(.8f);

            DisableFlagsDisplay ();

            foreach(GameObject enemy in circularEnemies) {
                enemy.SetActive(false);
            }

		} else {
			Debug.Log ("Difficulty set to easy");
			//easy or undefined
			//No damage on regular ennemies + no flying ennemies
			foreach (EnemyCollideEffect effect in ece) {
				effect.disableTrigger2D = true;
				if (effect.gameObject.name.Contains ("enemyFlying")) {
					effect.gameObject.SetActive (false);
				} else {
					ParticleSystem[] ps = effect.gameObject.GetComponentsInChildren<ParticleSystem> ();
					foreach (ParticleSystem particleSystem in ps) {
						particleSystem.Stop ();
					}
				}
			}

            foreach(GameObject enemy in circularEnemies) {
                enemy.SetActive(false);
            }
            foreach(GameObject enemy in horizontalEnemies) {
                enemy.SetActive(false);
            }
            foreach(GameObject enemy in verticalEnemies) {
                enemy.SetActive(false);
            }

            // Add Video Outro
            GameObject UICamera = GameObject.Find("UICamera");
            GameObject EndPoint = GameObject.FindGameObjectWithTag("endPoint");
            GameObject videoOutro = Instantiate(VideoOutroPrefab);
            if(SceneManager.GetActiveScene().name == "Tutorial") {
                videoOutro.transform.GetChild(1).gameObject.SetActive(false);
            }
            videoOutro.transform.SetParent(UICamera.transform.GetChild(0));
            EndPoint.GetComponent<EndLevelTrigger>().VideoOutroPanel = videoOutro;
            videoOutro.GetComponentInChildren<VideoPlayer>().targetCamera = UICamera.GetComponent<Camera>();
            videoOutro.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            videoOutro.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            videoOutro.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);

            videoOutro.GetComponent<Transform>().localScale = new Vector3(1,1,1);



        }
		
	}

	void DisableFlagsDisplay(){
		foreach(GameObject flag in intronFlags){
			SpriteRenderer[] renderers = flag.GetComponentsInChildren<SpriteRenderer> ();
			if(renderers.Length>0)
				renderers [0].enabled = false;

			TextMesh[] texts = flag.GetComponentsInChildren<TextMesh> ();
			if(texts.Length>0)
				texts[0].gameObject.SetActive(false);
		}

		foreach(GameObject flag in exonFlags){
			SpriteRenderer[] renderers = flag.GetComponentsInChildren<SpriteRenderer> ();
			if(renderers.Length>0)
				renderers [0].enabled = false;

			TextMesh[] texts = flag.GetComponentsInChildren<TextMesh> ();
			if(texts.Length>0)
				texts[0].gameObject.SetActive(false);
		}

	}

    private void setStartAndStopAndSnpY(float y)
    {
        GameObject startReplace = GameObject.Find("StartReplace");
        GameObject startBubble = GameObject.Find("StartBubble");
        GameObject stopReplace = GameObject.Find("StopReplace");
        GameObject stopBubble = GameObject.Find("StopBubble");

        GameObject snpReplace = GameObject.Find("SnpReplace");
        GameObject snpBubble = GameObject.Find("SnpBubble");

        if (startBubble != null && startReplace != null && stopReplace != null && stopBubble != null)
        {
            startReplace.transform.localPosition = new Vector3(startReplace.transform.localPosition.x, startReplace.transform.localPosition.y + y, startReplace.transform.localPosition.z);
            startBubble.transform.localPosition = new Vector3(startBubble.transform.localPosition.x, startBubble.transform.localPosition.y + y, startBubble.transform.localPosition.z);
            stopReplace.transform.localPosition = new Vector3(stopReplace.transform.localPosition.x, stopReplace.transform.localPosition.y + y, stopReplace.transform.localPosition.z);
            stopBubble.transform.localPosition = new Vector3(stopBubble.transform.localPosition.x, stopBubble.transform.localPosition.y + y, stopBubble.transform.localPosition.z);
        }

        if (snpReplace != null && snpBubble != null)
        {
            snpReplace.transform.localPosition = new Vector3(snpReplace.transform.localPosition.x, snpReplace.transform.localPosition.y + y, snpReplace.transform.localPosition.z);
            snpBubble.transform.localPosition = new Vector3(snpBubble.transform.localPosition.x, snpBubble.transform.localPosition.y + y, snpBubble.transform.localPosition.z);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
