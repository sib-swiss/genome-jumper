using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMute : MonoBehaviour
{

	public Sprite SpriteSoundOK;
	public Sprite SpriteSoundMute;

	public Image image;

	private void Start()
	{
		if (PlayerPrefs.HasKey("volume") && PlayerPrefs.GetInt("volume") == 1)
			image.sprite = SpriteSoundMute;

	}

	public void MuteSounds()
	{
		if (!PlayerPrefs.HasKey("volume") || PlayerPrefs.GetInt("volume") == 1)
		{
			PlayerPrefs.SetInt("volume", 0);
			image.sprite = SpriteSoundMute;
		}
		else
		{
			image.sprite = SpriteSoundOK;
			PlayerPrefs.SetInt("volume", 1);
		}
	}
	
}
