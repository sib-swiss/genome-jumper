using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingMenu : MonoBehaviour {

    public GameObject LoadingPanel;

	public void ShowLoading()
    {
        LoadingPanel.SetActive(true);
    }
}
