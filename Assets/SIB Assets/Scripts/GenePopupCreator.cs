using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GenePopup" , menuName = "Levels/GenePopup")]
public class GenePopupCreator : ScriptableObject {

    [Header("Gene infos")]
    public Texture geneImg;
    public string geneName;
    public string geneFullName;
    public float proteinLength;
    public string snp;
    public string population;
    public string geneDescENKey;
    public string chromosomeLink;
    public string transcript;
    public string snpName;

    [Header("Gene links")]
    public string uniprotLink;
    public string nextprotLink;
    public string pubmedLink;
    public string pubmedLink2;
    public string snpediaLink;
    public string transcriptLink;

    [Header("Publications")]
    public string publication1Title;
    public string publication1Link;
    public string publication2Title;
    public string publication2Link;

    [Header("Scores")]
    public int objectiveTimeSeconds;
    public int score1Star;
    public int score2Stars;
    public int score3Stars;
}
