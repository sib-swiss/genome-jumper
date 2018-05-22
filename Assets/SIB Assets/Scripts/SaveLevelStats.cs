using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLevelStats : MonoBehaviour {

    public string StarsCollected;
    public string Timer;
    public string Score;

    void Update()
    {

        // TODO: DEBUG LORSQUE LE COMPONENT PASSE EN FALSE POUR EVITER CRASH & REDONDANCE;
        Score = GameObject.Find("PointsText").GetComponent<Text>().text;
        
        if(GameObject.Find("GameWinScreen") || GameObject.Find("GameOverScreen"))
        {
            Debug.Log(Score);
        }
    }
}
