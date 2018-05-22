using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreLevelToPlay : MonoBehaviour {

    public int AvatarNumber;
    public int GeneNumber;
    public static LevelPopupCreator avatarObj;
    public static LevelPopupCreator avatarObj1;

    private bool lastAvatar = false;

    public void Start()
    {
		Debug.Log("Starting StoreLevelToPlay script with avatar A"+AvatarNumber+"G"+GeneNumber);
        if(AvatarNumber < 10) {
            avatarObj = Resources.Load("Avatar 0" + AvatarNumber) as LevelPopupCreator;
            Debug.Log(avatarObj.gene1 + "|" + avatarObj.gene2 + "|" + avatarObj.gene3);

			//Cas où current == 9 et 9+1 == 10
			if ((AvatarNumber + 1) < 10) {
				avatarObj1 = Resources.Load ("Avatar 0" + (AvatarNumber + 1)) as LevelPopupCreator;
			} else {
				avatarObj1 = Resources.Load ("Avatar " + (AvatarNumber + 1)) as LevelPopupCreator;
			}
           
        }
        else
        {
            avatarObj = Resources.Load("Avatar " + AvatarNumber) as LevelPopupCreator;
            if(AvatarNumber < 25) {
                avatarObj1 = Resources.Load("Avatar " + (AvatarNumber + 1)) as LevelPopupCreator;
                lastAvatar = false;
            }
            else
            {
                avatarObj1 = Resources.Load("Avatar " + AvatarNumber) as LevelPopupCreator;
                lastAvatar = true;
            }
            
        }
    }
		
    public void StoreLevelToPlayFunction()
    {
        //LevelPopupCreator test = avatarObj;
        //Debug.Log(test);
        PlayerPrefs.SetInt("CombinationPlayAvatar",AvatarNumber);
        PlayerPrefs.SetInt("CombinationPlayGene", GeneNumber);
		PlayerPrefs.SetString("GenesNames", avatarObj.gene1+"|"+avatarObj.gene2+"|"+avatarObj.gene3);
		Debug.Log("StoreLevelToPlayFunction GenesNames : "+avatarObj.gene1+"|"+avatarObj.gene2+"|"+avatarObj.gene3);
		Debug.Log("StoreLevelToPlayFunction: A"+AvatarNumber+"G"+GeneNumber);
		if(GeneNumber == 1 || GeneNumber == 2)
        {
            PlayerPrefs.SetInt("IsNextSceneLoadable", 1);
        }
        if (GeneNumber == 3)
        {
            if(!lastAvatar)
            {
                PlayerPrefs.SetInt("IsNextSceneLoadable", 1);
            }
            else
            {
                PlayerPrefs.SetInt("isNextSceneLoadable", 0);
            }
        }
		PlayerPrefs.Save();
    }
}
