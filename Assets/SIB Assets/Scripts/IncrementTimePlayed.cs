using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementTimePlayed : MonoBehaviour {

    private bool getAlreadyPlayedTime = true;
    public float theTime;

    void Update () {

        if (getAlreadyPlayedTime)
        {
            theTime += PlayerPrefs.GetFloat("timePlayed") + (Time.deltaTime * 1);
            getAlreadyPlayedTime = false;
        }
        else
        {
            theTime += Time.deltaTime * 1;
            PlayerPrefs.SetFloat("timePlayed", theTime);
        }
    }
}
