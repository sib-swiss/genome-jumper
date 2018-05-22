using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class StarsAnimations : MonoBehaviour
{
	public Animator left;
	public Animator mid;
	public Animator right;

	private float leftLength;
	private bool particlesLeftOn = false;
	private float rightLength;
	private bool particlesRighttOn = false;
	private float midLength;
	private bool particlesMidOn = false;
	public AudioClip starL;
	public AudioClip starR;
	public AudioClip starM;
	public AudioSource zik;
	private EndLevelTrigger endLvl;
	
	// Use this for initialization
	void Start ()
	{
		zik.Stop();
		left = left.GetComponent<Animator>();
		mid = mid.GetComponent<Animator>();
		right = right.GetComponent<Animator>();
		leftLength = left.GetCurrentAnimatorStateInfo(0).length+0.5f;
		midLength = mid.GetCurrentAnimatorStateInfo(0).length+0.5f;
		rightLength = right.GetCurrentAnimatorStateInfo(0).length+0.5f;
		endLvl = GameObject.FindWithTag("endPoint").GetComponent<EndLevelTrigger>();
		StartCoroutine(Wait());
		//Debug.Log(left.GetCurrentAnimatorStateInfo(0).IsName("StarLeft"));
		
	}
	
	IEnumerator Wait()
	{
		yield return new WaitForSeconds(1);
		if (endLvl.star1)
		{
			SoundManager.Instance.PlaySound(starL,transform.position);
			left.enabled = true;
		}
	}

	void Update()
	{

		if (!particlesLeftOn && left.GetCurrentAnimatorStateInfo(0).normalizedTime >= leftLength)
		{
			particlesLeftOn = true;
			if (endLvl.star2)
			{
				SoundManager.Instance.PlaySound(starR, transform.position);
				right.enabled = true;
			}
			left.GetComponentInChildren<ParticleSystem>().Play();
		}
		
		if (!particlesRighttOn && right.GetCurrentAnimatorStateInfo(0).normalizedTime >= rightLength)
		{
			particlesRighttOn = true;
			if (endLvl.star3)
			{
				SoundManager.Instance.PlaySound(starM,transform.position);
				mid.enabled = true;
			}
			right.GetComponentInChildren<ParticleSystem>().Play();
		}
		
		
		if (!particlesMidOn && mid.GetCurrentAnimatorStateInfo(0).normalizedTime >= midLength)
		{
			particlesMidOn = true;
			mid.GetComponentInChildren<ParticleSystem>().Play();
		}
		
		
	}

}
