using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public Text ScoreText;
    public int score;

    void Start()
    {
        ScoreText = GameObject.Find("PointsText").GetComponent<Text>();
        score = 000000 + PlayerPrefs.GetInt("PreviousScore");
    }

    void Update()
    {
        ScoreText.text = score.ToString("D6");
    }

    public void increaseScore(int points)
    {
        score += points;
    }

}
