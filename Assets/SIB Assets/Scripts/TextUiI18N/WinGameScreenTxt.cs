using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Lean.Localization;
using PaperPlaneTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinGameScreenTxt : MonoBehaviour {

	[Header("Text with variables")]
	public LeanLocalizedTextArgs GeneTermine;
	public LeanLocalizedTextArgs GeneName;
	public LeanLocalizedTextArgs ProteinLength;
	public LeanLocalizedTextArgs SnpVariant;
	public LeanLocalizedTextArgs Population;
	public LeanLocalizedTextArgs Description;
	public LeanLocalizedTextArgs SnpPresented;
	public LeanLocalizedTextArgs TranscriptLinks;

	[Header("Logo ScientificLinks")] 
	public GameObject ChromoWalkLogo;
	public GameObject UniprotLogo;
	public GameObject NextProtLogo;
	public GameObject SNPediaLogo;
    public GameObject PubmedLogo;
    public GameObject PubmedLogo2;

    [Header("Publications")]
    public GameObject Publication1;
    public GameObject Publication2;

    private GenePopupCreator activeGene;

    public GameObject[] ButtonsToDisable;

    private void Awake() {

        for(int i = 0; i < ButtonsToDisable.Length; i++)
        {
            ButtonsToDisable[i].SetActive(false);
        }

        activeGene = GameObject.Find("LevelManager").GetComponent<LevelProperties>().GeneScriptableObject;

        if (activeGene == null || activeGene.geneName == null || activeGene.geneName.Equals("")) {
            GeneTermine.SetArg("no infos", 0);
        }
        else
            GeneTermine.SetArg(activeGene.geneName, 0);
        ProteinLength.SetArg(activeGene.proteinLength, 0);
        setAllText(GeneName, activeGene.geneFullName);
        setAllText(SnpVariant, activeGene.snp);
        Population.PhraseName = activeGene.population;
        if (SceneManager.GetActiveScene().name.ToLower() == "foxo3alternate") {
            Description.PhraseName = "gene-foxo3-text";
        }
        else {
            Description.PhraseName = "gene-" + SceneManager.GetActiveScene().name.ToLower() + "-text";
        }


        SnpPresented.SetArg(activeGene.snpName, 0);
        TranscriptLinks.SetArg(activeGene.transcript, 0);
        TranscriptLinks.GetComponent<TextLinks>().Link = activeGene.transcriptLink;

        if (!activeGene.publication1Title.Equals("")) {
            Publication1.SetActive(true);
            Publication1.GetComponent<Text>().text = " • " + activeGene.publication1Title;
            Publication1.GetComponent<Button>().onClick.AddListener(() => {
                Application.OpenURL(activeGene.publication1Link);
            });
        }

        if (!activeGene.publication2Title.Equals("")) {
            Publication2.SetActive(true);
            Publication2.GetComponent<Text>().text = " • " + activeGene.publication2Title;
            Publication2.GetComponent<Button>().onClick.AddListener(() => {
                Application.OpenURL(activeGene.publication1Link);
            });
        }

        if (!activeGene.chromosomeLink.Equals("") && activeGene.chromosomeLink != null) {
            ChromoWalkLogo.SetActive(true);
            ChromoWalkLogo.GetComponent<LogoLinks>().SetLink(activeGene.chromosomeLink);
        }
        else {
            if(ChromoWalkLogo != null) {
                ChromoWalkLogo.SetActive(false);
            }
        }

        if (!activeGene.nextprotLink.Equals("") && activeGene.nextprotLink != null) {
            NextProtLogo.SetActive(true);
            NextProtLogo.GetComponent<LogoLinks>().SetLink(activeGene.nextprotLink);
        }
        else {
            if(NextProtLogo != null) {
                NextProtLogo.SetActive(false);
            }
        }

        if (!activeGene.pubmedLink.Equals("") && activeGene.pubmedLink != null) {
            PubmedLogo.SetActive(true);
            PubmedLogo.GetComponent<LogoLinks>().SetLink(activeGene.pubmedLink);
        }
        else {
            if(PubmedLogo != null)
            {
                PubmedLogo.SetActive(false);
            }
        }

        if (!activeGene.pubmedLink2.Equals("") && activeGene.pubmedLink2 != null) {
            PubmedLogo2.SetActive(true);
            PubmedLogo2.GetComponent<LogoLinks>().SetLink(activeGene.pubmedLink2);
        }
        else {
            if(PubmedLogo2 != null)
            {
                PubmedLogo2.SetActive(false);
            }
        }

        if (!activeGene.uniprotLink.Equals("") && activeGene.uniprotLink != null) {
            UniprotLogo.SetActive(true);
            UniprotLogo.GetComponent<LogoLinks>().SetLink(activeGene.uniprotLink);
        }
        else {
            if(UniprotLogo != null) {
                UniprotLogo.SetActive(false);
            }
        }

        if (!activeGene.snpediaLink.Equals("") && activeGene.snpediaLink != null) {
            SNPediaLogo.SetActive(true);
            SNPediaLogo.GetComponent<LogoLinks>().SetLink(activeGene.snpediaLink);
        }
        else {
            if(SNPediaLogo != null) {
                SNPediaLogo.SetActive(false);
            }
        }

        if (RateBox.Instance.Statistics.DialogIsRated == false && (PlayerPrefs.GetInt("gamesPlayedWithoutRating")% PlayerPrefs.GetInt("incrementRateCTA") == 0) && PlayerPrefs.GetInt("gamesPlayedWithoutRating") != 0) {
            RateBox.Instance.Show();
            PlayerPrefs.SetInt("gamesPlayedWithoutRating", 0);
            if(PlayerPrefs.GetInt("incrementRateCTA") < 16) {
                PlayerPrefs.SetInt("incrementRateCTA", PlayerPrefs.GetInt("incrementRateCTA") + 4); // TO MODIFY FOR REFRESH RATE OF AD
            }
            else {
                PlayerPrefs.SetInt("incrementRateCTA", 16);
            }
            
        }
        else if (RateBox.Instance.Statistics.DialogIsRated == false) {
            int increment = PlayerPrefs.GetInt("gamesPlayedWithoutRating");
            PlayerPrefs.SetInt("gamesPlayedWithoutRating", increment + 1);
        }
    }

	void setAllText(LeanLocalizedTextArgs leanLocalizedTextArgs, string text)
	{
		leanLocalizedTextArgs.PhraseName = text;
		leanLocalizedTextArgs.FallbackText = text;
		leanLocalizedTextArgs.GetComponent<Text>().text = text;
	}
	
}
