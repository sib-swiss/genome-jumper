using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneButtonDisplay : MonoBehaviour
{

    public GameObject[] geneButtons = new GameObject[3];
    private Color grayedButton;
    private int currentAvatarUnlocked;
    private int currentGeneUnlocked;

    // Update is called once per frame
    void Awake()
    {
        geneButtons[0] = transform.GetChild(1).gameObject;
        geneButtons[1] = transform.GetChild(2).gameObject;
        geneButtons[2] = transform.GetChild(3).gameObject;

        grayedButton = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 200.0f / 255.0f);
        int avatarNumber = int.Parse(transform.parent.name.Substring(6,2));
        if(PlayerPrefs.GetString("CurrentAvatarAndGeneUnlocked").Substring(1, 2).Contains("G")) {
            currentAvatarUnlocked = int.Parse(PlayerPrefs.GetString("CurrentAvatarAndGeneUnlocked").Substring(1, 1));
            currentGeneUnlocked = int.Parse(PlayerPrefs.GetString("CurrentAvatarAndGeneUnlocked").Substring(3, 1));
        }
        else
        {
            currentAvatarUnlocked = int.Parse(PlayerPrefs.GetString("CurrentAvatarAndGeneUnlocked").Substring(1, 2));
            Debug.Log(PlayerPrefs.GetString("CurrentAvatarAndGeneUnlocked").Substring(4, 1));
            currentGeneUnlocked = int.Parse(PlayerPrefs.GetString("CurrentAvatarAndGeneUnlocked").Substring(4, 1));
        }
        

        for (int i = 0; i < geneButtons.Length; i++)
        {
            int childCount = geneButtons[i].transform.childCount;

            for (int k = 0; k < childCount; k++)
            {
                if(avatarNumber > currentAvatarUnlocked)
                {
                    geneButtons[i].transform.GetChild(k).GetComponent<Button>().interactable = false;
                }

                if(avatarNumber == currentAvatarUnlocked)
                {
                    for (int z = 0; z < currentGeneUnlocked; z++)
                    {
                        geneButtons[z].transform.GetChild(k).GetComponent<Button>().interactable = true;
                    }
                }

                if(avatarNumber < currentAvatarUnlocked)
                {
                    geneButtons[0].transform.GetChild(k).GetComponent<Button>().interactable = true;
                    geneButtons[1].transform.GetChild(k).GetComponent<Button>().interactable = true;
                    geneButtons[2].transform.GetChild(k).GetComponent<Button>().interactable = true;
                }
            }
        }
    }
}
