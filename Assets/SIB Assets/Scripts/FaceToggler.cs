using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class FaceToggler : MonoBehaviour
{

    private string currentAvatarGeneCombination;
    private int currentAvatar;
    private int currentGene;

	private GameObject unknwonFacePrefab;



    // Use this for initialization
    void Start()
    {

		unknwonFacePrefab = Resources.Load<GameObject>("Unknown");

        currentAvatarGeneCombination = PlayerPrefs.GetString("CurrentAvatarAndGeneUnlocked");
        transform.GetChild(0).gameObject.SetActive(true);

		//Debug.Log ("CurrentAvatarAndGeneUnlocked "+currentAvatarGeneCombination);

		//New method
		int avatarId;
		int currentAvatarId,currentGeneId;

		//Parsing avatarId
		if(int.TryParse(gameObject.name.Split(' ')[1], out avatarId)){
			
			//Debug.Log ("AvatarId : "+avatarId);
			string pattern = @"A([0-9]+)G([0-9]+)";
			string[] avatarAndGene = Regex.Split (currentAvatarGeneCombination,pattern); 

			//Parsing last avatarId and geneId unlocked
			if (int.TryParse (avatarAndGene[1], out currentAvatarId)) {
				if (int.TryParse (avatarAndGene[2], out currentGeneId)) {

					//Debug.Log ("CurrentAvatar : "+currentAvatarId+" "+currentGeneId);
					if (avatarId < currentAvatarId) {
						//Already unlocked (previous avatar)
						transform.GetChild (0).gameObject.SetActive (false);
						transform.GetChild (3).gameObject.SetActive (true);
					} else if (avatarId > currentAvatarId) {
						//Next avatar
						transform.GetChild(0).gameObject.SetActive(true);
						transform.GetChild(1).gameObject.SetActive(false);
						transform.GetChild(2).gameObject.SetActive(false);
						transform.GetChild(3).gameObject.SetActive(false);
					} else {
						//Current avatar
						transform.GetChild(0).gameObject.SetActive(false);
						switch (currentGeneId) {
						case 1:
							PutUnknownFace();
							break;

						case 2:
							transform.GetChild (1).gameObject.SetActive (true);
							break;

						case 3:
						default:
							transform.GetChild (2).gameObject.SetActive (true);
							break;


						}
					}

				} else {
					Debug.Log ("Parsing error");
				}
			} else {
				Debug.Log ("Parsing error");
			}

		}else{
			Debug.Log("Can't parse properly currentAvatarGeneCombination "+currentAvatarGeneCombination);
		}



		//Amélioration possible split avec le mot Avatar et récupérer le second token. Parser le numéro. 
		//Avatars id < 10
		//Old solution 
		/*
        if (gameObject.name[7].ToString() == "0")
        {
            if(currentAvatarGeneCombination[2].ToString() == "G")
            {
                if(gameObject.name[8] <= currentAvatarGeneCombination[1])
                {
                    if(currentAvatarGeneCombination[3].ToString() == "3") {
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(3).gameObject.SetActive(true);
                    }
                    else if (currentAvatarGeneCombination[3].ToString() == "2")
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(2).gameObject.SetActive(true);
                    }
                    else if (currentAvatarGeneCombination[3].ToString() == "1")
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                        //transform.GetChild(1).gameObject.SetActive(true);
						PutUnknownFace();
                    }
                }
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(true);
            }
        }
		//Avatars id >= 10
        else if (gameObject.name[7].ToString() == "1")
        {
            if ( currentAvatarGeneCombination.Length == 5)
            {
                if ( (gameObject.name[8] <= currentAvatarGeneCombination[2]) || (currentAvatarGeneCombination[1].ToString() == "2"))
                {
                    if(currentAvatarGeneCombination[1].ToString() == "1")
                    {
                        if (currentAvatarGeneCombination[4].ToString() == "3")
                        {
                            transform.GetChild(0).gameObject.SetActive(false);
                            transform.GetChild(3).gameObject.SetActive(true);
                        }
                        else if (currentAvatarGeneCombination[4].ToString() == "2")
                        {
                            transform.GetChild(0).gameObject.SetActive(false);
                            transform.GetChild(2).gameObject.SetActive(true);
                        }
                        else if (currentAvatarGeneCombination[4].ToString() == "1")
                        {
                            transform.GetChild(0).gameObject.SetActive(false);
							PutUnknownFace();
                            //transform.GetChild(1).gameObject.SetActive(true);
                        }
                    }
                    
                    else if (currentAvatarGeneCombination[1].ToString() == "2")
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(3).gameObject.SetActive(true);
                    }
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(true);
					PutUnknownFace();
                    //transform.GetChild(1).gameObject.SetActive(false);
                }
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        else if (gameObject.name[7].ToString() == "2")
        {
            if(currentAvatarGeneCombination.Length == 5)
            {
                if(currentAvatarGeneCombination[1].ToString() == "2")
                {
                    if (gameObject.name[8] <= currentAvatarGeneCombination[2])
                    {
                        if (currentAvatarGeneCombination[4].ToString() == "3")
                        {
                            transform.GetChild(0).gameObject.SetActive(false);
                            transform.GetChild(3).gameObject.SetActive(true);
                        }
                        else if (currentAvatarGeneCombination[4].ToString() == "2")
                        {
                            transform.GetChild(0).gameObject.SetActive(false);
                            transform.GetChild(2).gameObject.SetActive(true);
                        }
                        else if (currentAvatarGeneCombination[4].ToString() == "1")
                        {
                            transform.GetChild(0).gameObject.SetActive(false);
                            transform.GetChild(1).gameObject.SetActive(true);
                        }
                    }
                }
                else if (currentAvatarGeneCombination[1].ToString() == "1")
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(false);
                }
                
            else
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(false);
                }
            }
        }
        */
    }

	void PutUnknownFace(){
		
		GameObject unknwonFace = Instantiate (unknwonFacePrefab,transform.position,transform.rotation,transform);
	}
}