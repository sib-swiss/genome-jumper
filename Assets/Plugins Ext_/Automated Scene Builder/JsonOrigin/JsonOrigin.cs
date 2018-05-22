using System;
using System.Collections.Generic;

[System.Serializable]
public class JsonOrigin
{
    public string id;
    public string geneName;
    public string transcript;
    public string stopCodon;
    public string chromosome;
    public int chromosomeLength;
    public int transcriptStartPosition;
    public int transcriptEndPosition;
    public double relativePositionPercentage;
    public string transcriptDirection;
    public int exonsCount;
    public int exonsRealLength;
    public int geneRealLength;
    public int totalNormalizedLength;
    public int totalNormalizedWalkableLength;
    public string description;
    public float proteinLength;
    public ExternalLinks externalLinks;
    public MutationInfo mutationInfo;
    public DebugInfo debugInfo;
    public ClassCount classCount;
    public List<LevelData> levelData;
}



