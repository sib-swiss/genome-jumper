using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRectTransformTo14OfScreen : MonoBehaviour
{
    public bool isPositive = false;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTransform = this.GetComponent<RectTransform>();
        float calculatedWidth = Screen.width / 3;
        rectTransform.sizeDelta = new Vector2(calculatedWidth, rectTransform.sizeDelta.y);

        if (isPositive) {
            rectTransform.anchoredPosition = new Vector2(calculatedWidth / 2, rectTransform.anchoredPosition.x);

        }
        else {
            rectTransform.anchoredPosition = new Vector2(-calculatedWidth / 2, rectTransform.anchoredPosition.x);
        }

        rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 0);
        rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, 0);
    }
}
