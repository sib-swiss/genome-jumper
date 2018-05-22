using MoreMountains.CorgiEngine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttachmentPersistency : MonoBehaviour {

    public int CurrentAvatar;
    public int CurrentGene;
    private GameObject Player;
    private SkeletonAnimator PlayerSkin;
    private GameObject PlayerRightHand;
    private GameObject PlayerHeadItem;
    private GameObject PlayerHead;
    private GameObject PlayerHeadBaby;
    private GameObject PlayerSuperVariant;
    private string SuperVariantPersistency;
    private AudioSource audio;

    void Awake()
    {
        CurrentAvatar = PlayerPrefs.GetInt("CombinationPlayAvatar");
        CurrentGene = PlayerPrefs.GetInt("CombinationPlayGene");
        Player = GameObject.FindGameObjectWithTag("Player");
        SuperVariantPersistency = PlayerPrefs.GetString("SuperVariantPersistency");
        audio = GameObject.Find("Global").GetComponent<AudioSource>();

        // Attachment
        PlayerSkin = Player.transform.GetChild(2).gameObject.GetComponent<SkeletonAnimator>();
        PlayerRightHand = Player.transform.GetChild(3).gameObject;
        PlayerHead = Player.transform.GetChild(1).gameObject;
        PlayerHeadBaby = Player.transform.GetChild(0).gameObject;
        PlayerHeadItem = Player.transform.GetChild(4).gameObject;
        PlayerSuperVariant = Player.transform.GetChild(5).gameObject;

		if (SuperVariantPersistency != null && !SuperVariantPersistency.Equals("None") ) {
			foreach(string variant in SuperVariantPersistency.Split('|')){
				ToggleSuperVariantEffect (variant);
			}
		}

        // Avatar 01 //
        if (CurrentAvatar == 1 && CurrentGene == 1) {
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 1 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 1 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 02 //
        if (CurrentAvatar == 2 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 2 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "athletic female";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 2 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "athletic female";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 03 //
        if (CurrentAvatar == 3 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 3 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 3 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 04 //
        if (CurrentAvatar == 4 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 4 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller female";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 4 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller female latino";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 05 //
        if (CurrentAvatar == 5 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 5 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller female";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 5 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller female";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 06 //
        if (CurrentAvatar == 6 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 6 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "baby";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 6 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "baby";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 07 //
        if (CurrentAvatar == 7 && CurrentGene == 1) {
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 7 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller female";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 7 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "athletic female";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 08 //
        if (CurrentAvatar == 8 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 8 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 8 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 09 //
        if (CurrentAvatar == 9 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 9 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller female black";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 9 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "athletic female black";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 10 //
        if (CurrentAvatar == 10 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 10 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "athletic male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 10 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "athletic male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 11 //
        if (CurrentAvatar == 11 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 11 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            Player.transform.localScale = new Vector3(0.19f, 0.19f, 0.25f);
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 11 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            Player.transform.localScale = new Vector3(0.19f, 0.19f, 0.25f);
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 12 //
        if (CurrentAvatar == 12 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 12 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "amish";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 12 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "amish";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 13 //
        if (CurrentAvatar == 13 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 13 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "athletic female";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 13 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "athletic female";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 14 //
        if (CurrentAvatar == 14 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 14 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 14 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 15 //
        if (CurrentAvatar == 15 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 15 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 15 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 16 //
        if (CurrentAvatar == 16 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 16 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 16 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 17 //
        if (CurrentAvatar == 17 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 17 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 17 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 18 //
        if (CurrentAvatar == 18 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 18 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller female";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 18 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller female";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 19 //
        if (CurrentAvatar == 19 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 19 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 19 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 20 //
        if (CurrentAvatar == 20 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 20 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller female black";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 20 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller female black";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 21 //
        if (CurrentAvatar == 21 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 21 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller female";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 21 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller female";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 22 //
        if (CurrentAvatar == 22 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 22 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male latino";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 22 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male latino";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 23 //
        if (CurrentAvatar == 23 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 23 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male black";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 23 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male black";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 24 //
        if (CurrentAvatar == 24 && CurrentGene == 1){
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 24 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "baby";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 24 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller female latino";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        // Avatar 25 //
        if (CurrentAvatar == 25 && CurrentGene == 1) {
            // Skin
            PlayerSkin.initialSkinName = "unknown";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "true");
        }
        if (CurrentAvatar == 25 && CurrentGene == 2){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
        if (CurrentAvatar == 25 && CurrentGene == 3){
            // Skin
            PlayerSkin.initialSkinName = "city dweller male latino";
            //SetThePlayerHead
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", "false");
        }
    }

    private void Update()
    {
        bool SnpTriggered = GameObject.FindGameObjectWithTag("Snp").transform.GetChild(2).GetComponent<SNPVariant>().SnpTriggered;
        // Avatar 01 //
        if (CurrentAvatar == 1 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if(!SnpTriggered) {
                PlayerHead.transform.GetChild(25).gameObject.SetActive(true);
            }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 1 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(0).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 1 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(0).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }

        // Avatar 02 //
        if (CurrentAvatar == 2 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { 
				PlayerHead.transform.GetChild(25).gameObject.SetActive(true); 
			} else { 
				PlayerHead.transform.GetChild(25).gameObject.SetActive(false); 
			}
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 2 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);

				PlayerHead.transform.GetChild(25).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(1).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 2 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(1).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }

        // Avatar 03 //
        if (CurrentAvatar == 3 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 3 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(2).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 3 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(2).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }

        // Avatar 04 //
        if (CurrentAvatar == 4 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 4 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(3).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 4 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(3).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }

        // Avatar 05 //
        if (CurrentAvatar == 5 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 5 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(4).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 5 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(4).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }

        // Avatar 06 //
        if (CurrentAvatar == 6 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(25).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
                PlayerHeadBaby.gameObject.SetActive(true);
                PlayerHeadBaby.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 6 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(true);
            PlayerHeadBaby.transform.GetChild(0).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHeadBaby.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHeadBaby.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                PlayerHeadBaby.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 6 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(true);
            PlayerHeadBaby.transform.GetChild(0).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHeadBaby.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHeadBaby.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
                PlayerHeadBaby.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }

        // Avatar 07 //
        if (CurrentAvatar == 7 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 7 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(6).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(6).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(6).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(6).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
			PlayerHeadItem.transform.GetChild(3).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 7 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(6).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(6).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
			PlayerHeadItem.transform.GetChild(3).gameObject.SetActive(true);
        }

        // Avatar 08 //
        if (CurrentAvatar == 8 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 8 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(7).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(7).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(7).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(7).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 8 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(7).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(7).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(7).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(7).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
			PlayerHeadItem.transform.GetChild(7).gameObject.SetActive(true);
        }

        // Avatar 09 //
        if (CurrentAvatar == 9 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 9 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(8).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(8).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(8).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(8).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 9 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(8).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(8).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(8).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(8).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }

        // Avatar 10 //
        if (CurrentAvatar == 10 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 10 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(9).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(9).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(9).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(9).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 10 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(9).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(9).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(9).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(9).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }

        // Avatar 11 //
        if (CurrentAvatar == 11 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else {
                PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
                Player.transform.localScale = new Vector3(0.19f, 0.19f, 0.25f);
            }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 11 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(10).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(10).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(10).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(10).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 11 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(10).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(10).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(10).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(10).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
			PlayerRightHand.transform.GetChild(13).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        // Avatar 12 //
        if (CurrentAvatar == 12 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 12 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(11).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(11).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(11).transform.GetChild(1).gameObject.SetActive(true);
				PlayerHeadItem.transform.GetChild(7).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(11).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(2).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 12 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(11).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(11).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(11).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(11).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
			PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(2).gameObject.SetActive(true);
			PlayerHeadItem.transform.GetChild(7).gameObject.SetActive(true);
        }
        // Avatar 13 //
        if (CurrentAvatar == 13 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 13 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(12).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(12).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(12).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(12).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 13 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(12).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(12).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(12).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(12).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(13).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        // Avatar 14 //
        if (CurrentAvatar == 14 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 14 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(13).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(13).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(13).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(13).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 14 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(13).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(13).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(13).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(13).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
			PlayerRightHand.transform.GetChild(5).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        // Avatar 15 //
        if (CurrentAvatar == 15 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 15 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(14).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(14).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(14).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(14).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(7).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 15 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(14).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(14).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(14).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(14).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(7).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(5).gameObject.SetActive(true);
        }
        // Avatar 16 //
        if (CurrentAvatar == 16 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 16 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(15).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(15).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(15).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(15).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 16 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(15).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(15).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(15).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(15).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(11).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        // Avatar 17 //
        if (CurrentAvatar == 17 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 17 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(16).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(16).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(16).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(16).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
			PlayerRightHand.transform.GetChild(8).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
			PlayerHeadItem.transform.GetChild(4).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 17 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(16).transform.GetChild(1).gameObject.SetActive(true);
				PlayerRightHand.transform.GetChild(8).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(16).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(16).transform.GetChild(2).gameObject.SetActive(true);
				PlayerRightHand.transform.GetChild(8).gameObject.SetActive(false);
            }
            PlayerHead.transform.GetChild(16).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);

			// Main
			PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);

			// Chapeau
			PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
			PlayerHeadItem.transform.GetChild(4).gameObject.SetActive(true);
        }
        // Avatar 18 //
        if (CurrentAvatar == 18 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 18 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(17).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(17).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(17).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(17).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 18 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(17).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(17).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(17).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(17).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
			PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        // Avatar 19 //
        if (CurrentAvatar == 19 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 19 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(18).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(18).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(18).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(18).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
			PlayerHeadItem.transform.GetChild(6).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 19 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(18).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(18).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(18).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(18).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
			PlayerHeadItem.transform.GetChild(6).gameObject.SetActive(true);
        }
        // Avatar 20 //
        if (CurrentAvatar == 20 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { 
				PlayerHead.transform.GetChild(25).gameObject.SetActive(true); 
			}
            else { 
				PlayerHead.transform.GetChild(25).gameObject.SetActive(false); 
			}
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 20 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(19).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(19).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(19).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(19).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);

            // Chapeau Strangebrain
			PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(false);
			PlayerHeadItem.transform.GetChild(5).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 20 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(19).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(19).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(19).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(19).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
			PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(false);
			PlayerHeadItem.transform.GetChild(5).gameObject.SetActive(true);
			PlayerHeadItem.transform.GetChild(6).gameObject.SetActive(true);
        }
        // Avatar 21 //
        if (CurrentAvatar == 21 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 21 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(20).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(20).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(20).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(20).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 21 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(20).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(20).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(20).transform.GetChild(2).gameObject.SetActive(true);
				PlayerHeadItem.transform.GetChild(7).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(20).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(1).gameObject.SetActive(true);
        }
        // Avatar 22 //
        if (CurrentAvatar == 22 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 22 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(21).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(21).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(21).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(21).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 22 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(21).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(21).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(21).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(21).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        // Avatar 23 //
        if (CurrentAvatar == 23 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 23 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(22).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(22).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(22).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(22).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 23 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);;
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(22).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(22).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(22).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(22).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        // Avatar 24 //
        if (CurrentAvatar == 24 && CurrentGene == 1)
        {
            // Tête
            if (!SnpTriggered) { 
				// Skin baby
				PlayerHeadBaby.gameObject.SetActive(false);
				PlayerHead.transform.GetChild(25).gameObject.SetActive(true); 
			}
            else { 
				PlayerHeadBaby.gameObject.SetActive(true);
				PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
				//The head choice is managed in SNPVariant for the first gene of an avatar especially for the baby
			}
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 24 && CurrentGene == 2)
        {
            // Tête
            if (!SnpTriggered) {
				// Skin baby
				PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
				PlayerHeadBaby.gameObject.SetActive(true);
				PlayerHeadBaby.gameObject.transform.GetChild(1).gameObject.SetActive(true);
				PlayerHeadBaby.gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            }
            else {
				PlayerHeadBaby.gameObject.SetActive(false);
                PlayerHead.transform.GetChild(23).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(23).transform.GetChild(1).gameObject.SetActive(true);
                PlayerRightHand.transform.GetChild(4).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(23).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 24 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(23).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(23).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(23).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(23).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(4).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        // Avatar 25 //
        if (CurrentAvatar == 25 && CurrentGene == 1)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) { PlayerHead.transform.GetChild(25).gameObject.SetActive(true); }
            else { PlayerHead.transform.GetChild(25).gameObject.SetActive(false); }
            // Main
            PlayerRightHand.transform.GetChild(0).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 25 && CurrentGene == 2)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(24).transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(24).transform.GetChild(0).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(24).transform.GetChild(1).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(24).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(9).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (CurrentAvatar == 25 && CurrentGene == 3)
        {
            // Skin baby
            PlayerHeadBaby.gameObject.SetActive(false);
            // Tête
            if (!SnpTriggered) {
                PlayerHead.transform.GetChild(24).transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                PlayerHead.transform.GetChild(24).transform.GetChild(1).gameObject.SetActive(false);
                PlayerHead.transform.GetChild(24).transform.GetChild(2).gameObject.SetActive(true);
            }
            PlayerHead.transform.GetChild(24).gameObject.SetActive(true);
            PlayerHead.transform.GetChild(25).gameObject.SetActive(false);
            // Main
            PlayerRightHand.transform.GetChild(9).gameObject.SetActive(true);
            // Chapeau
            PlayerHeadItem.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void ToggleSuperVariantEffect(string variant) {

        if (variant == "SuperMuscle") {
			// SuperVariante
			PlayerSuperVariant.transform.GetChild(0).gameObject.SetActive(true);
			Player.GetComponent<CorgiController>().DefaultParameters.SpeedFactor = 1.50f;
            //Sound is located on the prefab
            PlayerSuperVariant.transform.GetChild(0).GetComponent<AudioSource>().enabled = false;
        }
        if (variant == "Deef") {
			AudioSource asrc = Player.GetComponent<AudioSource> ();
			asrc.clip = Resources.Load("Sounds/deafness") as AudioClip;
			asrc.Play();
			asrc.loop = true;
        }
        if (variant == "Blind") {

        }
        if (variant == "Usher") {
            GameObject.Find("SuperVariantEffects").gameObject.transform.GetChild(2).gameObject.SetActive(true);
			AudioSource asrc = Player.GetComponent<AudioSource> ();
			asrc.clip = Resources.Load("Sounds/deafness") as AudioClip;
			asrc.Play();
			asrc.loop = true;
        }
        if (variant == "Beer") {
            GameObject.Find("SuperVariantEffects").gameObject.transform.GetChild(0).gameObject.SetActive(true);
			ParticleSystem ps = GameObject.Find("bubbleBeer").GetComponent<ParticleSystem>();
			ParticleSystem.MainModule psmm = ps.main;
			psmm.maxParticles = 500;
			ParticleSystem.EmissionModule psem = ps.emission;
			psem.rateOverTime = 100;
        }
        if (variant == "Cannabis") {
            GameObject.Find("SuperVariantEffects").gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (variant == "Speed") {
            Player.GetComponent<CorgiController>().DefaultParameters.SpeedFactor = 1.50f;
        }


    }
}
