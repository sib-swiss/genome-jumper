 using System.Collections;
using System.Collections.Generic;
 using MoreMountains.CorgiEngine;
using Spine.Unity;
using UnityEngine;
 using UnityEngine.Scripting.APIUpdating;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;
using UnityEngine.UI;

public class SNPVariant : MonoBehaviour {

  //Can only mute one thing. If multiple muutting please assign multiple SNPVariant scripts
	public enum SNPVariantTypeItemHeadSlot { HeadMask, Nightcap, AmishHat, ClassicalCap, HearingAid}
	public enum SNPVariantTypeItemRightHandSlot { HandMask, Bread, CoffeCup, Milk, Brocoli, Beer, Chisel, InsulineSyringue, Perfume, FeedingBottle, Cannabis}
  	public SNPVariantTypeItemHeadSlot SNPVariantItemHead;
	public SNPVariantTypeItemRightHandSlot SNPVariantItemRightHand;

    public string HeadItemPersistency;
    public string HandItemPersistency;
    private string SkinVariantPersistency;
    private string SuperVariant;
    private string SuperVariantPersistency;
    public SkeletonAnimator PlayerCurrentSkin;
    private string isFirstGeneOfAvatar;
    private int currentAvatar;

    private string sceneName;

    public bool hasBeenTriggered = false;
  	private bool initDone = false;
    private bool movementDone = false;

    public GameObject Player;
    private GameObject[] headItems;
	private GameObject[] rightHandItems;
	private GameObject pointsText;
	public GameObject particles;
    public GameObject snpBubble;
    public GameObject SnpPlateforme;
	public GameObject SnpReplace;

    public bool SnpTriggered = false;

	private Vector3 snpPlateformOrigin;
	private SpriteRenderer SNP;
	private SpriteRenderer thisSpriteRenderer;
	public AudioClip bubbleExplosionSound;
	public AudioClip AuraEffect;

	public GameObject infoPanel;
	//private float currentSize;
	
	private Vector3 scaleSNP;
	private bool goMoveSNP;
	
	[Header("Score on pickup")]
	public int points = 1000;
	
	private float move;
	private bool resizeDone;
	private LevelProperties lvlProperties;

	public GameObject PowerUp;
    private AudioSource audio;

	// Use this for initialization
	void Start ()
	{
		lvlProperties = GameObject.Find("LevelManager").GetComponent<LevelProperties>();
        sceneName = SceneManager.GetActiveScene().name;
        PowerUp = GameObject.FindGameObjectWithTag("PowerUp");
		move = 1.1f * Time.deltaTime;
		resizeDone = false;
        //currentSize = Camera.main.orthographicSize;
        goMoveSNP = false;
		headItems = GameObject.FindGameObjectsWithTag("Head Item");
		rightHandItems = GameObject.FindGameObjectsWithTag("Right Hand Item");
		pointsText = GameObject.Find("PointsText");
		thisSpriteRenderer = GetComponent<SpriteRenderer>();
		snpPlateformOrigin = SnpPlateforme.transform.position;
        snpBubble = GameObject.Find("SnpBubble");
        HeadItemPersistency = PlayerPrefs.GetString("CurrentGeneHeadItem");
        HandItemPersistency = PlayerPrefs.GetString("CurrentGeneHandItem");
        SkinVariantPersistency = PlayerPrefs.GetString("CurrentGeneSkinModification");
        SuperVariant = PlayerPrefs.GetString("SuperVariantEffect");
        isFirstGeneOfAvatar = PlayerPrefs.GetString("IsFirstGeneOfAvatar");
        currentAvatar = PlayerPrefs.GetInt("CombinationPlayAvatar");
        audio = GameObject.Find("Global").GetComponent<AudioSource>();
        infoPanel = GameObject.Find("HUD").transform.GetChild(5).gameObject;

        if(PlayerPrefs.GetString("PlayerTookSNP") == "true") {
            if (sceneName == "AKT1" || sceneName == "CYP1A2" || sceneName == "HERC2" || sceneName == "FOXO3" || sceneName == "MCM6" || sceneName == "TCF7L2") {
                SnpReplace.SetActive(false);
            }

            if(sceneName == "TCF7L2") {
                if(HandItemPersistency == "InsulineSyringue") { GameObject.Find("Right Hand Item Container").transform.GetChild(1).gameObject.SetActive(true); }
            }
            if (sceneName == "AKT1" || sceneName == "ALDH2" || sceneName == "PCDH15") {
                GameObject SuperVariantEffects = GameObject.Find("SuperVariantEffects").gameObject;
                SuperVariantEffects.SetActive(true);
                if (sceneName == "ALDH2") { SuperVariantEffects.transform.GetChild(0).gameObject.SetActive(true);  }
                //if (sceneName == "AKT1") { SuperVariantEffects.transform.GetChild(1).gameObject.SetActive(true); }
                //if (sceneName == "PCDH15") { SuperVariantEffects.transform.GetChild(1).gameObject.SetActive(true);  }
            }
                transform.position = new Vector3(0, 0, 0);
        }

        StartCoroutine(CheckSNPTrigger());

    }

    private IEnumerator CheckSNPTrigger()
    {
        yield return new WaitForSeconds(1f);
        if (PlayerPrefs.GetInt("HasTriggeredSnp") == 1)
        {
            hasBeenTriggered = true;
            SnpTriggered = true;
            TriggerSNP(true);
        }
        else
        {
            hasBeenTriggered = false;
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        if(Player == null)
        {
            Player = GameObject.Find("SIB Default Character");
        }
        if(PlayerCurrentSkin == null)
        {
            GameObject SibSpineCharacter = GameObject.Find("SIB Spine Character");
            if(SibSpineCharacter != null)
            {
                PlayerCurrentSkin = GameObject.Find("SIB Spine Character").GetComponent<SkeletonAnimator>();
            }
        }
		if(!initDone){
			foreach(GameObject item in rightHandItems){
					item.SetActive(false);
			}
			foreach(GameObject item in headItems){
					item.SetActive(false);
			}
			initDone=true;
		}
		
		if (goMoveSNP)
		{
            if(SnpReplace.transform.position != snpPlateformOrigin)
            {
                if (SnpReplace.GetComponent<SpriteRenderer>().sprite.name == "Multi-fullblock") {
                    SnpReplace.GetComponent<SpriteRenderer>().size = new Vector2(1.09f, 0.83f);
                    SnpReplace.transform.position = Vector3.MoveTowards(SnpReplace.transform.position, (snpPlateformOrigin - new Vector3(0, -0.102f, 0)), move);
                }
                else {
                    SnpReplace.transform.position = Vector3.MoveTowards(SnpReplace.transform.position, snpPlateformOrigin, move);
                    SnpPlateforme.transform.localPosition = Vector3.MoveTowards(SnpPlateforme.transform.localPosition, new Vector3(SnpPlateforme.transform.localPosition.x, -0.7f), .05f);
                }
                SnpReplace.transform.position = Vector3.MoveTowards(SnpReplace.transform.position, snpPlateformOrigin, move);
                StartCoroutine(ScaleObject(1f, SnpPlateforme.transform, new Vector3(0f, 0f, 0f)));
            }
            else
            {
                goMoveSNP = false;
                movementDone = true;
            }
            //StartCoroutine(ScaleObject(0.5f, SnpReplace.transform, new Vector3(1f,0.25f,1f)));
        }
        if(movementDone) {
            if(sceneName == "AKT1" || sceneName == "CYP1A2" || sceneName == "HERC2" || sceneName == "FOXO3" || sceneName == "MCM6" || sceneName == "TCF7L2") {
                SnpReplace.transform.localPosition = Vector3.MoveTowards(SnpReplace.transform.localPosition, new Vector3(SnpReplace.transform.localPosition.x, -1f), .05f);
                StartCoroutine(ScaleObject(1f, SnpReplace.transform, new Vector3(0f, 0f, 0f)));
            }
        }
		else
		{
			transform.localScale = new Vector3(0.95f + Mathf.PingPong(0.08f * Time.time, 0.05f), 0.2f + Mathf.PingPong(0.08f * Time.time, 0.05f));
		}
		
		/* OLD SYSTEM (SLOW + ZOOM)
		if (hasBeenTriggered && !infoPanel.active && !resizeDone)
		{
			Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, currentSize, 2f * Time.deltaTime);
			Camera.main.GetComponent<CameraController>().CameraOffset.x = 2;
			Camera.main.GetComponent<CameraController>().HorizontalLookDistance = 0;
			resizeDone = true;
		}
		*/
	}

	public virtual void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player")
        {
            if (!hasBeenTriggered)
            {
                TriggerSNP(true);
            }
        }
	}

    public void TriggerSNP(bool playSound)
    {
        Debug.Log("Enter SNP Variant");

            if(playSound == true)
            {
                MMSoundManager.Instance.PlaySound(bubbleExplosionSound, MMSoundManagerPlayOptions.Default);
            }

            //PowerUp.GetComponent<Renderer>().enabled = true;
            if (PowerUp != null)
            {
                var renderers = PowerUp.GetComponentsInChildren<ParticleSystem>();
                foreach (var rend in renderers)
                {
                    rend.Play();
                }
            }

            MMSoundManager.Instance.PlaySound(AuraEffect, MMSoundManagerPlayOptions.Default);

            //Test if tutorial
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                GameObject.Find("Head Item Container").transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("Right Hand Item Container").transform.GetChild(9).gameObject.SetActive(true);
                Player.transform.GetChild(2).GetComponent<SkeletonAnimator>().Skeleton.SetSkin("amish");
                Player.transform.GetChild(1).transform.GetChild(25).gameObject.SetActive(false);
                Player.transform.GetChild(1).transform.GetChild(25).transform.gameObject.SetActive(false);

                Player.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                Player.transform.GetChild(1).transform.GetChild(0).transform.gameObject.SetActive(true);
                Player.transform.GetChild(1).transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);
                SnpTriggered = true;
            }
            else
            {
                if (isFirstGeneOfAvatar.ToString() == "true")
                {
                    if (currentAvatar == 6)
                    {
                        SnpTriggered = true;
                        Player.transform.GetChild(1).transform.GetChild(25).gameObject.SetActive(false);
                        Player.transform.GetChild(1).transform.GetChild(25).transform.gameObject.SetActive(false);

                        //Activate baby head 1
                        Player.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                        Player.transform.GetChild(0).transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    if (currentAvatar == 24)
                    {
                        SnpTriggered = true;
                        Player.transform.GetChild(1).transform.GetChild(25).gameObject.SetActive(false);
                        Player.transform.GetChild(1).transform.GetChild(25).transform.gameObject.SetActive(false);

                        //Activate baby head 2
                        Player.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                        Player.transform.GetChild(0).transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else
                    {
                        SnpTriggered = true;
                        Player.transform.GetChild(1).transform.GetChild(25).gameObject.SetActive(false);
                        Player.transform.GetChild(1).transform.GetChild(25).transform.gameObject.SetActive(false);
                        Player.transform.GetChild(1).transform.GetChild(currentAvatar - 1).gameObject.SetActive(true);
                        Player.transform.GetChild(1).transform.GetChild(currentAvatar - 1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
                else
                {
                    SnpTriggered = true;
                }

                Debug.Log("Setting Head Item Container to " + HeadItemPersistency);
                if (HeadItemPersistency != null)
                {
                    for (int i = 0; i < GameObject.Find("Head Item Container").transform.childCount; i++)
                    {
                        if (HeadItemPersistency == "HeadMask") { GameObject.Find("Head Item Container").transform.GetChild(0).gameObject.SetActive(true); }
                        if (HeadItemPersistency == "Nightcap") { GameObject.Find("Head Item Container").transform.GetChild(1).gameObject.SetActive(true); }
                        if (HeadItemPersistency == "AmishHat") { GameObject.Find("Head Item Container").transform.GetChild(2).gameObject.SetActive(true); }
                        if (HeadItemPersistency == "ClassicalCap") { GameObject.Find("Head Item Container").transform.GetChild(3).gameObject.SetActive(true); }
                        if (HeadItemPersistency == "HearingAid") { GameObject.Find("Head Item Container").transform.GetChild(4).gameObject.SetActive(true); }
                        if (HeadItemPersistency == "StrangeBrain") { GameObject.Find("Head Item Container").transform.GetChild(5).gameObject.SetActive(true); }
                        if (HeadItemPersistency == "BHeart") { GameObject.Find("Head Item Container").transform.GetChild(6).gameObject.SetActive(true); }
                        if (HeadItemPersistency == "MoonCell") { GameObject.Find("Head Item Container").transform.GetChild(7).gameObject.SetActive(true); }
                        if (HeadItemPersistency == "glasses") { GameObject.Find("Head Item Container").transform.GetChild(8).gameObject.SetActive(true); }
                        if (HeadItemPersistency == "Ribbon") { GameObject.Find("Head Item Container").transform.GetChild(9).gameObject.SetActive(true); }
                    }
                }

                if (HandItemPersistency != null)
                {
                    for (int i = 0; i < GameObject.Find("Right Hand Item Container").transform.childCount; i++)
                    {
                        if (HandItemPersistency == "HandMask") { GameObject.Find("Right Hand Item Container").transform.GetChild(0).gameObject.SetActive(true); }
                        if (HandItemPersistency == "InsulineSyringue") { GameObject.Find("Right Hand Item Container").transform.GetChild(1).gameObject.SetActive(true); }
                        if (HandItemPersistency == "Bread") { GameObject.Find("Right Hand Item Container").transform.GetChild(2).gameObject.SetActive(true); }
                        if (HandItemPersistency == "Perfume") { GameObject.Find("Right Hand Item Container").transform.GetChild(3).gameObject.SetActive(true); }
                        if (HandItemPersistency == "FeedingBottle") { GameObject.Find("Right Hand Item Container").transform.GetChild(4).gameObject.SetActive(true); }
                        if (HandItemPersistency == "Cannabis") { GameObject.Find("Right Hand Item Container").transform.GetChild(5).gameObject.SetActive(true); }
                        if (HandItemPersistency == "CoffeCup") { GameObject.Find("Right Hand Item Container").transform.GetChild(6).gameObject.SetActive(true); }
                        if (HandItemPersistency == "Milk") { GameObject.Find("Right Hand Item Container").transform.GetChild(7).gameObject.SetActive(true); }
                        if (HandItemPersistency == "Cane") { GameObject.Find("Right Hand Item Container").transform.GetChild(8).gameObject.SetActive(true); }
                        if (HandItemPersistency == "Brocoli") { GameObject.Find("Right Hand Item Container").transform.GetChild(9).gameObject.SetActive(true); }
                        if (HandItemPersistency == "Beer") { GameObject.Find("Right Hand Item Container").transform.GetChild(10).gameObject.SetActive(true); }
                        if (HandItemPersistency == "Chisel") { GameObject.Find("Right Hand Item Container").transform.GetChild(11).gameObject.SetActive(true); }
                        if (HandItemPersistency == "Ribbon") { GameObject.Find("Right Hand Item Container").transform.GetChild(12).gameObject.SetActive(true); }
                        if (HandItemPersistency == "WhiteGlove") { GameObject.Find("Right Hand Item Container").transform.GetChild(13).gameObject.SetActive(true); }
                    }
                }

                if (SkinVariantPersistency != null && (PlayerCurrentSkin.initialSkinName != SkinVariantPersistency.ToString()))
                {
                    bool wasBaby = PlayerCurrentSkin.initialSkinName == "baby";
                    if (SkinVariantPersistency == "amish") { Player.transform.GetChild(2).GetComponent<SkeletonAnimator>().Skeleton.SetSkin("amish"); }
                    if (SkinVariantPersistency == "athletic female") { Player.transform.GetChild(2).GetComponent<SkeletonAnimator>().Skeleton.SetSkin("athletic female"); }
                    if (SkinVariantPersistency == "athletic female black") { Player.transform.GetChild(2).GetComponent<SkeletonAnimator>().Skeleton.SetSkin("athletic female black"); }
                    if (SkinVariantPersistency == "athletic male") { Player.transform.GetChild(2).GetComponent<SkeletonAnimator>().Skeleton.SetSkin("athletic male"); }
                    if (SkinVariantPersistency == "baby") { Player.transform.GetChild(2).GetComponent<SkeletonAnimator>().initialSkinName = "baby"; Player.transform.GetChild(2).GetComponent<SkeletonAnimator>().Skeleton.SetSkin("baby"); Player.transform.GetChild(2).GetComponent<SkeletonAnimator>().Skeleton.SetSlotsToSetupPose(); }
                    if (SkinVariantPersistency == "city dweller female") { Player.transform.GetChild(2).GetComponent<SkeletonAnimator>().Skeleton.SetSkin("city dweller female"); }
                    if (SkinVariantPersistency == "city dweller female black") { Player.transform.GetChild(2).GetComponent<SkeletonAnimator>().Skeleton.SetSkin("city dweller female black"); }
                    if (SkinVariantPersistency == "city dweller female latino") { Player.transform.GetChild(2).GetComponent<SkeletonAnimator>().Skeleton.SetSkin("city dweller female latino"); }
                    if (SkinVariantPersistency == "city dweller male") { Player.transform.GetChild(2).GetComponent<SkeletonAnimator>().Skeleton.SetSkin("city dweller male"); }
                    if (SkinVariantPersistency == "city dweller male black") { Player.transform.GetChild(2).GetComponent<SkeletonAnimator>().Skeleton.SetSkin("city dweller male black"); }
                    if (SkinVariantPersistency == "city dweller male latino") { Player.transform.GetChild(2).GetComponent<SkeletonAnimator>().Skeleton.SetSkin("city dweller male latino"); }
                    if (SkinVariantPersistency == "unknown") { Player.transform.GetChild(2).GetComponent<SkeletonAnimator>().Skeleton.SetSkin("unknown"); }
                    if (wasBaby)
                    {
                        Player.transform.GetChild(2).GetComponent<SkeletonAnimator>().initialSkinName = SkinVariantPersistency;
                        Player.transform.GetChild(2).GetComponent<SkeletonAnimator>().Skeleton.SetSlotsToSetupPose();
                    }

                }
            }

            AudioSource asrc = Player.GetComponent<AudioSource>();

            if (SuperVariant != null && SuperVariant != "None")
            {
                if (SuperVariant == "Speed")
                {
                    points = points / 2;
                    Player.GetComponent<CorgiController>().DefaultParameters.SpeedFactor = 1.50f;
                }
                if (SuperVariant == "SuperMuscle")
                {
                    Player.GetComponent<CorgiController>().DefaultParameters.SpeedFactor = 1.50f;
                    asrc.clip = Resources.Load("Sounds/supermuscles") as AudioClip;
                    if (!Player.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject.activeSelf)
                    {
                        asrc.Play();
                    }

                    points = points / 2;
                    Player.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                if (SuperVariant == "Deef")
                {
                    asrc.clip = Resources.Load("Sounds/deafness") as AudioClip;
                    asrc.Play();
                    asrc.loop = true;
                }
                if (SuperVariant == "Blind")
                {

                }
                if (SuperVariant == "Usher")
                {
                    GameObject.Find("SuperVariantEffects").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    asrc.clip = Resources.Load("Sounds/deafness") as AudioClip;
                    asrc.Play();
                    asrc.loop = true;
                }
                if (SuperVariant == "Beer")
                {
                    GameObject.Find("SuperVariantEffects").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    ParticleSystem ps = GameObject.Find("bubbleBeer").GetComponent<ParticleSystem>();
                    ParticleSystem.MainModule psmm = ps.main;
                    psmm.maxParticles = 500;
                    ParticleSystem.EmissionModule psem = ps.emission;
                    psem.rateOverTime = 100;

                }
                if (SuperVariant == "Cannabis")
                {
                    GameObject.Find("SuperVariantEffects").gameObject.transform.GetChild(2).gameObject.SetActive(true);
                }

                //PlayerPrefs.SetString("SuperVariantEffect","None");
                PlayerPrefs.Save();

            }

            thisSpriteRenderer.enabled = false;
            SnpReplace.GetComponent<SpriteRenderer>().enabled = true;
            goMoveSNP = true;

            infoPanel.SetActive(true);
            Instantiate(particles, transform.position, transform.rotation);
            hasBeenTriggered = true;

            PlayerPrefs.SetInt("HasTriggeredSnp", 1);

            pointsText.GetComponent<ScoreDisplay>().increaseScore(points);

            ScaleFading scaleFading = GetComponent<ScaleFading>();
            if (scaleFading != null)
            {
                scaleFading.enabled = true;
            }

        GameObject.Find("SnpIcon").GetComponent<Image>().color = new Color(255, 255, 255, 255);

        //if (currentAvatar == 21)
        //{
        //    GameObject.Find("Head Item Container").transform.GetChild(1).gameObject.transform.position = new Vector3(GameObject.Find("Head Item Container").transform.GetChild(1).gameObject.transform.position.x - 0.02f, GameObject.Find("Head Item Container").transform.GetChild(1).gameObject.transform.position.y -0.05f, GameObject.Find("Head Item Container").transform.GetChild(1).gameObject.transform.position.z);
        //}
    }

	IEnumerator ScaleObject(float time, Transform transf, Vector3 targetV)
	{
		Vector3 actualScale = transf.localScale;             // scale of the object at the begining of the animation
		Vector3 targetScale = new Vector3(1f, 1f, 1f);     // scale of the object at the end of the animation

		for (float t = 0; t < 1; t += Time.deltaTime / time)
		{
			transf.localScale = Vector3.Lerp(actualScale, targetV, t);
			yield return null;
		}
	}
}
