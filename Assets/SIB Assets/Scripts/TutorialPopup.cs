using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopup : MonoBehaviour {

        private bool timeIsScale = false;
        private GameObject loadingRadial;
        private float currentSize;
        private Transform popup;
		public bool shouldTriggerLevelSelectionWhenExit = false;
        //private Zoom playerZoom;

        // Use this for initialization
        void Start ()
        {
            popup = transform.GetChild(0);
            currentSize = Camera.main.orthographicSize;
            //playerZoom = GameObject.FindGameObjectWithTag("Player").GetComponent<Zoom>();
            //playerZoom.enabled = false;
        }

        // Update is called once per frame
        void OnTriggerEnter2D (Collider2D col) {
        if(col.gameObject.tag == "Player")
            //Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, 1.3f, 18f * Time.deltaTime);
            //Camera.main.GetComponent<CameraController>().CameraOffset.x = Mathf.MoveTowards(Camera.main.GetComponent<CameraController>().CameraOffset.x, 0, 18f * Time.deltaTime);
            //Camera.main.GetComponent<CameraController>().CameraOffset.x = 0;
            //Camera.main.GetComponent<CameraController>().HorizontalLookDistance = 0;
            //Time.timeScale = 0.05F;
            //Time.fixedDeltaTime = Time.timeScale * .02F;
           // timeIsScale = true;
            popup.gameObject.SetActive(true);
        }

    void OnTriggerExit2D(Collider2D collision)
    {
        ResetTime();

		//Camera.main.GetComponent<CameraController>().CameraOffset.x = 2;
		//Camera.main.GetComponent<CameraController>().HorizontalLookDistance = 1;

		if (shouldTriggerLevelSelectionWhenExit) {
			PlayerPrefs.SetInt("TutorialCompleted", 1);
			PlayerPrefs.Save();
			//LoadingSceneManager.LoadScene ("LevelSelection");
		}
    }
    
    public void ResetTime()
        {
            //Time.timeScale = 1;
            //timeIsScale = false;
            //Time.fixedDeltaTime = Time.timeScale * .02F;
            popup.gameObject.SetActive(false);
        }
}
