using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SkipVideo : MonoBehaviour
{

	private Button btn;

    public void StopIntroMovie()
	{
        GetComponent<VideoFadeEffect>().FadeOut();
	}


    public void StopOutroMovie()
    {
        if (PlayerPrefs.GetString("Difficulty").Equals("easy"))
        {
            GetComponent<VideoFadeEffect>().FadeOut();
            GameObject.FindGameObjectWithTag("endPoint").GetComponent<MoreMountains.CorgiEngine.EndLevelTrigger>().ActionOnTrigger();
        }
    }


}
