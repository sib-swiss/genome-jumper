using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MoreMountains.CorgiEngine;
using UnityEngine.Analytics;

public class SceneLoader : MonoBehaviour
{
    private int CurrentAvatar;
    private int CurrentGene;

    public void LoadScene(string sceneName)
    {

        if (SceneManager.GetActiveScene().name == "LevelSelection"){
            if(sceneName == "Menu" || sceneName == "Glossaire" || sceneName == "Tutorial") {
                SceneManager.LoadScene(sceneName);
				return;
            }else {
                CurrentAvatar = transform.GetComponent<StoreLevelToPlay>().AvatarNumber;
                CurrentGene = transform.GetComponent<StoreLevelToPlay>().GeneNumber;
            }
        }
        if (sceneName == "LevelSelection"){
			if (PlayerPrefs.GetInt ("TutorialCompleted") == 0 && !SceneManager.GetActiveScene().name.Equals("Glossaire")) {
				Debug.Log("Launching tutorial");
                PlayerPrefs.SetInt("HasTriggeredSnp", 0);
				PlayerPrefs.SetString ("checkpoint", "");
                PlayerPrefs.SetString("SuperVariantEffect", "None");
                PlayerPrefs.SetInt("PreviousTime", 0);
				PlayerPrefs.SetInt("PreviousScore", 0);
				PlayerPrefs.SetString("PlayerTookSNP", "false");
				PlayerPrefs.SetString("PlayerTookStart", "false");
				PlayerPrefs.Save ();
				Analytics.CustomEvent("launchPlay", new Dictionary<string, object>
					{
						{ "gene", "tutorial" }
					}
				);
                Debug.Log("<color=red> SceneLoader LoadingSceneManager.LoadScene A DEBUG </color>");
                MoreMountains.Tools.MMSceneLoadingManager.LoadScene("Tutorial");
                //LoadingSceneManager.LoadScene("Tutorial");
                //SceneManager.LoadScene("Tutorial");

                return;
			} else {
				Debug.Log("Launching level selection");
				//LevelSplicing.moveables = new List<GameObject>();
				//LevelSplicing.lastExonFlag = new GameObject();
				//LevelSplicing.lastIntronFlag = new GameObject();
				//LevelSplicing.scriptCounter = 0;
				PlayerPrefs.SetString ("checkpoint", "");
				PlayerPrefs.SetInt("PreviousTime", 0);
                PlayerPrefs.SetString("SuperVariantEffect", "None");
                PlayerPrefs.SetInt("PreviousScore", 0);
				PlayerPrefs.SetString("PlayerTookSNP", "false");
                PlayerPrefs.SetInt("HasTriggeredSnp", 0);
                PlayerPrefs.SetString("PlayerTookStart", "false");
				PlayerPrefs.Save();
				SceneManager.LoadScene(sceneName);
				return;
			}
        }

		//LevelSplicing.resetSplicing ();

		//PlayerPrefs.SetString("levels", avatarObj.gene1+"|"+avatarObj.gene2+"|"+avatarObj.gene3);

		int currentGeneNumber = PlayerPrefs.GetInt ("CombinationPlayGene");
		int nextGeneNumber = currentGeneNumber+1; //New avatar
		
		string[] genesNames = PlayerPrefs.GetString ("GenesNames").Split('|');

        if(sceneName == "Next"){

			PlayerPrefs.SetInt("PreviousTime", 0);
            PlayerPrefs.SetInt("PreviousScore", 0);
            PlayerPrefs.SetString("checkpoint", "");
            PlayerPrefs.SetString("CheckpointOrder", null);
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("PlayerTookSNP", "false");
            PlayerPrefs.SetString("PlayerTookStart", "false");
            PlayerPrefs.SetInt("hasTriggeredStartCodon", 0);
            PlayerPrefs.SetInt("hasTriggeredStopCodon", 0);
            PlayerPrefs.SetInt("HasTriggeredSnp", 0);
            PlayerPrefs.Save();


			if (currentGeneNumber == 3 || (nextGeneNumber>3)) {
                /*LevelPopupCreator nextAvatarGenes;
                PlayerPrefs.SetInt("CombinationPlayAvatar", PlayerPrefs.GetInt("CombinationPlayAvatar") + 1);
				PlayerPrefs.SetInt("CombinationPlayGene", 1);

                if(PlayerPrefs.GetInt("CombinationPlayAvatar") < 10) {
                    nextAvatarGenes = Resources.Load("Avatar 0" + PlayerPrefs.GetInt("CombinationPlayAvatar")) as LevelPopupCreator;
                }
                else {
                    nextAvatarGenes = Resources.Load("Avatar " + PlayerPrefs.GetInt("CombinationPlayAvatar")) as LevelPopupCreator;
                }
                PlayerPrefs.SetString("GenesNames", nextAvatarGenes.gene1 + "|" + nextAvatarGenes.gene2 + "|" + nextAvatarGenes.gene3);
                genesNames = PlayerPrefs.GetString ("GenesNames").Split('|');
                PlayerPrefs.SetInt("CombinationPlayGene",1);
                nextGeneNumber = 1;*/

				Debug.Log ("SceneLoader NextAvatar : "+ (PlayerPrefs.GetInt("CombinationPlayAvatar") + 1));
                PlayerPrefs.SetInt("NextAvatar", PlayerPrefs.GetInt("CombinationPlayAvatar") + 1);
				PlayerPrefs.SetInt("CombinationPlayGene", 1);
				PlayerPrefs.Save();
                SceneManager.LoadScene("LevelSelection");
				return;

            }
            
            PlayerPrefs.SetInt("CombinationPlayAvatar", PlayerPrefs.GetInt("CombinationPlayAvatar"));
			PlayerPrefs.SetInt("CombinationPlayGene", nextGeneNumber);
            GetPersistency(PlayerPrefs.GetInt("CombinationPlayAvatar"), PlayerPrefs.GetInt("CombinationPlayGene"));
            PlayerPrefs.Save();
			Analytics.CustomEvent("launchPlay", new Dictionary<string, object>
				{
					{ "gene", genesNames[nextGeneNumber-1] }
				}
			);
			SceneManager.LoadScene(genesNames[nextGeneNumber-1]);
			return;
        }

		if(sceneName == "LoadGene"){

			PlayerPrefs.SetInt("PreviousTime", 0);
            PlayerPrefs.SetInt("PreviousScore", 0);
            PlayerPrefs.SetString("PlayerTookSNP", "false");
            PlayerPrefs.SetString("PlayerTookStart", "false");
            PlayerPrefs.SetInt("HasTriggeredSnp", 0);

            PlayerPrefs.SetString ("checkpoint", "");
            GetPersistency(CurrentAvatar, CurrentGene);
			PlayerPrefs.Save();
			//string geneName = transform.parent.GetComponent<GenePlayPopup>().genePopup.geneName;
			string geneName = genesNames[currentGeneNumber-1];
			//Debug.Log("Loadgene "+geneName+" "+transform.parent.name+" "+ transform.name);
			Debug.Log("SceneLoader LoadGene "+geneName);
			Analytics.CustomEvent("launchPlay", new Dictionary<string, object>
				{
					{ "gene", geneName }
				}
			);
			SceneManager.LoadScene(geneName);
			return;
        }
        else {
            if(sceneName != "LevelSelection" || sceneName != "Next" || sceneName != "LoadGene") {
                SceneManager.LoadScene(sceneName);
            }
            
        }
    }


    public void GetPersistency(int avatarNb, int geneNb) {

        // Set levels difficulty
        if(avatarNb == 1 ) { 
			PlayerPrefs.SetString("Difficulty", "easy"); 
		} else if (avatarNb > 1 && avatarNb <= 10) {
			PlayerPrefs.SetString("Difficulty", "medium"); 
		}else { 
			PlayerPrefs.SetString("Difficulty", "hard"); 
		}

		PlayerPrefs.SetString("SuperVariantPersistency","None");

        Debug.Log("avatarNb " + avatarNb + " geneNb " + geneNb);

        // Avatar 01 //
        if (avatarNb == 1 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            Debug.Log("Set string firstGene to true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");

        }
        if (avatarNb == 1 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 1 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "CoffeCup");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }

        // Avatar 02 //
        if (avatarNb == 2 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "athletic female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 2 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "athletic female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
			PlayerPrefs.SetString("SuperVariantEffect","SuperMuscle");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 2 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "athletic female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "Brocoli");
			PlayerPrefs.SetString("SuperVariantPersistency","SuperMuscle");
            PlayerPrefs.SetString("SuperVariantEffect", "SuperMuscle");
        }

        // Avatar 03 //
        if (avatarNb == 3 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 3 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 3 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "Beer");
			PlayerPrefs.SetString("SuperVariantEffect","Beer");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }

        // Avatar 04 //
        if (avatarNb == 4 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 4 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female latino");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 4 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female latino");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "InsulineSyringue");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }

        // Avatar 05 //
        if (avatarNb == 5 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 5 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 5 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female");
			PlayerPrefs.SetString("CurrentGeneHeadItem", "Ribbon");
			PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }

        // Avatar 06 //
        if (avatarNb == 6 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "baby");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 6 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "baby");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 6 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "baby");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }

        // Avatar 07 //
        if (avatarNb == 7 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "ClassicalCap");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 7 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "athletic female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "Speed");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 7 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "athletic female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "Perfume");
            PlayerPrefs.SetString("SuperVariantPersistency", "Speed");
            PlayerPrefs.SetString("SuperVariantEffect", "Speed");
        }

        // Avatar 08 //
        if (avatarNb == 8 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 8 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
			PlayerPrefs.SetString("CurrentGeneHeadItem", "MoonCell");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 8 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
			PlayerPrefs.SetString("CurrentGeneHeadItem", "HearingAid");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
			PlayerPrefs.SetString("SuperVariantEffect","Deef");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }

        // Avatar 09 //
        if (avatarNb == 9 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female black");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 9 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "athletic female black");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "Speed");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 9 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "athletic female black");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "Milk");
            PlayerPrefs.SetString("SuperVariantPersistency", "Speed");
            PlayerPrefs.SetString("SuperVariantEffect", "Speed");
        }

        // Avatar 10 //
        if (avatarNb == 10 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "athletic male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 10 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "athletic male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
			PlayerPrefs.SetString("SuperVariantEffect","SuperMuscle");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 10 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "athletic male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "Bread");
			PlayerPrefs.SetString("SuperVariantPersistency","SuperMuscle");
            PlayerPrefs.SetString("SuperVariantEffect", "SuperMuscle");
        }

        // Avatar 11 TODO SKIN//
        if (avatarNb == 11 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 11 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "WhiteGlove");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 11 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "Nightcap");
            PlayerPrefs.SetString("CurrentGeneHandItem", "WhiteGlove");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        // Avatar 12 //
        if (avatarNb == 12 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "amish");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "AmishHat");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 12 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "amish");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "AmishHat");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 12 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "amish");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "AmishHat");
			PlayerPrefs.SetString("CurrentGeneHandItem", "Chisel");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        // Avatar 13 //
        if (avatarNb == 13 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "athletic female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "Speed");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 13 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "athletic female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "WhiteGlove");
            PlayerPrefs.SetString("SuperVariantPersistency", "Speed");
            PlayerPrefs.SetString("SuperVariantEffect", "Speed");
        }
        if (avatarNb == 13 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "athletic female");
			PlayerPrefs.SetString("CurrentGeneHeadItem", "Ribbon");
			PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantPersistency", "Speed");
            PlayerPrefs.SetString("SuperVariantEffect", "Speed");
        }
        // Avatar 14 //
        if (avatarNb == 14 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 14 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
			PlayerPrefs.SetString("CurrentGeneHandItem", "Cannabis");
			PlayerPrefs.SetString("SuperVariantEffect","Cannabis");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 14 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
			PlayerPrefs.SetString("CurrentGeneHeadItem", "ClassicalCap");
			PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
			PlayerPrefs.SetString("SuperVariantPersistency","Cannabis");
            PlayerPrefs.SetString("SuperVariantEffect", "Cannabis");
        }
        // Avatar 15 //
        if (avatarNb == 15 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "Milk");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 15 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
			PlayerPrefs.SetString("CurrentGeneHeadItem", "StrangeBrain");
            PlayerPrefs.SetString("CurrentGeneHandItem", "Milk");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 15 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
			PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "Milk");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        // Avatar 16 //
        if (avatarNb == 16 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 16 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "Chisel");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 16 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "Chisel");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        // Avatar 17 //
        if (avatarNb == 17 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HearingAid");
            PlayerPrefs.SetString("CurrentGeneHandItem", "Cane");
			PlayerPrefs.SetString("SuperVariantEffect","Usher");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 17 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
			PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
			PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
			PlayerPrefs.SetString("SuperVariantEffect","Speed");
			PlayerPrefs.SetString("SuperVariantPersistency","Usher");
        }
        if (avatarNb == 17 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
			PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "CoffeCup");
			PlayerPrefs.SetString("SuperVariantPersistency","Speed|Usher");
            PlayerPrefs.SetString("SuperVariantEffect", "Usher");
        }
        // Avatar 18 //
        if (avatarNb == 18 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 18 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
			PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
			PlayerPrefs.SetString("SuperVariantEffect","SuperMuscle");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }			
        if (avatarNb == 18 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "Perfume");
			PlayerPrefs.SetString("SuperVariantPersistency","SuperMuscle");
            PlayerPrefs.SetString("SuperVariantEffect", "SuperMuscle");
        }
        // Avatar 19 //
        if (avatarNb == 19 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
			PlayerPrefs.SetString("CurrentGeneHeadItem", "BHeart");
			PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 19 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
			PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 19 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
			PlayerPrefs.SetString("CurrentGeneHandItem", "InsulineSyringue");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        // Avatar 20 //
        if (avatarNb == 20 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female black");
			PlayerPrefs.SetString("CurrentGeneHeadItem", "StrangeBrain");
			PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 20 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female black");
			PlayerPrefs.SetString("CurrentGeneHeadItem", "BHeart");
			PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 20 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female black");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HearingAid");
			PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
			PlayerPrefs.SetString("SuperVariantEffect","Deef");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        // Avatar 21 //
        if (avatarNb == 21 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 21 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "Nightcap");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 21 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "NightCap");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        // Avatar 22 //
        if (avatarNb == 22 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male latino");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 22 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male latino");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 22 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male latino");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "Brocoli");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        // Avatar 23 //
        if (avatarNb == 23 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male black");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 23 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male black");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 23 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male black");
			PlayerPrefs.SetString("CurrentGeneHeadItem", "BHeart");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        // Avatar 24 //
        if (avatarNb == 24 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
			PlayerPrefs.SetString("CurrentGeneSkinModification", "baby");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 24 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 24 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller female latino");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "HandMask");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        // Avatar 25 //
        if (avatarNb == 25 && geneNb == 1) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "HeadMask");
            PlayerPrefs.SetString("CurrentGeneHandItem", "Brocoli");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 25 && geneNb == 2) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "Nightcap");
            PlayerPrefs.SetString("CurrentGeneHandItem", "Brocoli");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
        if (avatarNb == 25 && geneNb == 3) {
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
            // SNP Persistency
            PlayerPrefs.SetString("CurrentGeneSkinModification", "city dweller male latino");
            PlayerPrefs.SetString("CurrentGeneHeadItem", "Nightcap");
            PlayerPrefs.SetString("CurrentGeneHandItem", "Brocoli");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            PlayerPrefs.SetString("SuperVariantPersistency", "None");
        }
    }
}
