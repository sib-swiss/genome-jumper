using System.Collections;
using System.Collections.Generic;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

public class GlossaryItems : MonoBehaviour
{

	public Sprite image;
	private string TitleKey;

	private void Start()
	{
		TitleKey = GetComponentInChildren<LeanLocalizedTextArgs>().PhraseName;
	}

	public string getTitleKey()
	{
		return TitleKey;
	}
}
