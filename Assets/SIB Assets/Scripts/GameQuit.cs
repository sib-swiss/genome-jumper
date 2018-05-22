using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuit : MonoBehaviour {
	   
    public void quitGame()
    {
        Application.Quit();
    }

	void Update () {
        if (Input.GetKeyDown("escape"))
        {
            quitGame();
        }
	}
}
