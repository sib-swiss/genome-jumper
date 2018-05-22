using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLinks : MonoBehaviour
{

	
	public enum TextWithLinks { SNP, TRANSCRIPT, SCIENTIFIC }
	public TextWithLinks textWithLinks;
	
	[Header("Links")] 
	public string Link;
/** UNDERLINE
	public int underlineStart = 0;
	public int underlineEnd = 0;
	
	private Text text;
	private RectTransform textRectTransform = null;
	private TextGenerator textGenerator = null;

	private GameObject lineGameObject = null;
	private Image lineImage = null;
	private RectTransform lineRectTransform = null;

	private GenePopupCreator activeGene;

	private int txtlength;

	private void Start()
	{
		activeGene = GameObject.Find("LevelManager").GetComponent<LevelProperties>().GeneScriptableObject;

		switch (textWithLinks)
		{
			case TextWithLinks.SNP : txtlength = activeGene.snpName.Length;
				break;
			case TextWithLinks.TRANSCRIPT : txtlength = activeGene.transcript.Length;
				break;
		}
		
		text = gameObject.GetComponent<Text>();
		textRectTransform = gameObject.GetComponent<RectTransform>();
		textGenerator = text.cachedTextGenerator;
		lineGameObject = new GameObject("Underline");
		lineImage = lineGameObject.AddComponent<Image>();
		lineImage.color = text.color;
		lineRectTransform = lineGameObject.GetComponent<RectTransform>();
		lineRectTransform.SetParent(transform, false);
		lineRectTransform.anchorMin = textRectTransform.pivot;
		lineRectTransform.anchorMax = textRectTransform.pivot;
	}*/

	public void GoToLink()
	{
		Application.OpenURL(Link);
	}

    public void GoToLinkString(string link)
    {
        Application.OpenURL(link);
    }

    /* UNDERLINE TEXT 
	private void Update()
	{
		
		
		if (textGenerator.characterCount < 0)
			return;
		
		UICharInfo[] charactersInfo = textGenerator.GetCharactersArray();

		underlineStart = charactersInfo.Length - (txtlength + 9);
		underlineEnd = charactersInfo.Length - 1;

		if (!(underlineEnd > underlineStart && underlineEnd < charactersInfo.Length))
			return;

		UILineInfo[] linesInfo = textGenerator.GetLinesArray();
		if (linesInfo.Length < 1)
			return;

		float height = linesInfo[0].height -0.5f;
		Canvas canvas = gameObject.GetComponentInParent<Canvas>();
		float factor = 1.0f/canvas.scaleFactor;
		lineRectTransform.anchoredPosition = new Vector2(
			factor*(charactersInfo[underlineStart].cursorPos.x+charactersInfo[underlineEnd].cursorPos.x)/2.0f,
			factor*(charactersInfo[underlineStart].cursorPos.y-height/1.0f)
		);
		lineRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, factor*Mathf.Abs(charactersInfo[underlineStart].cursorPos.x-charactersInfo[underlineEnd].cursorPos.x));
		lineRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height/10.0f);
	}
	*/
}
