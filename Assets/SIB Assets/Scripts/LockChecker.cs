using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockChecker : MonoBehaviour {

    public Sprite Info;
    public Sprite Lock;

    void Start()
    {

        if (transform.GetChild(0).GetComponent<Button>().interactable == true)
        {
            transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color(38f, 190f, 255f, 255f);
            transform.GetChild(1).gameObject.GetComponentInChildren<Image>().sprite = Info;
        }
        if (transform.GetChild(0).GetComponent<Button>().interactable == false)
        {
            transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            transform.GetChild(1).gameObject.GetComponentInChildren<Image>().sprite = Lock;
            transform.GetChild(1).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(17.5f, 20);
        }
    }
}
