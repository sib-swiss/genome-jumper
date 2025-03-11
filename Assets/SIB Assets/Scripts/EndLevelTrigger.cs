using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace MoreMountains.CorgiEngine
{
    public class EndLevelTrigger : MonoBehaviour
    {
        public bool characterSurvivedLevel = false;

        private ScoreDisplay score;
        private LevelProperties LvlManager;
		private StartStopCodonBehaviour StopCodon;
		private StartStopCodonBehaviour StartCodon;
        
        public bool star1 = false;
        public bool star2 = false;
        public bool star3 = false;
        
        public GameObject VideoOutroPanel;
        public GameObject HudModifiable;
        public GameObject SVInfoPanel;

        private SNPVariant[] SNPVariants;
        private int playTime;
        
        private int multiplierTimeNegative = 125;
		private int multiplierTimePositive = 20;

        private CorgiController controller;
        //private GameObject Player;

        void Start()
        {
            score = GameObject.Find("PointsText").GetComponent<ScoreDisplay>();
            LvlManager = GameObject.Find("LevelManager").GetComponent<LevelProperties>();
            SNPVariants = GameObject.FindObjectsOfType<SNPVariant>();
			StartCodon =  GameObject.Find("StartBubble").GetComponent<StartStopCodonBehaviour>();
			StopCodon =  GameObject.Find("StopBubble").GetComponent<StartStopCodonBehaviour>();
            //Player = GameObject.FindGameObjectWithTag("Player");
            HudModifiable = GameObject.Find("HUD").gameObject;
        }
            
        void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("Enter End");
            controller = col.GetComponent<CorgiController>();
            Debug.Log("Enter End 1");
            score.increaseScore((int)controller.GetComponent<Health>().CurrentHealth); // we add the number of life to score
            Debug.Log("Enter End 2");
            playTime = GameObject.Find("TimerText").GetComponent<Timer>().playTime;
            Debug.Log("Enter End 3");
            GameObject.FindGameObjectWithTag("Player").GetComponent<DisplayCharacterOnEndLevel>().enabled = true;
            Debug.Log("Enter End 4");

            /*
			//old method based on time + score
			int inc = 0; 
			if (LvlManager.ObjectiveTime > playTime) {
				inc = (LvlManager.ObjectiveTime - playTime) * multiplierTimePositive; 
			} else {
				inc = (LvlManager.ObjectiveTime - playTime) * multiplierTimeNegative; 
			}
			score.increaseScore(inc);
			Debug.Log (LvlManager.ObjectiveTime + "s " + playTime + "s = " + inc +" score added. Score total now "+score.score+".");
			*/

            PlayerPrefs.SetInt("CurrentLevelScore", score.score);
            Debug.Log("Enter End 5");
            PlayerPrefs.SetInt("CurrentTimer", playTime);
            Debug.Log("Enter End 6");
            PlayerPrefs.SetString("TimerText", GameObject.Find("TimerText").GetComponent<Text>().text);
            Debug.Log("Enter End 7");

            if(SceneManager.GetActiveScene().name == "Tutorial") {
                GameObject.FindGameObjectWithTag("Player").SetActive(false);
                HudModifiable.SetActive(false);
                //SVInfoPanel.SetActive(false);
                VideoOutroPanel.SetActive(true);
            }
            else {
                //if (PlayerPrefs.GetString("Difficulty").Equals("easy") && VideoOutroPanel != null) {
                //    GameObject.FindGameObjectWithTag("Player").SetActive(false);
                //    HudModifiable.SetActive(false);
                //    //SVInfoPanel.SetActive(false);
                //    VideoOutroPanel.SetActive(true);
                //}
                //else {
                Debug.Log("Enter End 8");
                ActionOnTrigger();
                Debug.Log("Enter End 9");
                //}
            }

            string AvatarName = PlayerPrefs.GetString("AvatarName");
            Debug.Log("Enter End 10");
            string GeneName = PlayerPrefs.GetString("GeneName");
            Debug.Log("Enter End 11");

            // Sending Analytics event
            //Analytics.CustomEvent("level_completed", new Dictionary<string, object>
            //{
            //    { "avatar", "Avatar " + AvatarName},
            //    { "gene",  GeneName }
            //}
        //);
        }

        public void ActionOnTrigger()
        {
            characterSurvivedLevel = true;
            // CALCULATING STARS
            var allTaken = true;
            Debug.Log("ActionOnTrigger 1");
            if (SNPVariants.Length > 0) allTaken = SNPVariants.All(snp => snp.hasBeenTriggered);
            Debug.Log("ActionOnTrigger 2");

            var stopTaken = false;
            Debug.Log("ActionOnTrigger 3");
            stopTaken = StopCodon.hasBeenTriggered;
            Debug.Log("ActionOnTrigger 4");

            var startTaken = false;
            startTaken = StartCodon.hasBeenTriggered;
            Debug.Log("ActionOnTrigger 5");

            int nbStars = 0;
            if (allTaken)
            {

                nbStars++;
            }
            if (startTaken)
            {
                nbStars++;
            }
            if (stopTaken)
            {
                nbStars++;
            }
            Debug.Log("ActionOnTrigger 6");

            switch (nbStars)
            {
                case 1:
                    star1 = true;
                    PlayerPrefs.SetInt("CurrentStars", 1);
                    break;
                case 2:
                    star1 = true;
                    star2 = true;
                    PlayerPrefs.SetInt("CurrentStars", 2);
                    break;
                case 3:
                    star1 = true;
                    star2 = true;
                    star3 = true;
                    PlayerPrefs.SetInt("CurrentStars", 3);
                    break;
            }
            Debug.Log("ActionOnTrigger 7");

            //old method based on time + score
            /*
            if (score.score >= LvlManager.Score1Star && score.score < LvlManager.Score2Stars)
            {
                star1 = true;
                PlayerPrefs.SetInt("CurrentStars", 1);
            }
            else if (score.score >= LvlManager.Score2Stars && score.score < LvlManager.Score3Stars)
            {
                star1 = true;
                star2 = true;
                PlayerPrefs.SetInt("CurrentStars", 2);
            }
            else if (score.score >= LvlManager.Score3Stars && allTaken)
            {
                star1 = true;
                star2 = true;
                star3 = true;
                PlayerPrefs.SetInt("CurrentStars", 3);
            }*/
        }
    }
}
