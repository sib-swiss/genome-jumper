using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarsPopupInfo : MonoBehaviour
{

    public GameObject avatarsPopup;
    private GameObject thisAvatar;
    private GenePopupDisplay genePopupDisplay;
    private GenePopupCreator scriptableObject;


    public void SeePopup(Text geneName)
    {
        GetComponent<Transform>().gameObject.SetActive(false);
        if (geneName.text == "FOXO3" && transform.parent.gameObject.name == "Avatar25") {
            scriptableObject = Resources.Load<GenePopupCreator>("ScriptableObjects/FOXO3Alternate") as GenePopupCreator;
        }
        else {
            scriptableObject = Resources.Load<GenePopupCreator>("ScriptableObjects/" + geneName.text) as GenePopupCreator;
        }
        genePopupDisplay = avatarsPopup.GetComponent<GenePopupDisplay>();
        genePopupDisplay.activeGene = scriptableObject;
        genePopupDisplay.AvatarToShow = GetComponent<Transform>().gameObject;
        avatarsPopup.SetActive(true);

    }
}