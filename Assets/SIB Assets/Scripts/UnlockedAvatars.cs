using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UnlockedAvatars : MonoBehaviour {

    public Button[] avatarButtons;
    private Color grayedButton;

    void Start()
    {
        // We set an halfway opacity for a "grayed style" button like it's locked
        grayedButton = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 100.0f / 255.0f);
        // We get the number of avatars unlocked by the player;

        //Uncommented for real lock tests
        //PlayerPrefs.SetString("CurrentAvatarAndGeneUnlocked", "A1G1"); //A25G3 = unlock all see isGameStartedForFirstTime
        //PlayerPrefs.SetString("CurrentAvatarAndGeneUnlocked", "A26G1"); //A25G3 = unlock all see isGameStartedForFirstTime

        string avatarUnlock = PlayerPrefs.GetString("CurrentAvatarAndGeneUnlocked");
        avatarUnlock = avatarUnlock.Substring(1,2);
        if(avatarUnlock.Contains("G"))
        {
            avatarUnlock = avatarUnlock.Substring(0, 1);
        }
        int avatarUnlockInt = int.Parse(avatarUnlock);
        // We set the buttons depending on the currentAvatarUnlocked to lock not unlocked avatars to play.

        for (int i = 0; i < avatarButtons.Length; i++)
        {
            if (i + 1 > avatarUnlockInt)
            {
                // Disabled click on buttons + Gray the image with the custom color;
                avatarButtons[i].interactable = false;
                //avatarButtons[i].GetComponentInChildren<RawImage>().color = grayedButton;
            }
        }
    }

}
