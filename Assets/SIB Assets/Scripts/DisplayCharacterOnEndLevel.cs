using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCharacterOnEndLevel : MonoBehaviour {

    private GameObject Player;
    private GameObject PlayerHead;
    private GameObject PlayerSpecialHead;
    private GameObject Spine;
    private int currentAvatar;
    private int currentGene;

    // Use this for initialization
    void Start () {
        Player = this.gameObject;
        GameObject container = GameObject.Find("UICamera");
        PlayerHead = Player.transform.GetChild(1).gameObject;
        PlayerSpecialHead = Player.transform.GetChild(0).gameObject;
        Spine = Player.transform.GetChild(2).gameObject;

        currentAvatar = PlayerPrefs.GetInt("CombinationPlayAvatar");
        currentGene = PlayerPrefs.GetInt("CombinationPlayGene");

        Spine.GetComponent<Animator>().enabled = false;
        Player.SetActive(true);

        MonoBehaviour[] mbs = this.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour mb in mbs) {
                mb.enabled = false;
        }

        if(Spine.GetComponent<SkeletonAnimator>().initialSkinName == "baby") {
            if(GameObject.Find("FeedingBottle") != null && GameObject.Find("FeedingBottle").gameObject.activeInHierarchy) {
                GameObject.Find("FeedingBottle").gameObject.transform.localPosition = new Vector3(33.7f, -65.7f, 1.83123f);
            }
            if(GameObject.Find("Nightcap") != null && GameObject.Find("Nightcap").gameObject.activeInHierarchy){
                GameObject.Find("Nightcap").gameObject.transform.localPosition = new Vector3(-40.7f, -28.1f, 0);
                GameObject.Find("Nightcap").gameObject.transform.localScale = new Vector3(50, 50, 1.03f);
            }
        }

        Spine.layer = 5;
        Spine.transform.localPosition = new Vector3(Spine.transform.localPosition.x, Spine.transform.localPosition.y, 2);
        Spine.GetComponent<MeshRenderer>().sortingLayerName = "UI";
        Spine.GetComponent<MeshRenderer>().sortingOrder = 120;

        if(currentAvatar == 6 || (currentAvatar == 24 && currentGene == 1)) {
            for(int z = 0; z < PlayerSpecialHead.transform.childCount; z++) {
                if (PlayerSpecialHead.transform.GetChild(z).gameObject.activeInHierarchy) {
                    for(int y = 0; y < PlayerSpecialHead.transform.GetChild(z).childCount; y++) {
                        if (PlayerSpecialHead.transform.GetChild(z).GetChild(y).gameObject.activeInHierarchy) {
                            for(int p = 0; p < PlayerSpecialHead.transform.GetChild(z).GetChild(y).childCount; p++) {
                                if(PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).GetComponent<SpriteRenderer>() != null) {
                                    PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).gameObject.transform.localPosition = new Vector3(PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).gameObject.transform.localPosition.x, PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).gameObject.transform.localPosition.y, 0);
                                    PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).gameObject.layer = 5;
                                    PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                                    PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 122;
                                }
                                else {
                                    for (int u = 0; u < PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).childCount; u++) {
                                        PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).GetChild(u).gameObject.transform.localPosition = new Vector3(PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).GetChild(u).gameObject.transform.localPosition.x, PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).GetChild(u).gameObject.transform.localPosition.y, 0);
                                        PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).GetChild(u).gameObject.layer = 5;
                                        PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).GetChild(u).gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                                        if (PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).GetChild(u).gameObject.name.Contains("eyes") || PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).GetChild(u).gameObject.name.Contains("nose") || PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).GetChild(u).gameObject.name.Contains("mouth") || PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).GetChild(u).gameObject.name.Contains("hair") || PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).GetChild(u).gameObject.name.Contains("tutou")) {
                                            // VERIFIER SI C'EST PAS AU DESSUS
                                            if (PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).GetChild(u).gameObject.name.Contains("long hair blond 2") || PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).GetChild(u).gameObject.name.Contains("red_back")) {
                                                PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).GetChild(u).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 89;
                                            }
                                            PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).GetChild(u).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 122;
                                        }
                                        else {
                                            PlayerSpecialHead.transform.GetChild(z).GetChild(y).GetChild(p).GetChild(u).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 121;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        else {
            for (int i = 0; i < PlayerHead.transform.childCount; i++) {
                // If the avatar is active
                if (PlayerHead.transform.GetChild(i).gameObject.activeInHierarchy) {
                    Debug.Log("PlayerHead i : " + PlayerHead.transform.GetChild(i).gameObject.name);
                    for (int k = 0; k < PlayerHead.transform.GetChild(i).childCount; k++) {
                        // If the gene is active
                        if (PlayerHead.transform.GetChild(i).GetChild(k).gameObject.activeInHierarchy) {
                            Debug.Log("WTF : " + PlayerHead.transform.GetChild(i).GetChild(k).gameObject.name);
                            // We get all the sub parts of the head
                            for (int j = 0; j < PlayerHead.transform.GetChild(i).GetChild(k).childCount; j++) {
                                // If hair
                                if (PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).GetComponent<SpriteRenderer>() != null) {
                                    PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).gameObject.transform.localPosition = new Vector3(PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).gameObject.transform.localPosition.x, PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).gameObject.transform.localPosition.y, 0);
                                    PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).gameObject.layer = 5;
                                    PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                                    // VERIFIER SI C'EST PAS AU DESSUS
                                    if (PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).gameObject.name.Contains("long hair blond 2") || PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).gameObject.name.Contains("red_back")) {
                                        PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 89;
                                    }
                                    else {
                                        PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 122;
                                    }
                                }
                                else {
                                    for (int w = 0; w < PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).childCount; w++) {
                                        PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).GetChild(w).gameObject.transform.localPosition = new Vector3(PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).GetChild(w).gameObject.transform.localPosition.x, PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).GetChild(w).gameObject.transform.localPosition.y, 0);
                                        PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).GetChild(w).gameObject.layer = 5;
                                        PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).GetChild(w).gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                                        if (PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).GetChild(w).gameObject.name.Contains("eyes") || PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).GetChild(w).gameObject.name.Contains("nose") || PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).GetChild(w).gameObject.name.Contains("mouth") || PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).GetChild(w).gameObject.name.Contains("hair") || PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).GetChild(w).gameObject.name.Contains("tutou")) {
                                            PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).GetChild(w).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 122;
                                        }
                                        else {
                                            PlayerHead.transform.GetChild(i).GetChild(k).GetChild(j).GetChild(w).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 121;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
        Player.transform.SetParent(container.transform.GetChild(0).GetChild(11).GetChild(3).GetChild(0).transform);
        this.transform.localScale = new Vector3(45f, 45f, transform.localScale.z);
        this.transform.localPosition = new Vector3(0,-53, 0);
        this.transform.GetChild(2).GetComponent<MeshRenderer>().sortingOrder = 90;
        this.transform.GetChild(2).GetComponent<MeshRenderer>().sortingLayerName = "UI";

        if(currentAvatar == 3 && currentGene == 3) {
            Spine.transform.localPosition = new Vector3(Spine.transform.localPosition.x, Spine.transform.localPosition.y, 0);
            PlayerHead.transform.GetChild(2).transform.localPosition = new Vector3(PlayerHead.transform.GetChild(2).transform.localPosition.x, PlayerHead.transform.GetChild(2).transform.localPosition.y, 0); 
        }

        if((currentAvatar == 2 && (currentGene == 2 || currentGene == 3)) || (currentAvatar == 10 && (currentGene == 2 || currentGene == 3)) || (currentAvatar == 18 && (currentGene == 2 || currentGene == 3)) ) {
            Player.transform.GetChild(5).transform.GetChild(0).gameObject.GetComponent<AudioSource>().enabled = false;
            Player.transform.GetChild(5).transform.GetChild(0).gameObject.SetActive(true);
            Player.transform.GetChild(5).transform.GetChild(0).gameObject.layer = 5;
            Player.transform.GetChild(5).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            Player.transform.GetChild(5).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 90;
            Player.transform.GetChild(5).transform.GetChild(0).gameObject.transform.localPosition = new Vector3(Player.transform.GetChild(5).transform.GetChild(0).gameObject.transform.localPosition.x, 4f, Player.transform.GetChild(5).transform.GetChild(0).gameObject.transform.localPosition.z);
            Player.transform.GetChild(5).transform.GetChild(0).gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
    }
}
