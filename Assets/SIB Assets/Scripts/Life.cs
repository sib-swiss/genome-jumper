using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour {

	public Sprite zero;
	public Sprite quart;
	public Sprite moitie;
	public Sprite troisQuart;
	public Sprite plein;

	public void UpdateLife (float life) {

		Image h1  = GameObject.Find ("h1").GetComponent<Image>();
		Image h2  = GameObject.Find ("h2").GetComponent<Image>();
		Image h3  = GameObject.Find ("h3").GetComponent<Image>();

		switch ((int) life) {
		case 0: 
			h3.sprite = zero;
			h2.sprite = zero;
			h1.sprite = zero;
			break;
		case 25 :
			h3.sprite = zero;
			h2.sprite = zero;
			h1.sprite = quart;
			break;
		case 50 :
			h3.sprite = zero;
			h2.sprite = zero;
			h1.sprite = moitie;
			break;
		case 75 :
			h3.sprite = zero;
			h2.sprite = zero;
			h1.sprite = troisQuart;
			break;
		case 100 :
			h3.sprite = zero;
			h2.sprite = zero;
			h1.sprite = plein;
			break;
		case 125 :
			h3.sprite = zero;
			h2.sprite = quart;
			h1.sprite = plein;
			break;
		case 150 :
			h3.sprite = zero;
			h2.sprite = moitie;
			h1.sprite = plein;
			break;
		case 175 :
			h3.sprite = zero;
			h2.sprite = troisQuart;
			h1.sprite = plein;
			break;
		case 200 :
			h3.sprite = zero;
			h2.sprite = plein;
			h1.sprite = plein;
			break;
		case 225 :
			h3.sprite = quart;
			h2.sprite = plein;
			h1.sprite = plein;
			break;
		case 250 :
			h3.sprite = moitie;
			h2.sprite = plein;
			h1.sprite = plein;
			break;
		case 275 :
			h3.sprite = troisQuart;
			h2.sprite = plein;
			h1.sprite = plein;
			break;
		default :
			h3.sprite = plein;
			h2.sprite = plein;
			h1.sprite = plein;
			break;			
		}
	}
}
