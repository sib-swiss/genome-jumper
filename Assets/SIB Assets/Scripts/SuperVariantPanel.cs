using System.Collections;
using System.Collections.Generic;
using Lean.Localization;
using MoreMountains.CorgiEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SuperVariantPanel : MonoBehaviour
{

	private Text text;
    private GameObject player;
	private LeanLocalizedTextArgs lean;
	public bool go1;
	public bool go;
	public float speed;
	
	public ParticleSystem speed2Particles;

	public int ShowForSeconds = 3;
	
	
	void Start()
	{
		go1 = true;
		text = GetComponentInChildren<Text>();
		lean = GetComponentInChildren<LeanLocalizedTextArgs>();
		string geneName = SceneManager.GetActiveScene().name.ToLower();
        if(geneName == "foxo3alternate") { geneName = "foxo3"; }
		lean.PhraseName = "gene-" + geneName + "-short-text";
        player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
        if(!player.activeSelf) {
            transform.GetChild(1).gameObject.SetActive(false);
        }
		if (Vector3.Distance(text.transform.localPosition, new Vector3(0, 0, 0)) > 10f && go1)
		{
			text.transform.localPosition = Vector3.MoveTowards(text.transform.localPosition, new Vector3(0, 0, 0), speed * Time.deltaTime);
			StartCoroutine(hideText());
			StartCoroutine(goParticles2());
		}
		if (go)
		{
			text.transform.localPosition = Vector3.MoveTowards(text.transform.localPosition, new Vector3(3000, 0, 0), speed * Time.deltaTime);
			if (text.transform.localPosition.x >= 2500)
			{
				go = false;
			}
		}
	}

	IEnumerator hideText()
	{
		yield return new WaitForSeconds(ShowForSeconds);
		go = true;
		go1 = false;
		StopCoroutine(hideText());
	}

	IEnumerator goParticles2()
	{
		yield return new WaitForSeconds(2.3f);
		speed2Particles.Play();
		StopCoroutine(goParticles2());
		
	}

	/* OLD SYSTEM (ZOOM + SLOWMOTION)
	private bool timeIsScale = false;
	private GameObject loadingRadial;
	private float currentSize;
	//private Zoom playerZoom;
	
	// Use this for initialization
	void Start ()
	{
		loadingRadial = GameObject.Find("ProgressRadial");
		Time.timeScale = 0.01F;
		Time.fixedDeltaTime = Time.timeScale * .02F;
		timeIsScale = true;
		currentSize = Camera.main.orthographicSize;
		//playerZoom = GameObject.FindGameObjectWithTag("Player").GetComponent<Zoom>();
		//playerZoom.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!loadingRadial.active)
		{
			//playerZoom.enabled = true;
			ResetTime();
		}
		else
		{
			Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, 1.3f, 18f * Time.deltaTime);
			//Camera.main.GetComponent<CameraController>().CameraOffset.x = Mathf.MoveTowards(Camera.main.GetComponent<CameraController>().CameraOffset.x, 0, 18f * Time.deltaTime);
			Camera.main.GetComponent<CameraController>().CameraOffset.x = 0;
			Camera.main.GetComponent<CameraController>().HorizontalLookDistance = 0;
		}
	}

	public void ResetTime()
	{
		Time.timeScale = 1;
		timeIsScale = false;
		Time.fixedDeltaTime = Time.timeScale * .02F;
		this.transform.gameObject.SetActive(false);
	}*/
}
