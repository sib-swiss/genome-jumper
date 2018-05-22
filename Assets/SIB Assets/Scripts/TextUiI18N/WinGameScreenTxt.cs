using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Lean.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VoxelBusters.Utility;

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
	public GameObject PubmedLogo;
	public GameObject SNPediaLogo;

	private GenePopupCreator activeGene;

	private void Awake()
	{
		activeGene = GameObject.Find("LevelManager").GetComponent<LevelProperties>().GeneScriptableObject;

		if (activeGene == null || activeGene.geneName == null || activeGene.geneName.Equals(""))
		{
			GeneTermine.SetArg("no infos", 0);
		}
		else 
			GeneTermine.SetArg(activeGene.geneName, 0);
		ProteinLength.SetArg(activeGene.proteinLength, 0);
		setAllText(GeneName, activeGene.geneFullName);
		setAllText(SnpVariant, activeGene.snp);
		Population.PhraseName = activeGene.population;
        if(SceneManager.GetActiveScene().name.ToLower() == "foxo3alternate") {
            Description.PhraseName = "gene-foxo3-text";
        }
        else {
            Description.PhraseName = "gene-" + SceneManager.GetActiveScene().name.ToLower() + "-text";
        }
		
		
		SnpPresented.SetArg(activeGene.snpName, 0);
		TranscriptLinks.SetArg(activeGene.transcript, 0);
		TranscriptLinks.GetComponent<TextLinks>().Link = activeGene.transcriptLink;

		if (!activeGene.chromosomeLink.Equals("") && activeGene.chromosomeLink != null)
		{
			ChromoWalkLogo.SetActive(true);
			ChromoWalkLogo.GetComponent<LogoLinks>().SetLink(activeGene.chromosomeLink);
		}
			
		if (!activeGene.nextprotLink.Equals("") && activeGene.nextprotLink != null)
		{
			NextProtLogo.SetActive(true);
			NextProtLogo.GetComponent<LogoLinks>().SetLink(activeGene.nextprotLink);
		}
			
		if (!activeGene.pubmedLink.Equals("") && activeGene.pubmedLink != null)
		{
			PubmedLogo.SetActive(true);
			PubmedLogo.GetComponent<LogoLinks>().SetLink(activeGene.pubmedLink);
		}
			
		if (!activeGene.uniprotLink.Equals("") && activeGene.uniprotLink != null)
		{
			UniprotLogo.SetActive(true);
			UniprotLogo.GetComponent<LogoLinks>().SetLink(activeGene.uniprotLink);
		}
			
		if (!activeGene.snpediaLink.Equals("") && activeGene.snpediaLink != null)
		{
			SNPediaLogo.SetActive(true);
			SNPediaLogo.GetComponent<LogoLinks>().SetLink(activeGene.snpediaLink);
		}
			
	}

	void setAllText(LeanLocalizedTextArgs leanLocalizedTextArgs, string text)
	{
		leanLocalizedTextArgs.PhraseName = text;
		leanLocalizedTextArgs.FallbackText = text;
		leanLocalizedTextArgs.GetComponent<Text>().text = text;
	}
	
}
