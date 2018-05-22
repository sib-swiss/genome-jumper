using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetStoredScoreForGene : MonoBehaviour {

    public Text TimerText;
    public Text StarsText;
    public Text ScoreText;
    private int currentAvatar;
    private int currentGene;

    void Start()
    {
        TimerText = transform.GetChild(2).GetChild(0).GetComponent<Text>();
        StarsText = transform.GetChild(3).GetChild(0).GetComponent<Text>();
        ScoreText = transform.GetChild(4).GetChild(0).GetComponent<Text>();
    }

	// Use this for initialization
	void Update () {

        currentAvatar = int.Parse(transform.name.Substring(6,2));
        currentGene = int.Parse(transform.name.Substring(21, 2));

        // Timer Display
        if (PlayerPrefs.GetInt("A" + currentAvatar + "G" + currentGene + "bestTime") != 0) {

            int minutes = ((PlayerPrefs.GetInt("A" + currentAvatar + "G" + currentGene + "bestTime") /60) % 60);
            int seconds = (PlayerPrefs.GetInt("A" + currentAvatar + "G" + currentGene + "bestTime") % 60);
            
            if (PlayerPrefs.GetInt("A" + currentAvatar + "G" + currentGene + "bestTime") < 60)
            {
                if(seconds < 10) { TimerText.text = ("00:0" + seconds); }
                else { TimerText.text = ("00:" + seconds); }
            } 
            else
            {
                if (minutes < 10)
                {
                    if(seconds < 10) { TimerText.text = ("0" + minutes + ":" +"0" + seconds); }
                    else { TimerText.text = ("0" + minutes + ":" + seconds); }
                }
              else
                {
                    if (seconds < 10) { TimerText.text = (minutes + ":" + "0" + seconds); }
                    else { TimerText.text = (minutes + ":" + seconds); }
                }
            }
        }
        else if (PlayerPrefs.GetInt("A" + currentAvatar + "G" + currentGene + "bestTime") == 0) { TimerText.text = "00:00"; }

        // Stars Display
        if (PlayerPrefs.GetInt("A" + currentAvatar + "G" + currentGene + "collectedStars") != 0) { StarsText.text = PlayerPrefs.GetInt("A" + currentAvatar + "G" + currentGene + "collectedStars").ToString() + " / 3"; }
        else if (PlayerPrefs.GetInt("A" + currentAvatar + "G" + currentGene + "collectedStars") == 0) { StarsText.text = "0/3"; }

        // Score Display
        if (PlayerPrefs.GetInt("A" + currentAvatar + "G" + currentGene + "score") != 0) { ScoreText.text = PlayerPrefs.GetInt("A" + currentAvatar + "G" + currentGene + "score").ToString() + " pts"; }
        else if (PlayerPrefs.GetInt("A" + currentAvatar + "G" + currentGene + "score") == 0) { ScoreText.text = " 0 pts"; }



    }
}
