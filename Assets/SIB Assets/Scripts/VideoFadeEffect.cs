using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoFadeEffect : MonoBehaviour {
	
	public VideoPlayer videoPlayer;

    private void Start()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer == null)
            videoPlayer = GetComponentInParent<VideoPlayer>();
    }

    public void FadeIn()
	{
		StartCoroutine(FadeAction(videoPlayer, videoPlayer.targetCameraAlpha, 1));
	}
	

	public void FadeOut()
    {
        Debug.Log(videoPlayer);

		//No fadeOut for outro video
		if (videoPlayer.clip.name.Equals ("outro")) {
			videoPlayer.Stop ();
		} else {
			StartCoroutine(FadeAction(videoPlayer, videoPlayer.targetCameraAlpha, 0));
		}
     
	}

	
	public IEnumerator FadeAction(VideoPlayer vp, float start, float end, float lerpTime = 0.5f)
	{
		float _timeStartedLerping = Time.time;
		float timeSinceStarted = Time.time - _timeStartedLerping;
		float percentageComplete = timeSinceStarted / lerpTime;

		while (true)
		{
			timeSinceStarted = Time.time - _timeStartedLerping;
			percentageComplete = timeSinceStarted / lerpTime;

			float currentValue = Mathf.Lerp(start, end, percentageComplete);

			vp.targetCameraAlpha = currentValue;

			if (percentageComplete >= 1) break;
			
			yield return new WaitForEndOfFrame();
		}
		
		videoPlayer.Stop();
	}
}
