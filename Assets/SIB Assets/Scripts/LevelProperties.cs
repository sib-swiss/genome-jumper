using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelProperties : MonoBehaviour
{
    private string paramsJson = "levelsParams.json";
    private RoundData[] LvlParams;
    private GameParams LoadedData;

    [Header("Level Informations")] 
    public GenePopupCreator GeneScriptableObject;
    
    //[Header("Set the level number & gene number")]
   // public int CurrentGene;

    [Header("Level Parameters")] 
    public string LvlName;
    public int ObjectiveTime;
    public int Score1Star;
    public int Score2Stars;
    public int Score3Stars;

    [Header("Infos on level gameobjects")]
    public string currentCombinationPlayed;
    public float LevelLength;
    public float SuperVariantCount;
    public float ExonCount;
    public float IntronCount;
    public float UtrCount;
    public float SnpCount;

    [Header("SNP or SuperVariant Taken")] 
    public string ItemRightHand;
    public string ItemHead;

    [Header("UI")] 
    public Text lvlNameTextUi;

    [Header("Avatar infos")]
    public string AvatarName;
    public string GeneName;

    void Start()
    {
        GeneScriptableObject = Resources.Load<GenePopupCreator>("ScriptableObjects/"+SceneManager.GetActiveScene().name);
        ObjectiveTime = GeneScriptableObject.objectiveTimeSeconds;
        Score1Star = GeneScriptableObject.score1Star;
        Score2Stars = GeneScriptableObject.score2Stars;
        Score3Stars = GeneScriptableObject.score3Stars;

        //lvlNameTextUi.text = GeneScriptableObject.geneName.ToString();

        currentCombinationPlayed = PlayerPrefs.GetString("CombinationPlay");
        //CurrentGene = int.Parse(SceneManager.GetActiveScene().name.Replace("Gene", ""));
        ExonCount = GameObject.FindGameObjectsWithTag("Exon").Length;
        IntronCount = GameObject.FindGameObjectsWithTag("Huge Intron").Length + GameObject.FindGameObjectsWithTag("Large Intron").Length + GameObject.FindGameObjectsWithTag("Medium Intron").Length + GameObject.FindGameObjectsWithTag("Small Intron").Length;
        UtrCount = GameObject.FindGameObjectsWithTag("Utr").Length;
        SnpCount = GameObject.FindGameObjectsWithTag("Snp").Length;
        LevelLength = ExonCount + IntronCount + UtrCount + SnpCount;
        SuperVariantCount = GameObject.FindGameObjectsWithTag("SuperVariante").Length;

        AvatarName = PlayerPrefs.GetString("AvatarName");
        PlayerPrefs.SetString("GeneName", GeneScriptableObject.geneName);
        GeneName = GeneScriptableObject.geneName;

        // Sending Analytics event
        Analytics.CustomEvent("level_started", new Dictionary<string, object>
            {
                { "avatar", "Avatar " + AvatarName},
                { "gene",  GeneName }
            }
        );

    }
    /*
    private void LoadGameData()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, paramsJson);

        if(File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);  
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            
               LoadedData = JsonUtility.FromJson<GameParams>(dataAsJson);
               LvlParams = LoadedData.lvlParams;
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }*/
}
