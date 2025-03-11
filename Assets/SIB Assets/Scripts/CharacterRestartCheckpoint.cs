using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRestartCheckpoint : MonoBehaviour
{
    public GameObject sibCharacter;

    // Update is called once per frame
    void Update()
    {
        if(sibCharacter == null) {
            sibCharacter = GameObject.Find("SIB Default Character");
        }
    }

    public void RestartAtCurrentCheckpoint()
    {
        LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        sibCharacter.GetComponent<CorgiController>().enabled = false;
        sibCharacter.transform.position = levelManager.CurrentCheckPoint.transform.position;
        sibCharacter.GetComponent<CorgiController>().enabled = true;
        sibCharacter.GetComponent<Character>().ConditionState.ChangeState(CharacterStates.CharacterConditions.Normal);

    }
}
