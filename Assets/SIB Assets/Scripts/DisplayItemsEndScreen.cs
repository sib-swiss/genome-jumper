using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItemsEndScreen : MonoBehaviour {

    private SNPVariant SnpBubbleVariant;
    private GameObject HeadItems;
    private GameObject HandItems;

    private int avatarNumber;
    private int geneNumber;

	// Use this for initialization
	void Start () {

        SnpBubbleVariant = GameObject.FindGameObjectWithTag("Snp").transform.GetChild(2).gameObject.GetComponent<SNPVariant>();
        HeadItems = transform.GetChild(0).gameObject;
        HandItems = transform.GetChild(1).gameObject;

        avatarNumber = PlayerPrefs.GetInt("CombinationPlayAvatar");
        geneNumber = PlayerPrefs.GetInt("CombinationPlayGene");


        for (int i = 0; i < HeadItems.transform.childCount; i++) {
            if(HeadItems.transform.GetChild(i).gameObject.name.ToString() == SnpBubbleVariant.HeadItemPersistency.ToString()) {
                HeadItems.transform.GetChild(i).gameObject.SetActive(true);
            }
            else {
                HeadItems.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        for (int k = 0; k < HandItems.transform.childCount; k++) {
            if (HandItems.transform.GetChild(k).gameObject.name.ToString() == SnpBubbleVariant.HandItemPersistency.ToString()) {
                HandItems.transform.GetChild(k).gameObject.SetActive(true);
            }
            else {
                HandItems.transform.GetChild(k).gameObject.SetActive(false);
            }
        }

        if(avatarNumber == 12 && (geneNumber == 2 || geneNumber == 3)) {
            HeadItems.transform.GetChild(7).gameObject.SetActive(true);
        }


        if(avatarNumber == 17 && geneNumber == 1) {
            //HeadItems.transform.GetChild(HeadItems.transform.childCount - 1).gameObject.SetActive(true);
        }

        if(avatarNumber == 17 && geneNumber == 2) {
            HandItems.transform.GetChild(8).gameObject.SetActive(true);
            HandItems.transform.GetChild(8).gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            HandItems.transform.GetChild(8).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 146;
            HandItems.transform.GetChild(8).gameObject.layer = 5;
            HeadItems.transform.GetChild(4).gameObject.SetActive(true);
            HandItems.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            HandItems.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 146;
            HandItems.transform.GetChild(4).gameObject.layer = 5;
        }

        if(avatarNumber == 19) {
            HeadItems.transform.GetChild(6).gameObject.SetActive(true);
            HeadItems.transform.GetChild(6).GetChild(0).GetComponent<Renderer>().sortingOrder = 134;
            HeadItems.transform.GetChild(6).GetChild(0).GetComponent<Renderer>().sortingLayerName = "UI";
        }

        if (avatarNumber == 7 && (geneNumber == 2 || geneNumber == 3)) {
            HeadItems.transform.GetChild(3).gameObject.SetActive(true);
        }

        if(avatarNumber == 8 && geneNumber == 3) {
            HeadItems.transform.GetChild(7).gameObject.SetActive(true);
        }

        if (avatarNumber == 20 && (geneNumber == 2 || geneNumber == 3)) {
            HeadItems.transform.GetChild(5).gameObject.SetActive(true);
            HeadItems.transform.GetChild(6).gameObject.SetActive(true);
            HeadItems.transform.GetChild(6).gameObject.transform.localPosition = new Vector3(HeadItems.transform.GetChild(6).gameObject.transform.localPosition.x+60, HeadItems.transform.GetChild(6).gameObject.transform.localPosition.y-20, HeadItems.transform.GetChild(6).gameObject.transform.localPosition.z);
            HeadItems.transform.GetChild(6).GetChild(0).GetComponent<Renderer>().sortingOrder = 134;
            HeadItems.transform.GetChild(6).GetChild(0).GetComponent<Renderer>().sortingLayerName = "UI";
        }

        if(avatarNumber == 21 && geneNumber == 3) {
            HeadItems.transform.GetChild(1).gameObject.SetActive(true);
            HeadItems.transform.GetChild(1).gameObject.transform.localPosition = new Vector3(HeadItems.transform.GetChild(1).gameObject.transform.localPosition.x, HeadItems.transform.GetChild(1).gameObject.transform.localPosition.y - 16, HeadItems.transform.GetChild(1).gameObject.transform.localPosition.z);
            HeadItems.transform.GetChild(7).gameObject.SetActive(true);
        }

        if(avatarNumber == 23 && geneNumber == 3) {
            HeadItems.transform.GetChild(6).GetChild(0).GetComponent<Renderer>().sortingOrder = 134;
            HeadItems.transform.GetChild(6).GetChild(0).GetComponent<Renderer>().sortingLayerName = "UI";
        }
    }
}
