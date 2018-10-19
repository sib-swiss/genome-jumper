using Lean.Localization;
using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreenBehavior : MonoBehaviour {

    [Header("Top UI")]
    public Image Top;
    public GameObject Stars;
    public Text GameWinText;

    [Header("Middle UI")]
    public GameObject HeadItems;
    public GameObject RightHandItems;
    public Image MRTop;
    public Image MRBot;
    public GameObject FirstBloc;
    public GameObject SecondBloc;
    public Image ViewPort;
    public GameObject ViewPortContent;
    public GameObject ScientificBloc;
    public GameObject Scrollbar;
    public Text TimeScore;
    public Image TimeIcon;
    public Text Score;
    public Image ScoreIcon;

    [Header("Bottom UI")]
    public GameObject returnBtn;
    public GameObject shareBtn;
    public GameObject restartBtn;
    public GameObject nextBtn;

    [Header("Text Display")]
    public Text WellDoneText;

    private void Start() {
        StartCoroutine(FadeEverything());
    }

    public IEnumerator FadeEverything() {

        if (GameObject.FindGameObjectWithTag("Player") != null) {
            if (GameObject.FindGameObjectWithTag("Player").activeInHierarchy) {
                GameObject.FindGameObjectWithTag("Player").SetActive(false);
            }
        }

        WellDoneText.GetComponent<LeanLocalizedTextArgs>().SetArg(PlayerPrefs.GetString("AvatarName"), 0);

        //BG FADE
        this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r, this.GetComponent<Image>().color.g, this.GetComponent<Image>().color.b, 0);

        // TOP FADE
        StartCoroutine(FadeOutImage(Top));
        Stars.GetComponent<StarsAnimations>().enabled = false;
        for(int i = 0; i < Stars.transform.childCount; i++) {
            if(Stars.transform.GetChild(i).GetComponent<Image>() != null) {
                StartCoroutine(FadeOutImage(Stars.transform.GetChild(i).GetComponent<Image>()));
            }
            if(Stars.transform.GetChild(i).childCount > 0) {
                Stars.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        }
        StartCoroutine(FadeOutText(GameWinText));

        // MIDDLE FADE
        for(int j = 0; j < HeadItems.transform.childCount; j++) {
            HeadItems.transform.GetChild(j).gameObject.SetActive(false);
        }
        for(int k = 0; k < RightHandItems.transform.childCount; k++) {
            RightHandItems.transform.GetChild(k).gameObject.SetActive(false);
        }
        StartCoroutine(FadeOutImage(MRTop));
        StartCoroutine(FadeOutImage(MRBot));

        for(int l = 0; l < FirstBloc.transform.childCount; l++) {
            StartCoroutine(FadeOutText(FirstBloc.transform.GetChild(l).GetComponent<Text>()));
        }
        for (int m = 0; m < SecondBloc.transform.childCount; m++) {
            StartCoroutine(FadeOutText(SecondBloc.transform.GetChild(m).GetComponent<Text>()));
        }

        //StartCoroutine(FadeOutImage(ViewPort));

        for(int w = 0; w < ViewPortContent.transform.childCount; w++) {
            if(ViewPortContent.transform.GetChild(w).GetComponent<Text>() != null) {
                StartCoroutine(FadeOutText(ViewPortContent.transform.GetChild(w).GetComponent<Text>()));
            }
        }

        for(int z = 0; z < ScientificBloc.transform.childCount; z++) {
            if (ScientificBloc.transform.GetChild(z).GetComponent<Text>() != null){
                StartCoroutine(FadeOutText(ScientificBloc.transform.GetChild(z).GetComponent<Text>()));
            }
        }

        StartCoroutine(FadeOutImage(Scrollbar.GetComponent<Image>()));
        Scrollbar.GetComponent<Scrollbar>().enabled = false;
        StartCoroutine(FadeOutImage(Scrollbar.transform.GetChild(0).GetChild(0).GetComponent<Image>()));

        StartCoroutine(FadeOutText(TimeScore));
        StartCoroutine(FadeOutImage(TimeIcon));
        StartCoroutine(FadeOutText(Score));
        StartCoroutine(FadeOutImage(ScoreIcon));

        StartCoroutine(FadeOutImage(returnBtn.transform.GetChild(0).GetComponent<Image>()));
        StartCoroutine(FadeOutText(returnBtn.transform.GetChild(1).GetComponent<Text>()));

        StartCoroutine(FadeOutImage(shareBtn.transform.GetChild(0).GetComponent<Image>()));
        StartCoroutine(FadeOutText(shareBtn.transform.GetChild(1).GetComponent<Text>()));

        StartCoroutine(FadeOutImage(restartBtn.transform.GetChild(0).GetComponent<Image>()));
        StartCoroutine(FadeOutText(restartBtn.transform.GetChild(1).GetComponent<Text>()));

        StartCoroutine(FadeOutImage(nextBtn.transform.GetChild(0).GetComponent<Image>()));
        StartCoroutine(FadeOutText(nextBtn.transform.GetChild(1).GetComponent<Text>()));

        yield return new WaitForSeconds(0.5f);

        // Disable all gameobjects to prevent persistent screening

        StartCoroutine(FadeInImage(WellDoneText.transform.GetChild(0).GetChild(0).GetComponent<Image>()));
        StartCoroutine(FadeInText(WellDoneText));
        WellDoneText.transform.GetChild(0).GetChild(0).GetComponent<Button>().enabled = true;
        StartCoroutine(FadeInText(WellDoneText.transform.GetChild(0).GetChild(1).GetComponent<Text>()));

        yield return null;
    }

    public IEnumerator FadeOutText(Text txt) {
        for(float i = 1; i >= 0; i -= Time.deltaTime*2) {
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, i);
            if(i <= 0.05f) {
                txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 0);
                txt.gameObject.SetActive(false);
                yield break;
            }
            yield return null;
        }
    }

    public IEnumerator FadeOutImage(Image img) {
        for (float i = 1; i >= 0; i -= Time.deltaTime*2) {
            img.color = new Color(img.color.r, img.color.g, img.color.b, i);
            if (i <= 0.05f) {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
                img.gameObject.SetActive(false);
                yield break;
            }
            yield return null;
        }
    }

    public IEnumerator FadeInText(Text txt) {
        for (float i = 0; i <=1; i += Time.deltaTime*2) {
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, i);
            if(i >= 0.95f) {
                txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 255);
                yield break;
            }
            yield return null;
        }
    }

    public IEnumerator FadeInImage(Image img) {
        for (float i = 0; i <= 1; i += Time.deltaTime*2) {
            img.color = new Color(img.color.r, img.color.g, img.color.b, i);
            if (i >= 0.95f) {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 255);
                yield break;
            }
            yield return null;
        }
    }
}
