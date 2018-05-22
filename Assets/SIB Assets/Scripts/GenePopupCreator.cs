using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GenePopup" , menuName = "Levels/GenePopup")]
public class GenePopupCreator : ScriptableObject {

    public Texture geneImg;
    public string geneName;
    public string geneFullName;
    public float proteinLength;
    public string snp;
    public string population;
    public string geneDescENKey;
    public string chromosomeLink;
    public string uniprotLink;
    public string nextprotLink;
    public string pubmedLink;
    public string snpediaLink;
    public string transcriptLink;
    public string transcript;
    public string snpName;
    public int objectiveTimeSeconds;
    public int score1Star;
    public int score2Stars;
    public int score3Stars;
}
