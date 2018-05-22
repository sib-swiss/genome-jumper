using System.Collections;
using System.Collections.Generic;
using ExifLibrary;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class CheckpointsManager : MonoBehaviour {

 	public GameObject checkpoint;
	private int intronInterval = 5; //instanciate a checkpoint each XX exons
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
        /*if(inc>=0){
            if(levelManager){
				inc++;
				if(!PlayerPrefs.GetString("checkpoint").Equals("") && !PlayerPrefs.GetString("checkpoint").Equals("Spawn") && inc>incNeeded){
					//Debug.Log("Searching last waypoint");
					GameObject lastCheckpoint = GameObject.Find(PlayerPrefs.GetString("checkpoint"));

					if(!lastCheckpoint){
						PlayerPrefs.SetString("checkpoint","");
						inc = -1;
						return;
					}

					Character player = levelManager.Players[0];
                    GameObject spawnPoint = GameObject.Find("Spawn");
                    spawnPoint.transform.position = new Vector3(lastCheckpoint.transform.position.x - 1, lastCheckpoint.transform.position.y, lastCheckpoint.transform.position.z);
                    //player.transform.position = new Vector3(lastCheckpoint.transform.position.x -1,lastCheckpoint.transform.position.y,lastCheckpoint.transform.position.z);
                    inc = -1;
				}
			}else{
            
            //Debug.Log("Last checkpoint : "+PlayerPrefs.GetString("checkpoint"));
                levelManager = GameObject.FindObjectOfType(typeof(LevelManager)) as LevelManager;
				GameObject[] introns = GameObject.FindGameObjectsWithTag("Intron Flag");
				Debug.Log(introns.Length);
				int counter = 0;
				int counterWaypoint = 1;
				for(int i=0; i<introns.Length;i++){
					counter ++;
					if(counter >= intronInterval){
						var clone = Instantiate(checkpoint,introns[i].transform.position,introns[i].transform.rotation);
						clone.name = "Checkpoint "+counterWaypoint;
						counter = 0;
						counterWaypoint++;
					}
				}
			}
		}
        */
    }
}
