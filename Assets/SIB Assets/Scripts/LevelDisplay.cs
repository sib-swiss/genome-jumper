using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelDisplay : MonoBehaviour {

    public LevelPopupCreator levelPopup;

    private Text levelName;

    // Use this for initialization
    void Start()
    {
        levelName = transform.GetChild(1).GetComponent<Text>();
        if(SceneManager.GetActiveScene().name == "LevelSelection")
        {
            levelName.text = levelPopup.levelName;
        }
        var avatarParent = transform.parent.name;
        var avatarNumber = int.Parse(avatarParent.Substring(6, 2));
    }
}
