using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class LoadingScreen : MonoBehaviour {

	private GameObject first;
	private GameObject first2;
	private GameObject second;
	private GameObject second2;
	
	private Vector3 scaleZero;
	private Vector3 scaleNormal;

	private bool snpEffect;
	private bool snpEffect2;
	
	public GameObject[] snps;
	public GameObject[] snps2;

	private int snpActive = 0;
	private int snpActive2 = 0;

	private bool go;
	private bool go2;
	
	// Use this for initialization
	void Start () {
		scaleZero = new Vector3(0,0,1);
		scaleNormal = new Vector3(6,6,1);
		first = snps[0];
		second = snps[1];
		first2 = snps2[0];
		second2 = snps2[1];
		snpEffect = true;
		snpEffect2 = true;
		go = false;
		go2 = false;
		InvokeRepeating ("Gogo", 2.0f, 4.0f);
		InvokeRepeating ("Gogo2", 2.0f, 4.0f);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (snpEffect)
			first.transform.localScale = new Vector3(19f + Mathf.PingPong(19f * Time.time, 15f), 19f + Mathf.PingPong(19f * Time.time, 15f));
		
		if (snpEffect2)
			first2.transform.localScale = new Vector3(19f + Mathf.PingPong(19f * Time.time, 15f), 19f + Mathf.PingPong(19f * Time.time, 15f));

		if (go)
		{
			explode();
		}
		
		if (go2)
		{
			explode2();
		}
	}

	void Gogo()
	{
		go = true;
	}

	void Gogo2()
	{
		go2 = true;
	}
	
	void explode(){
		snpEffect = false;
		first.transform.localScale = Vector3.Lerp (first.transform.localScale, scaleZero, 4f * Time.deltaTime);
		if (first.transform.localScale.x <= 0.1f)
			second.transform.localScale = Vector3.Lerp (second.transform.localScale, scaleNormal, 4f * Time.deltaTime);
		if (second.transform.localScale.x >= 4.0f)
		{
			first = second;

			if (snpActive == 3)
			{
				snpActive = 0;
				second = snps[1];
			}
			else
			{
				snpActive++;
				if (snpActive == 3)
					second = snps[0];
				else second = snps[snpActive + 1];
			}
			
			snpEffect = true;
			go = false;
		}
	}
	
	void explode2(){
		snpEffect2 = false;
		first2.transform.localScale = Vector3.Lerp (first2.transform.localScale, scaleZero, 4f * Time.deltaTime);
		if (first2.transform.localScale.x <= 0.1f)
			second2.transform.localScale = Vector3.Lerp (second2.transform.localScale, scaleNormal, 4f * Time.deltaTime);
		if (second2.transform.localScale.x >= 4.0f)
		{
			first2 = second2;

			if (snpActive2 == 3)
			{
				snpActive2 = 0;
				second2 = snps2[1];
			}
			else
			{
				snpActive2++;
				if (snpActive2 == 3)
					second2 = snps2[0];
				else second2 = snps2[snpActive2 + 1];
			}
		
			snpEffect2 = true;
			go2 = false;
		}
	}
}
