using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GenePopupDisplay : MonoBehaviour
{

    public GenePopupCreator activeGene;

    [Header("Text with variables")]
    public Text GeneTermine;
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

    public GameObject AvatarToShow;

    // Use this for initialization
    void OnEnable()
    {
        GeneTermine = transform.GetChild(1).GetComponent<Text>();
        GeneName = transform.GetChild(3).GetComponent<LeanLocalizedTextArgs>();
        ProteinLength = transform.GetChild(5).GetComponent<LeanLocalizedTextArgs>();
        SnpVariant = transform.GetChild(7).GetComponent<LeanLocalizedTextArgs>();
        Population = transform.GetChild(9).GetComponent<LeanLocalizedTextArgs>();
        Description = transform.GetChild(10).GetChild(0).GetChild(0).GetChild(0).GetComponent<LeanLocalizedTextArgs>();
        SnpPresented = transform.GetChild(10).GetChild(0).GetChild(0).GetChild(1).GetComponent<LeanLocalizedTextArgs>();
        TranscriptLinks = transform.GetChild(10).GetChild(0).GetChild(0).GetChild(2).GetComponent<LeanLocalizedTextArgs>();

        ChromoWalkLogo = transform.GetChild(11).GetChild(0).gameObject;
        UniprotLogo = transform.GetChild(11).GetChild(1).gameObject;
        NextProtLogo = transform.GetChild(11).GetChild(2).gameObject;
        SNPediaLogo = transform.GetChild(11).GetChild(3).gameObject;
        PubmedLogo = transform.GetChild(11).GetChild(4).gameObject;
        PubmedLogo2 = transform.GetChild(11).GetChild(5).gameObject;

        GeneTermine.GetComponent<Text>().text = activeGene.geneName;
        ProteinLength.SetArg(activeGene.proteinLength, 0);
        setAllText(GeneName, activeGene.geneFullName);
        setAllText(SnpVariant, activeGene.snp);
        Population.PhraseName = activeGene.population;
        Description.PhraseName = "gene-" + activeGene.geneName.ToLower() + "-text";

        SnpPresented.SetArg(activeGene.snpName, 0);
        TranscriptLinks.SetArg(activeGene.transcript, 0);

        if (!activeGene.chromosomeLink.Equals("") && activeGene.chromosomeLink != null)
        {
            ChromoWalkLogo.SetActive(true);
            ChromoWalkLogo.GetComponent<LogoLinks>().SetLink(activeGene.chromosomeLink);
        }
        else {
            ChromoWalkLogo.SetActive(false);
        }

        if (!activeGene.nextprotLink.Equals("") && activeGene.nextprotLink != null)
        {
            NextProtLogo.SetActive(true);
            NextProtLogo.GetComponent<LogoLinks>().SetLink(activeGene.nextprotLink);
        }
        else {
            NextProtLogo.SetActive(false);
        }

        if (!activeGene.pubmedLink.Equals("") && activeGene.pubmedLink != null)
        {
            PubmedLogo.SetActive(true);
            PubmedLogo.GetComponent<LogoLinks>().SetLink(activeGene.pubmedLink);
        }
        else {
            PubmedLogo.SetActive(false);
        }

        if (!activeGene.pubmedLink.Equals("") && activeGene.pubmedLink2 != null) {
            PubmedLogo2.SetActive(true);
            PubmedLogo2.GetComponent<LogoLinks>().SetLink(activeGene.pubmedLink2);
        }
        else {
            PubmedLogo2.SetActive(false);
        }

        if (!activeGene.uniprotLink.Equals("") && activeGene.uniprotLink != null)
        {
            UniprotLogo.SetActive(true);
            UniprotLogo.GetComponent<LogoLinks>().SetLink(activeGene.uniprotLink);
        }
        else {
            UniprotLogo.SetActive(false);
        }

        if (!activeGene.snpediaLink.Equals("") && activeGene.snpediaLink != null)
        {
            SNPediaLogo.SetActive(true);
            SNPediaLogo.GetComponent<LogoLinks>().SetLink(activeGene.snpediaLink);
        }
        else {
            SNPediaLogo.SetActive(false);
        }

    }

    void setAllText(LeanLocalizedTextArgs leanLocalizedTextArgs, string text)
    {
        leanLocalizedTextArgs.PhraseName = text;
        leanLocalizedTextArgs.FallbackText = text;
        leanLocalizedTextArgs.GetComponent<Text>().text = text;
    }

    public void HidePopup()
    {
        AvatarToShow.SetActive(true);
        GetComponent<Transform>().gameObject.SetActive(false);
    }

}
