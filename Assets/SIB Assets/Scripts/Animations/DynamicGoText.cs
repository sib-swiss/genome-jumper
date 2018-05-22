using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGoText : MonoBehaviour {

    public GameObject arrowEmpty;
    public GameObject arrowFull;

    void Update()
    {
        if (Mathf.RoundToInt(Time.time) % 2 == 0)
        {
            arrowEmpty.SetActive(true);
            arrowFull.SetActive(false);
        };
        if (Mathf.RoundToInt(Time.time) % 2 == 1)
        {
            arrowEmpty.SetActive(false);
            arrowFull.SetActive(true);
        };
    }
}
