using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public SceneLoader sl;

    public void isPlayerNameInPlayerPrefs()
    {
        if( PlayerPrefs.GetString("Pseudo") != "" ) {
            Debug.Log(PlayerPrefs.GetString("Pseudo"));
            sl.LoadScene("LevelSelection");
        }
        else
        {
            sl.LoadScene("Options");
        }
    }

}
