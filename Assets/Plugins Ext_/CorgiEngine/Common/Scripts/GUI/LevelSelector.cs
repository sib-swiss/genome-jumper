using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;
using System.Collections.Generic;

namespace MoreMountains.CorgiEngine
{
	/// <summary>
	/// This component allows the definition of a level that can then be accessed and loaded. Used mostly in the level map scene.
	/// </summary>
	public class LevelSelector : MonoBehaviour
	{
		/// the exact name of the target level
	    public string LevelName;

        /// <summary>
        /// Loads the level specified in the inspector
        /// </summary>

        void Start()
        {
            var levelSplicing = GameObject.FindObjectOfType(typeof(LevelSplicing)) as LevelSplicing;
        }

        public virtual void GoToLevel()
	    {
			PlayerPrefs.SetString ("checkpoint", "");
            PlayerPrefs.SetString("SuperVariantEffect", "None");
            LevelManager.Instance.GotoLevel(LevelName);

	    }

		/// <summary>
		/// Restarts the current level
		/// </summary>
	    public virtual void RestartLevel(bool saveCheckpoint)
		{
			// we trigger an unPause event for the GameManager (and potentially other classes)
			MMEventManager.TriggerEvent (new CorgiEngineEvent (CorgiEngineEventTypes.UnPause));
			LoadingSceneManager.LoadScene(SceneManager.GetActiveScene().name);
            LevelSplicing.moveables = new List<GameObject>();
            LevelSplicing.lastExonFlag = new GameObject();
            LevelSplicing.lastIntronFlag = new GameObject();
            LevelSplicing.scriptCounter = 0;
			PlayerPrefs.SetString("restart", "true");
            PlayerPrefs.SetString("CurrentGeneSkinModification", PlayerPrefs.GetString("CurrentGeneSkinModification"));
            PlayerPrefs.SetString("CurrentGeneHeadItem", PlayerPrefs.GetString("CurrentGeneHeadItem"));
            PlayerPrefs.SetString("CurrentGeneHandItem", PlayerPrefs.GetString("CurrentGeneHandItem"));
            PlayerPrefs.SetString("IsFirstGeneOfAvatar", PlayerPrefs.GetString("IsFirstGeneOfAvatar"));
            

            if (PlayerPrefs.GetString("checkpoint") == "Spawn") {
                PlayerPrefs.SetString("PlayerTookSNP", "false");
                PlayerPrefs.SetString("PlayerTookStart", "false");
            }

            if (!saveCheckpoint)
                ResetCheckpointPlayerPrefs();

            PlayerPrefs.Save();
		}

		public virtual void LastCheckpoint(){
			var levelManager = GameObject.FindObjectOfType(typeof(LevelManager)) as LevelManager;
			var currentCheckPoint = levelManager.CurrentCheckPoint;

            if (currentCheckPoint) {
				Debug.Log("Last checkpoint saving : "+currentCheckPoint.name);
				PlayerPrefs.SetString ("checkpoint", currentCheckPoint.name);
                PlayerPrefs.SetFloat("checkpointPosX", GameObject.Find(currentCheckPoint.name).transform.position.x - 1);
                PlayerPrefs.SetFloat("checkpointPosY", GameObject.Find(currentCheckPoint.name).transform.position.y + 1);
                PlayerPrefs.SetFloat("checkpointPosZ", GameObject.Find(currentCheckPoint.name).transform.position.z);
                PlayerPrefs.SetInt("PreviousTime",GameObject.Find("UICamera").gameObject.transform.GetChild(0).transform.GetChild(7).transform.GetChild(1).transform.GetChild(0).GetComponent<Timer>().playTime);
                PlayerPrefs.SetInt("PreviousScore", (GameObject.Find("UICamera").gameObject.transform.GetChild(0).transform.GetChild(7).transform.GetChild(2).transform.GetChild(0).GetComponent<ScoreDisplay>().score)/2);
                if (GameObject.Find("SnpBubble").GetComponent<SNPVariant>().hasBeenTriggered == true) {
                    PlayerPrefs.SetString("PlayerTookSNP", "true");
                }
                else {
                    PlayerPrefs.SetString("PlayerTookSNP", "false");
                }
                if(GameObject.Find("StartBubble").GetComponent<StartStopCodonBehaviour>().hasBeenTriggered == true) {
                    PlayerPrefs.SetString("PlayerTookStart", "true");
                }
                else {
                    PlayerPrefs.SetString("PlayerTookStart", "false");
                }
                PlayerPrefs.Save();
            } else {
                ResetCheckpointPlayerPrefs();

            }
			RestartLevel (true);
		}
		
        private void ResetCheckpointPlayerPrefs()
        {
            PlayerPrefs.SetString("checkpoint", "");
            PlayerPrefs.SetInt("PreviousTime", 0);
            PlayerPrefs.SetInt("PreviousScore", 0);
            PlayerPrefs.SetString("PlayerTookSNP", "false");
            PlayerPrefs.SetString("PlayerTookStart", "false");
            PlayerPrefs.DeleteKey("checkpointPosX");
            PlayerPrefs.DeleteKey("checkpointPosY");
            PlayerPrefs.DeleteKey("checkpointPosZ");
            PlayerPrefs.Save();
        }

	}
}