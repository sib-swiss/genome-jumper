using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessRenderScale : MonoBehaviour {

    public int renderScale = 3;

    void Start()
    {
        var ScreenWidth = Screen.width;
        var ScreenHeight = Screen.height;

        var ScreenWidthCut = ScreenWidth / renderScale;
        var ScreenHeightCut = ScreenHeight / renderScale;

        Screen.SetResolution(ScreenWidthCut, ScreenHeightCut, true);
    }
		
}
