﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;
using MoreMountains.MMInterface;

namespace MoreMountains.CorgiEngine
{
	/// <summary>
	/// Simple start screen class.
	/// </summary>
	public class StartScreen : MonoBehaviour
	{
		/// the level to load after the start screen
		public string NextLevel;
		/// the delay after which the level should auto skip (if less than 1s, won't autoskip)
		public float AutoSkipDelay = 0f;

		[Header("Fades")]
		public float FadeInDuration = 1f;
		public float FadeOutDuration = 1f;


		/// <summary>
		/// Initialization
		/// </summary>
		protected virtual void Start()
		{	
			GUIManager.Instance.SetHUDActive (false);
			MMEventManager.TriggerEvent (new MMFadeOutEvent (FadeInDuration));

			if (AutoSkipDelay > 1f)
			{
				FadeOutDuration = AutoSkipDelay;
				StartCoroutine (LoadFirstLevel ());
			}
		}

		/// <summary>
		/// During update we simply wait for the user to press the "jump" button.
		/// </summary>
		protected virtual void Update()
		{
			if (!Input.GetButtonDown ("Player1_Jump"))
				return;
			
			ButtonPressed ();
		}

		/// <summary>
		/// What happens when the main button is pressed
		/// </summary>
		public virtual void ButtonPressed()
		{
			MMEventManager.TriggerEvent (new MMFadeInEvent (FadeOutDuration));
			// if the user presses the "Jump" button, we start the first level.
			StartCoroutine (LoadFirstLevel ());
		}

		/// <summary>
		/// Loads the next level.
		/// </summary>
		/// <returns>The first level.</returns>
		protected virtual IEnumerator LoadFirstLevel()
		{
			yield return new WaitForSeconds (FadeOutDuration);
			LoadingSceneManager.LoadScene (NextLevel);
		}
	}
}