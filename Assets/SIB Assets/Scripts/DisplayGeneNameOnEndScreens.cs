using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGeneNameOnEndScreens : MonoBehaviour {

    public Text levelWinText;
    public Text levelLooseText;

    void Update()
    {
        //levelWinText.text = "You won the gene : " + GameObject.Find("UICamera").GetComponent<GUIManager>().LevelText.text;
        //levelLooseText.text = "You lost on the gene : " + GameObject.Find("UICamera").GetComponent<GUIManager>().LevelText.text;
    }
}
