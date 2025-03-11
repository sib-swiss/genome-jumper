using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetSNPText : MonoBehaviour
{

    public Text text;
    public LeanLocalizedTextArgs lean;

    public AnimationCurve slideCurve;
    public RectTransform textRect;
    public Vector2 targetPosition = new Vector2(0, 300);

    private Vector2 startPosition;

    private float timer = 0f;
    private bool isSliding = false;
    private float slideDuration = 1f;
    public float disableDelay = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<Text>();
        lean = this.GetComponent<LeanLocalizedTextArgs>();
        string geneName = SceneManager.GetActiveScene().name.ToLower();
        if (geneName == "foxo3alternate") { geneName = "foxo3"; }
        lean.PhraseName = "gene-" + geneName + "-short-text";

        Canvas canvas = textRect.transform.parent.parent.GetComponent<Canvas>();
        float screenWidth = canvas.GetComponent<RectTransform>().rect.width;
        startPosition = new Vector2(-screenWidth / 2 - textRect.rect.width, targetPosition.y);
        textRect.anchoredPosition = startPosition;
        this.TriggerSlideIn();
    }

    private void Update() {
        if(isSliding) {
            timer += Time.deltaTime;

            float t = Mathf.Clamp01(timer / slideDuration);
            float curveT = slideCurve.Evaluate(t);

            textRect.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, curveT);

            if (t >= 1)
            {
                isSliding = false;
                Invoke(nameof(DisableText), disableDelay);
            }
        }
    }


    public void TriggerSlideIn()
    {
        timer = 0f;
        isSliding = true;
    }

    private void DisableText()
    {
        gameObject.SetActive(false);
    }
}
