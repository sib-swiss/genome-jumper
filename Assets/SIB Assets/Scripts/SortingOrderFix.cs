using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrderFix : MonoBehaviour {

    public string LayerName = "UI";

	// Use this for initialization
	void Start () {
        this.GetComponent<MeshRenderer>().sortingLayerName = LayerName;
	}
}
