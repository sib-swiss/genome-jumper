using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class ShareScore : MonoBehaviour
{
/*
	public void Share()
	{
		var score = PlayerPrefs.GetInt("CurrentLevelScore");
		var scenename = SceneManager.GetActiveScene().name;
	}
	*/

    // public
    // private
    private bool isProcessing = false;
    public string msg;

    public GameObject bottomPanel;
	public GameObject genomeJumperText;
    public GameObject items;
    public GameObject stores;
    public string ScreenshotName = "screenshot.png";

    public void ShareScreenshotWithText(string text)
    {
        string screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
        if (File.Exists(screenShotPath)) File.Delete(screenShotPath);

        ScreenCapture.CaptureScreenshot(ScreenshotName);

        StartCoroutine(delayedShare(screenShotPath, text));
    }

    //CaptureScreenshot runs asynchronously, so you'll need to either capture the screenshot early and wait a fixed time
    //for it to save, or set a unique image name and check if the file has been created yet before sharing.
    IEnumerator delayedShare(string screenShotPath, string text)
    {
        while (!File.Exists(screenShotPath))
        {
            yield return new WaitForSeconds(.05f);
        }

        NativeShare.Share(text, screenShotPath, "", "", "image/png", true, "");

        bottomPanel.SetActive(true);
        genomeJumperText.SetActive(false);
        items.SetActive(true);
        stores.SetActive(false);
    }

    public void ButtonShare()
    {
        bottomPanel.SetActive(false);
        genomeJumperText.SetActive(true);

        items.SetActive(false);
        stores.SetActive(true);
        ShareScreenshotWithText("Genome Jumper Game");
    }

    // OLD SYSTEM
    /*

    //function called from a button
    public void ButtonShare ()
    {
        
        bottomPanel.SetActive(false);
	    genomeJumperText.SetActive(true);

        items.SetActive(false);
        stores.SetActive(true);

        if (!isProcessing){
            StartCoroutine( ShareScreenshot() );
        }
    }


    public IEnumerator ShareScreenshot()
    {
        isProcessing = true;
        // wait for graphics to render
        yield return new WaitForEndOfFrame();
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
        // create the texture
        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24,true);
        // put buffer into texture
        screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height),0,0);
        // apply
        screenTexture.Apply();
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
        byte[] dataToSave = screenTexture.EncodeToPNG();
        string destination = Path.Combine(Application.persistentDataPath,System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
        File.WriteAllBytes(destination, dataToSave);
        if(!Application.isEditor)
        {
            // block to open the file and share it ------------START
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","file://" + destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
 
            intentObject.Call<AndroidJavaObject> ("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), ""+ msg);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");
 
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

	        currentActivity.Call("startActivity", intentObject);
        }
        isProcessing = false;

        bottomPanel.SetActive(true);
	    genomeJumperText.SetActive(false);
        items.SetActive(true);
        stores.SetActive(false);
    }*/


	
}

