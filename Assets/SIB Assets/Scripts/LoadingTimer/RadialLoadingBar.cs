using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialLoadingBar : MonoBehaviour
{

	public Transform LoadingBar;
	public Transform TextIndicator;
	private int secondToWait = 15;
	private float currentAmount = 15;
	private float speed = .8f;
	
	// Update is called once per frame
	void Update () {
		if (currentAmount > 0)
		{
			currentAmount -= Time.timeScale*speed;
			TextIndicator.GetComponent<Text>().text = ((int) currentAmount).ToString();
		}
		else
		{
			this.transform.gameObject.SetActive(false);
		}

		LoadingBar.GetComponent<Image>().fillAmount = currentAmount / secondToWait;
	}
}
