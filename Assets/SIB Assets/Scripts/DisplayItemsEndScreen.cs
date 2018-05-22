using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItemsEndScreen : MonoBehaviour {

    private SNPVariant SnpBubbleVariant;
    private GameObject HeadItems;
    private GameObject HandItems;

	// Use this for initialization
	void Start () {

        SnpBubbleVariant = GameObject.FindGameObjectWithTag("Snp").transform.GetChild(2).gameObject.GetComponent<SNPVariant>();
        HeadItems = transform.GetChild(0).gameObject;
        HandItems = transform.GetChild(1).gameObject;

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

        if(PlayerPrefs.GetString("CurrentAvatarGeneCombination").ToString() == "A17G1") {
            HeadItems.transform.GetChild(HeadItems.transform.childCount - 1).gameObject.SetActive(true);
        }

    }
}
