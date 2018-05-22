using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStats : MonoBehaviour {

    public Text avatarUnlockedText;
    public Text totalStarsText;
    public Text timeElapsedText;

    public int numberOfAvatars;
    public int numberOfStars;
    private int avatarUnlocked;

    private int totalStarsCollected = 0;

    void Start () {
        string currentAvatar = PlayerPrefs.GetString("CurrentAvatarAndGeneUnlocked");

        if(currentAvatar[2].ToString() == "G") {
            avatarUnlocked = int.Parse(currentAvatar.Substring(1, 1));
        }
        else {
            if(int.Parse(currentAvatar.Substring(1, 2)) == 26) {
                avatarUnlocked = 25;
            }
            else {
                avatarUnlocked = int.Parse(currentAvatar.Substring(1, 2));
            }
        }
        
        for(int i = 1; i <= avatarUnlocked;i++)
        {
            for(int k = 1; k <= 3; k++)
            {
                totalStarsCollected += PlayerPrefs.GetInt("A" + i + "G" + k + "collectedStars");
            }
        }

        if(avatarUnlocked < 10) { avatarUnlockedText.text = "0" + avatarUnlocked + " / " + numberOfAvatars; }
        else if (avatarUnlocked >=10 ) { avatarUnlockedText.text = avatarUnlocked + " / " + numberOfAvatars; }

        if (totalStarsCollected < 10) { totalStarsText.text = "0" + totalStarsCollected + " / " + numberOfStars; }
        else if (totalStarsCollected >= 10) { totalStarsText.text = totalStarsCollected + " / " + numberOfStars; }
    }

     void Update()
        {
        string hours = Mathf.Floor((PlayerPrefs.GetFloat("timePlayed") % 216000) / 3600).ToString("00");
        string minutes = Mathf.Floor((PlayerPrefs.GetFloat("timePlayed") % 3600) / 60).ToString("00");
        string seconds = (PlayerPrefs.GetFloat("timePlayed") % 60).ToString("00");
        timeElapsedText.text = hours + ":" + minutes + ":" + seconds;
    }
}
