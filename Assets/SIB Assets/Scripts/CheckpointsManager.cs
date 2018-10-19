using System.Collections;
using System.Collections.Generic;
using ExifLibrary;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class CheckpointsManager : MonoBehaviour {

 	public GameObject checkpoint;
	private int intronInterval = 3; //instanciate a checkpoint each XX exons
	// Use this for initialization
	private LevelManager levelManager;
    //private int incNeeded = 10;
	//private int inc = 0;

	void Awake () {
        //Instanciate a checkpoint
        if (!PlayerPrefs.GetString("checkpoint").Equals("") && !PlayerPrefs.GetString("checkpoint").Equals("Spawn"))
        {
            gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("checkpointPosX"), PlayerPrefs.GetFloat("checkpointPosY"), PlayerPrefs.GetFloat("checkpointPosZ"));
        }
    }

    // Update is called once per frame
    void Update () {
        if (!levelManager)
        {
            levelManager = GameObject.FindObjectOfType(typeof(LevelManager)) as LevelManager;
            GameObject[] introns = GameObject.FindGameObjectsWithTag("Intron Flag");
            //Debug.Log(introns.Length);
            int counter = 0;
            int counterWaypoint = 1;
            for (int i = 0; i < introns.Length; i++)
            {
                counter++;
                if (counter >= intronInterval)
                {
	                var clone = Instantiate(checkpoint, introns[i].transform.position, introns[i].transform.rotation);
                    clone.name = "Checkpoint " + counterWaypoint;

                    // Hotfix for some checkpoints missplaced
                    if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ABCC11" && clone.name == "Checkpoint 4") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 0.7f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "OPN1MW" && clone.name == "Checkpoint 1") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 0.7f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "AKT1" && clone.name == "Checkpoint 1") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 0.75f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "EPOR" && clone.name == "Checkpoint 1") {
                        clone.transform.position = new Vector3(clone.transform.position.x  - 1.869f, clone.transform.position.y + 1.046f, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ALDH2" && clone.name == "Checkpoint 2") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 3.75f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SLC45A2" && clone.name == "Checkpoint 2") {
                        clone.transform.position = new Vector3(clone.transform.position.x -0.2f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "TCF1L2" && clone.name == "Checkpoint 1") {
                        clone.transform.position = new Vector3(clone.transform.position.x - 8.75f, clone.transform.position.y + 1, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "TCF1L2" && clone.name == "Checkpoint 2") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 7.6f, clone.transform.position.y+1, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "BRCA1" && clone.name == "Checkpoint 7") {
                        clone.transform.position = new Vector3(clone.transform.position.x - 1.75f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "BRCA1" && clone.name == "Checkpoint 4") {
                        clone.transform.position = new Vector3(clone.transform.position.x - 1.4f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "BRCA1" && clone.name == "Checkpoint 3") {
                        clone.transform.position = new Vector3(clone.transform.position.x - 5f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "BRCA1" && clone.name == "Checkpoint 2") {
                        clone.transform.position = new Vector3(clone.transform.position.x - 1.5f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "BRCA1" && clone.name == "Checkpoint 1") {
                        clone.transform.position = new Vector3(clone.transform.position.x - 0.55f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "LCT" && clone.name == "Checkpoint 5") {
                        clone.transform.position = new Vector3(clone.transform.position.x - 6.5f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "LCT" && clone.name == "Checkpoint 4") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 43f - 0.2f, clone.transform.position.y + 1, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "LCT" && clone.name == "Checkpoint 3") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 3f, clone.transform.position.y , clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "LCT" && clone.name == "Checkpoint 2") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 3f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "LCT" && clone.name == "Checkpoint 1") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 7f, clone.transform.position.y+2f, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "HERC2" && clone.name == "Checkpoint 2") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 3.75f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "LCT" && clone.name == "Checkpoint 1") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 4.25f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "PER2" && clone.name == "Checkpoint 6") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 3.5f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "PER2" && clone.name == "Checkpoint 4") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 9.5f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "PER2" && clone.name == "Checkpoint 3") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 3.5f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "PER2" && clone.name == "Checkpoint 2") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 12.5f, clone.transform.position.y-2f, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "BRAF" && clone.name == "Checkpoint 3") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 3.5f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "BRAF" && clone.name == "Checkpoint 1") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 3.75f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ACTN3" && clone.name == "Checkpoint 5") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 3.75f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ACTN3" && clone.name == "Checkpoint 4") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 7.25f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ACTN3" && clone.name == "Checkpoint 2") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 3.5f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ABCC11" && clone.name == "Checkpoint 8") {
                        clone.transform.position = new Vector3(clone.transform.position.x - 8f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ABCC11" && clone.name == "Checkpoint 4") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 4.5f, clone.transform.position.y + 1f, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ABCC11" && clone.name == "Checkpoint 3") {
                        clone.transform.position = new Vector3(clone.transform.position.x - 1.5f, clone.transform.position.y - 1f, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "OCA2" && clone.name == "Checkpoint 3") {
                        clone.transform.position = new Vector3(clone.transform.position.x - 2f, clone.transform.position.y - 1f, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "OCA2" && clone.name == "Checkpoint 3") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 3.5f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "OCA2" && clone.name == "Checkpoint 3") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 32f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MCM6" && clone.name == "Checkpoint 3") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 7f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MCM6" && clone.name == "Checkpoint 1") {
                        clone.transform.position = new Vector3(clone.transform.position.x - 3f, clone.transform.position.y -1f, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SH2B3" && clone.name == "Checkpoint 1") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 11f, clone.transform.position.y -1f, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SMARCAD1" && clone.name == "Checkpoint 4") {
                        clone.transform.position = new Vector3(clone.transform.position.x - 2.5f, clone.transform.position.y -1f, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SMARCAD1" && clone.name == "Checkpoint 2") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 6.25f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SMARCAD1" && clone.name == "Checkpoint 1") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 4.25f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SERPINE1" && clone.name == "Checkpoint 1") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 4f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "AKT1" && clone.name == "Checkpoint 4") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 3f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "AKT1" && clone.name == "Checkpoint 2") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 13f, clone.transform.position.y  + 1f, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "OPN4" && clone.name == "Checkpoint 1") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 13f, clone.transform.position.y - 1f, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "PCDH15" && clone.name == "Checkpoint 1") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 70.8f, clone.transform.position.y , clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ABO" && clone.name == "Checkpoint 2") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 3.5f, clone.transform.position.y, clone.transform.position.z);
                    }
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ABO" && clone.name == "Checkpoint 1") {
                        clone.transform.position = new Vector3(clone.transform.position.x + 13.25f, clone.transform.position.y, clone.transform.position.z);
                    }
                    counter = 0;
                    counterWaypoint++;
                }
            }
        }
        else
        {
	        if (PlayerPrefs.HasKey("checkpoint") && !PlayerPrefs.GetString("checkpoint").Equals(""))
	        {
		        CheckPoint ch = GameObject.Find(PlayerPrefs.GetString("checkpoint")).GetComponent<CheckPoint>();
		        levelManager.SetCurrentCheckpoint(ch);

	        }
        }
    }
}
