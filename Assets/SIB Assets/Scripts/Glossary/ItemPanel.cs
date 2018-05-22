using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{

	private LeanLocalizedTextArgs title;
	private LeanLocalizedTextArgs descritpion;
	private Image image;
	
	// Use this for initialization
	private void Awake()
	{
		title = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<LeanLocalizedTextArgs>();
		descritpion = gameObject.transform.GetChild(0).transform.GetChild(2).GetComponentInChildren<LeanLocalizedTextArgs>();
		image = gameObject.transform.GetChild(0).transform.GetChild(1).transform.GetComponentInChildren<Image>();
	}

	public void setItemInfos(GlossaryItems infos)
	{
		string titleKey = infos.getTitleKey();
		title.PhraseName = titleKey;
		descritpion.PhraseName = titleKey.Substring(0, titleKey.Length - 5) + "text";
		image.sprite = infos.image;
	}
}
