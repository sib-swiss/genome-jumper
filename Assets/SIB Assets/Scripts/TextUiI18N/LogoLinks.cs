using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoLinks : MonoBehaviour
{

	private string Link;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	public void SetLink(string link)
	{
		Link = link;
	}

	public void GoLink()
	{
		Application.OpenURL(Link);
	}
}
