using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulatePublications : MonoBehaviour {

    public GenePopupCreator gene;
    public GameObject Publication1;
    public GameObject Publication2;

    private void OnEnable() {

        gene = this.GetComponent<GenePopupDisplay>().activeGene;

        if(!gene.publication1Title.Equals("")) {
            Publication1.SetActive(true);
            Publication1.GetComponent<Text>().text = " • " + gene.publication1Title;
            Publication1.GetComponent<Button>().onClick.AddListener(() => {
                Application.OpenURL(gene.publication1Link);
            });
        }
        if (!gene.publication2Title.Equals("")) {
            Publication2.SetActive(true);
            Publication2.GetComponent<Text>().text = " • " + gene.publication2Title;
            Publication2.GetComponent<Button>().onClick.AddListener(() => {
                Application.OpenURL(gene.publication2Link);
            });
        }
    }
}
