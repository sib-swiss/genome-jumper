using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using MoreMountains.Tools;

namespace MoreMountains.MMInterface
{
	public struct MMFadeEvent
	{
		public float Duration;
		public float TargetAlpha;

		/// <summary>
		/// Initializes a new instance of the <see cref="MoreMountains.MMInterface.MMFadeEvent"/> struct.
		/// </summary>
		/// <param name="duration">Duration, in seconds.</param>
		/// <param name="targetAlpha">Target alpha, from 0 to 1.</param>
		public MMFadeEvent (float duration, float targetAlpha)
		{
			Duration = duration;
			TargetAlpha = targetAlpha;
		}
	}

	public struct MMFadeInEvent 
	{ 
		public float Duration;

		/// <summary>
		/// Initializes a new instance of the <see cref="MoreMountains.MMInterface.MMFadeInEvent"/> struct.
		/// </summary>
		/// <param name="duration">Duration.</param>
		public MMFadeInEvent(float duration)
		{
			Duration = duration;
		}
	}

	public struct MMFadeOutEvent 
	{ 
		public float Duration;

		/// <summary>
		/// Initializes a new instance of the <see cref="MoreMountains.MMInterface.MMFadeOutEvent"/> struct.
		/// </summary>
		/// <param name="duration">Duration.</param>
		public MMFadeOutEvent(float duration)
		{
			Duration = duration;
		}
	}

	public class MMFader : MonoBehaviour, MMEventListener<MMFadeEvent>, MMEventListener<MMFadeInEvent>, MMEventListener<MMFadeOutEvent>
	{
		public bool Unscaled = true;
		public float FaderOnAlpha = 1f;

		protected CanvasGroup _canvasGroup;
		protected Image _image;

		protected float _currentTargetAlpha;
		protected float _currentDuration;

		protected bool _fading = false;
		protected float _fadeCounter;
		protected float _fadeStartTime = 0f;
		protected float _deltaAlpha;


		protected virtual void Start()
		{
			Initialization ();
		}

		protected virtual void Initialization()
		{
			_canvasGroup = GetComponent<CanvasGroup> ();
			_image = GetComponent<Image> ();
			if (!_fading)
			{
				_canvasGroup.alpha = 0;
				_image.enabled = false;	
			}
		}

		protected virtual void Update()
		{
			if (_canvasGroup == null) { return; }

			if (_fading) 
			{
				if (Unscaled)
				{
					if (Time.unscaledTime - _fadeStartTime < _currentDuration)
					{
						EnableFader ();
						_canvasGroup.alpha += _deltaAlpha * Time.unscaledDeltaTime;
					} 
					else
					{
						StopFading ();
					}
				} 
				else
				{					
					if (Time.time - _fadeStartTime < _currentDuration)
					{
						EnableFader ();
						_canvasGroup.alpha += _deltaAlpha * Time.deltaTime;
					} 
					else
					{
						StopFading ();
					}
				}
			}
		}

		protected virtual void StopFading()
		{
			_canvasGroup.alpha = _currentTargetAlpha;
			_fading = false;
			if (_canvasGroup.alpha == 0)
			{
				DisableFader ();
			}
		}

		protected virtual void DisableFader()
		{
			_image.enabled = false;	
			_canvasGroup.blocksRaycasts = false;
		}

		protected virtual void EnableFader()
		{
			_image.enabled = true;
			_canvasGroup.blocksRaycasts = true;
		}

		public virtual void OnMMEvent(MMFadeEvent fadeEvent)
		{
			_fading = true;
			_fadeCounter = 0f;
			_fadeStartTime = Time.time;
			_currentTargetAlpha = (fadeEvent.TargetAlpha == -1) ? FaderOnAlpha : fadeEvent.TargetAlpha;
			_currentDuration = fadeEvent.Duration;
			_deltaAlpha = ((_currentTargetAlpha - this.gameObject.GetComponentNoAlloc<CanvasGroup>().alpha) / _currentDuration);
		}

		public virtual void OnMMEvent(MMFadeInEvent fadeEvent)
		{
			this.gameObject.GetComponentNoAlloc<CanvasGroup>().alpha = 0f;
			_fading = true;
			_fadeCounter = 0f;
			_fadeStartTime = Time.time;
			_currentDuration = fadeEvent.Duration;
			_currentTargetAlpha = 1f;
			_deltaAlpha = (1f / _currentDuration);
		}

		public virtual void OnMMEvent(MMFadeOutEvent fadeEvent)
		{
			this.gameObject.GetComponentNoAlloc<CanvasGroup>().alpha = 1f;
			_fading = true;
			_fadeCounter = 0f;
			_fadeStartTime = Time.time;
			_currentTargetAlpha = 0f;
			_currentDuration = fadeEvent.Duration;
			_deltaAlpha = (-1f / _currentDuration);
		}

		protected virtual void OnEnable()
		{
			this.MMEventStartListening<MMFadeEvent> ();
			this.MMEventStartListening<MMFadeInEvent> ();
			this.MMEventStartListening<MMFadeOutEvent> ();
		}

		protected virtual void OnDisable()
		{
			this.MMEventStopListening<MMFadeEvent> ();
			this.MMEventStopListening<MMFadeInEvent> ();
			this.MMEventStopListening<MMFadeOutEvent> ();
		}
	}
}
