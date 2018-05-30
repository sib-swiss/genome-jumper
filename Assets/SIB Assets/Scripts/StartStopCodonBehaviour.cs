using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class StartStopCodonBehaviour : MonoBehaviour {

    private GameObject pointsText;
    public GameObject StartOrStopPlatform;
    public GameObject StartOrStopReplace;
    public GameObject particles;
    public AudioClip bubbleExplosionSound;
    private SpriteRenderer thisSpriteRenderer;
    public bool hasBeenTriggered = false;

    private float currentSize;

    private Vector3 scaleStartCodon;
    private bool goMoveStartCodon;

    [Header("Score on pickup")]
    public int points = 1000;

    private float move;

	// Use this for initialization
	void Start () {
        move = 1.2f * Time.deltaTime;
        pointsText = GameObject.Find("PointsText");
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
		if(PlayerPrefs.GetString("PlayerTookStart").Equals("true") && transform.name.Equals("StartBubble")) {
            transform.position = new Vector3(0, 0, 0);
            Debug.Log(transform.position + " v " + GameObject.FindGameObjectWithTag("Player").transform.position);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if(goMoveStartCodon)
        {
            StartOrStopReplace.transform.position = Vector3.MoveTowards(StartOrStopReplace.transform.position, StartOrStopPlatform.transform.position, move);
            if (StartOrStopReplace.transform.position == StartOrStopPlatform.transform.position)
                StartOrStopPlatform.GetComponent<SpriteRenderer>().enabled = false;
            //StartPlatform.transform.localPosition = Vector3.MoveTowards(StartPlatform.transform.localPosition, new Vector3(StartPlatform.transform.localPosition.x, -0.7f), .05f);
            //StartCoroutine(ScaleObject(0.5f, transform, new Vector3(1f, 0.25f, 1f)));
        }
        else
        {
            //transform.localScale = new Vector3(0.32f + Mathf.PingPong(0.08f * Time.time, 0.05f), 0.15f + Mathf.PingPong(0.08f * Time.time, 0.05f));
        }
	}

    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (!hasBeenTriggered)
        {
            
            SoundManager.Instance.PlaySound(bubbleExplosionSound,transform.position);
            thisSpriteRenderer.enabled = false;
            StartOrStopReplace.GetComponent<SpriteRenderer>().enabled = true;
            goMoveStartCodon = true;
            Instantiate(particles, transform.position, transform.rotation);

            pointsText.GetComponent<ScoreDisplay>().increaseScore(points);

            ScaleFading scaleFading = GetComponent<ScaleFading>();
            if (scaleFading != null)
            {
                scaleFading.enabled = true;
            }

            hasBeenTriggered = true;
        }
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
