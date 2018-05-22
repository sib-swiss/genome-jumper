using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

	public AudioClip sound;

	// Use this for initialization
	public void Play()
	{
		SoundManager.Instance.PlaySound(sound, transform.position);
	}

}
