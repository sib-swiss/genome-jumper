using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextBtnBehavior : MonoBehaviour {

    private Button nextBtn;

	// Use this for initialization
	void Start () {
        nextBtn = this.GetComponent<Button>();

        Debug.Log("INT : " + PlayerPrefs.GetInt("CombinationPlayGene"));
        if(PlayerPrefs.GetInt("CombinationPlayGene") == 3) {
            nextBtn.onClick.AddListener(() => {
                GameObject.Find("winGameScreen").transform.GetChild(GameObject.Find("winGameScreen").transform.childCount - 1).gameObject.SetActive(true);
                GameObject.Find("winGameScreen").GetComponent<FadeScreenBehavior>().enabled = true;
            });
        }
        else {
            nextBtn.onClick.AddListener(() => {
                this.GetComponent<SceneLoader>().LoadScene("Next");
                
            });
        }
	}
}
