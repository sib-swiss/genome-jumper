using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine.Events;
using Object = UnityEngine.Object;

/*
	NOT USED ANYMORE
*/

public class SuperVariant : MonoBehaviour {

	/*
	private bool initDone = false;
	private bool hasBeenTriggered = false;
	private GameObject[] headItems;
	private GameObject[] rightHandItems;
	
	public enum SuperVariantType { ALDH2, AKT1, PCDH15, GJB2, ACTN3, EPOR }
	public SuperVariantType superVariant;
	
	public enum SNPVariantTypeItemHeadSlot { HeadMask, Nightcap, AmishHat, ClassicalCap, HearingAid}
	public enum SNPVariantTypeItemRightHandSlot { HandMask, Bread, CoffeCup, Milk, Brocoli, Beer, Chisel, InsulineSyringue, Perfume, FeedingBottle, Cannabis}

	public SNPVariantTypeItemHeadSlot SNPVariantItemHead;
	public SNPVariantTypeItemRightHandSlot SNPVariantItemRightHand;

	[Header("GameObject Profiles")]
	public GameObject ALDH2;
	public GameObject AKT1;
	public GameObject PCDH15;
	
	[Header("Audio Sources")]
	public GameObject Global;
	public AudioClip deaf;

	[Header("Attributes")]
	/// <summary>
	/// The speed modifier for the super muscles type
	/// </summary>
    public float speedModifier = 1f;
	public int points = 100;

	private GameObject pointsText;
	private GameObject score;
	public GameObject infoPanel;
	private LevelProperties lvlProperties;

	private float currentSize;
	
	private CorgiController _corgiController;
	// private Camera _camera;

	private bool resizeDone;

	private AudioSource audio;
	// Use this for initialization
	void Start ()
	{
		lvlProperties = GameObject.Find("LevelManager").GetComponent<LevelProperties>();
		pointsText = GameObject.Find("PointsText");
		resizeDone = false;
		currentSize = Camera.main.orthographicSize;
		audio = Global.GetComponent<AudioSource>();
		headItems = GameObject.FindGameObjectsWithTag("Head Item");
		rightHandItems = GameObject.FindGameObjectsWithTag("Right Hand Item");
	}

	// Update is called once per frame
	void Update () {
		if(!initDone){
			foreach(GameObject item in rightHandItems){
				item.SetActive(false);
			}
			foreach(GameObject item in headItems){
				item.SetActive(false);
			}
			initDone=true;
		}
		if (_corgiController == null) {
			_corgiController = Object.FindObjectOfType<CorgiController>();
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

		AudioSource asr = _corgiController.gameObject.GetComponent<AudioSource> ();

	    switch (superVariant)
	    {
			case SuperVariantType.ACTN3 :
			if (_corgiController)
				{
				_corgiController.DefaultParameters.SpeedFactor = 1.50f;
					points = points / 2;
				}
				break;
		    case SuperVariantType.EPOR :
			if (_corgiController)
			    {
				_corgiController.DefaultParameters.SpeedFactor = 1.50f;
				    points = points / 2;
			    }
			    //Enable Super Heart GFX
			    SuperVariantPlayerModifier svpm = Object.FindObjectOfType<SuperVariantPlayerModifier>();
			    if (svpm)
			    {
				    svpm.enableSuperHeart();
			    }
			    break;
			case SuperVariantType.PCDH15:
				PCDH15.SetActive (true);
				asr.clip = deaf;
				asr.Play();
				asr.loop = true;
				break;
			case SuperVariantType.ALDH2 :
				ALDH2.SetActive(true);
				break;
			case SuperVariantType.AKT1 :
				AKT1.SetActive(true);
				break;
			case SuperVariantType.GJB2 :
				asr.clip = deaf;
				asr.Play();
				asr.loop = true;
				break;
	    }
	    
	    if(SNPVariantItemHead != null){
		    //Disable all head items
		    //Debug.Log("SNPVariantItemHead "+headItems.Length);
				
				
		
		    foreach(GameObject item in headItems){
			    //Debug.Log("Comparing "+item.name+" "+ SNPVariantItemHead.ToString());
			    if(item.name == SNPVariantItemHead.ToString()){
				    item.SetActive(true);
				    lvlProperties.ItemHead = item.name;
				    Debug.Log("Activating "+item.name);
			    }else{
				    item.SetActive(false);
			    }
		    }
	    }

	    if(SNPVariantItemRightHand != null){
		    //Disable all right hand items
		    foreach(GameObject item in rightHandItems){
			    //Debug.Log("Comparing "+item.name+" "+ SNPVariantItemHead.ToString());
			    if(item.name == SNPVariantItemRightHand.ToString()){
				    item.SetActive(true);
				    lvlProperties.ItemRightHand = item.name;
				    Debug.Log("Activating "+item.name);
			    }else{
				    item.SetActive(false);
			    }
		    }
	    }
	    
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
	}*/

	/*
	NOT USED ANYMORE
*/
}
