using System.Collections;
using System.Collections.Generic;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

public class LevelPopupDisplay : MonoBehaviour {

    public LevelPopupCreator levelPopup;
    public Text levelName;
    public Text populationAndSex;
    public Text Gene1;
    public Text Gene2;
    public Text Gene3;

	// Use this for initialization
	void Start () {
        levelName.text = levelPopup.levelName;
        populationAndSex.GetComponent<LeanLocalizedTextArgs>().PhraseName = levelPopup.population;
        Gene1.text = levelPopup.gene1;
        Gene2.text = levelPopup.gene2;
        if(levelPopup.levelName == "Motoo") {
            Gene3.text = "FOXO3";
        }
        else {
            Gene3.text = levelPopup.gene3;
        }
        
    }
}
