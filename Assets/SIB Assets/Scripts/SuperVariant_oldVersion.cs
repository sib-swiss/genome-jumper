using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine.Events;
using UnityEngine.PostProcessing;
using VoxelBusters.Utility;
using Object = UnityEngine.Object;

public class SuperVariant_oldVersion : MonoBehaviour {


	public enum SuperVariantType { SuperMuscles, Beer, Colorblind, Cannabis, Deafness}
	public SuperVariantType superVariant;

    public Camera PostProcessCam;

	[Header("PostProcessing Profiles")]
	public PostProcessingProfile superMusclesProfile;
	public PostProcessingProfile beerProfile;
	public PostProcessingProfile colorblindProfile;
	public PostProcessingProfile cannabisProfile;
	public PostProcessingProfile deafnessProfile;

	[Header("Attributes")]
	/// <summary>
	/// The speed modifier for the super muscles type
	/// </summary>
    public float speedModifier = 1f;
	public int points = 100;

	private GameObject pointsText;
	private GameObject score;
	public GameObject infoPanel;

	private float currentSize;
	
	private CharacterHorizontalMovement _characterHorizontalMovement;
	// private Camera _camera;
	private bool hasBeenTriggered = false;

	private bool resizeDone;
	// Use this for initialization
	void Start () {
		pointsText = GameObject.Find("PointsText");
		resizeDone = false;
		currentSize = Camera.main.orthographicSize;
	}

	// Update is called once per frame
	void Update () {
		if (_characterHorizontalMovement == null) {
			_characterHorizontalMovement = Object.FindObjectOfType<CharacterHorizontalMovement>();
		}

		if (hasBeenTriggered && !infoPanel.active && !resizeDone)
		{
			Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, currentSize, 2f * Time.deltaTime);
			Camera.main.GetComponent<CameraController>().CameraOffset.x = 2;
			Camera.main.GetComponent<CameraController>().HorizontalLookDistance = 0;
			resizeDone = true;
		}
	}
    
	public virtual void OnTriggerEnter2D(Collider2D col){
		CorgiController controller=col.GetComponent<CorgiController>();
		if (controller == null || hasBeenTriggered)
			return;
		hasBeenTriggered = true;
		infoPanel.SetActive(true);
		StartCoroutine ("IncreaseScore");
		triggerSuperVariantEffect();
	}

    private void triggerSuperVariantEffect()
    {
        Debug.Log("Triggering Super Variante Effect !");

        if (superVariant == SuperVariantType.SuperMuscles)
        {
            if (_characterHorizontalMovement)
            {
                // Debug.Log ("Movement speed multiplier activated !");
                _characterHorizontalMovement.MovementSpeedMultiplier = speedModifier;
                points = points / 2;
            }

            //Enable Super Heart GFX
            SuperVariantPlayerModifier svpm = Object.FindObjectOfType<SuperVariantPlayerModifier>();
            if (svpm)
            {
                svpm.enableSuperHeart();
            }
        }
        PostProcessCam.GetComponent<PostProcessingBehaviour>().enabled = true;
        Debug.Log(PostProcessCam.GetComponent<PostProcessingBehaviour>().isActiveAndEnabled);
    }

	private IEnumerator IncreaseScore()
	{
		var x = 0;
		while (hasBeenTriggered)
		{
			//if (x < 1) Time.timeScale = 0.01F;
			//else Time.timeScale = 1;
			yield return new WaitForSeconds(1);
			x++;
			pointsText.GetComponent<ScoreDisplay>().increaseScore(points);
		}
	}
}
