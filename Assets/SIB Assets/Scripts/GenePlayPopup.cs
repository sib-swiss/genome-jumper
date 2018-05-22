using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenePlayPopup : MonoBehaviour {

    public GenePopupCreator genePopup;

    public Text geneName;
    public Text geneTimer;
    public Text geneStars;

    string DefaultTimerLvl1 = "12:13";
    string DefaultStarsLvl1 = "1 / 3";

    void Start () {

        geneName = transform.GetChild(1).GetComponent<Text>();
        geneTimer = transform.GetChild(2).GetChild(0).GetComponent<Text>();
        geneStars = transform.GetChild(3).GetChild(0).GetComponent<Text>();

        geneName.text = genePopup.geneName;
        geneTimer.text = DefaultTimerLvl1;
        geneStars.text = DefaultStarsLvl1;
	}
	
}
