using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePostProcessCam : MonoBehaviour {

    private GameObject[] variants;

	void Start () {
        int variantsLength = GameObject.FindGameObjectsWithTag("SuperVariante").Length;

        for (int i = 0; i < variantsLength; i++)
        {
            GameObject.FindGameObjectsWithTag("SuperVariante")[i].SetActive(false);
        }
        GameObject.Find("PostProcessCamera").SetActive(false);

		
	}
}
