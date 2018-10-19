using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelIcons : MonoBehaviour {

    public GameObject StartImage;
    public GameObject SnpImage;
    public GameObject StopImage;

    private GameObject StartCodon;
    private GameObject Snp;
    private GameObject StopCodon;

    public bool StartAnimation = true;
    public bool SnpAnimation = true;
    public bool StopAnimation = true;

    private void Start() {
        StartCodon = GameObject.Find("Start");
        Snp = GameObject.Find("Snp");
        StopCodon = GameObject.Find("Stop");

        if(SceneManager.GetActiveScene().name == "Tutorial") {
            this.gameObject.SetActive(false);
        }
    }

    private void Update() {
        if (StartCodon.transform.GetChild(2).GetComponent<StartStopCodonBehaviour>().hasBeenTriggered) {
            if (StartAnimation) {
                StartImage.GetComponent<ParticleSystem>().Play();
                StartCoroutine(FadeInImage(StartImage.GetComponent<Image>()));
                StartAnimation = false;
            }
            
        }
        if (Snp.transform.GetChild(2).GetComponent<SNPVariant>().SnpTriggered) {
            if (SnpAnimation) {
                SnpImage.GetComponent<ParticleSystem>().Play();
                StartCoroutine(FadeInImage(SnpImage.GetComponent<Image>()));
                SnpAnimation = false;
            }
            
        }
        if (StopCodon.transform.GetChild(2).GetComponent<StartStopCodonBehaviour>().hasBeenTriggered) {
            if (StopAnimation) {
                StopImage.GetComponent<ParticleSystem>().Play();
                StartCoroutine(FadeInImage(StopImage.GetComponent<Image>()));
                StopAnimation = false;
            }
            
        }
    }

    public IEnumerator FadeInImage(Image img) {
        for (float i = 0.29f; i <= 1; i += Time.deltaTime) {
            if(i >= 0.95f) {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
                yield break;
            }
            else {
                img.color = new Color(img.color.r, img.color.g, img.color.b, i);
                yield return null;
            }
        }
    }


}
