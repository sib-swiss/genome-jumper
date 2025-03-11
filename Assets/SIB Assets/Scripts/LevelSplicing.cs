using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSplicing : MonoBehaviour {

	public bool shouldTrigger = false;

	public static float movingSpeed = 3.33f; // Compteur de déplacement

	public static GameObject lastExonFlag;
	public static GameObject lastIntronFlag;

	public GameObject _lastExonFlag;
	public GameObject _lastIntronFlag;

	public List<GameObject> moveables = new List<GameObject>(); // Liste statique de l'ensemble des objets déplacable lors du splicing
	public List<GameObject> toMoves = new List<GameObject>(); //Object à déplacer

	private float toMoveDistance = 0; //Distance de déplacement en x
	private float toMoveInc = 0; // Compteur de déplacement

	public static int scriptCounter = 0;
	private int currentScriptCounter = 0;


    void Start()
    {
		//resetSplicing();


		currentScriptCounter = scriptCounter;

		if (moveables.Count == 0) {
			moveables.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));
			moveables.AddRange(GameObject.FindGameObjectsWithTag("Exon"));
      		moveables.AddRange(GameObject.FindGameObjectsWithTag("Intron"));
     		moveables.AddRange(GameObject.FindGameObjectsWithTag("Small Intron"));
			moveables.AddRange(GameObject.FindGameObjectsWithTag("Medium Intron"));
			moveables.AddRange(GameObject.FindGameObjectsWithTag("Big Intron"));
			moveables.AddRange(GameObject.FindGameObjectsWithTag("Large Intron"));
			moveables.AddRange(GameObject.FindGameObjectsWithTag("Huge Intron"));
			moveables.AddRange(GameObject.FindGameObjectsWithTag("Utr"));
			moveables.AddRange(GameObject.FindGameObjectsWithTag("Snp"));
			moveables.AddRange(GameObject.FindGameObjectsWithTag("Up"));
			moveables.AddRange(GameObject.FindGameObjectsWithTag("Down"));
			moveables.AddRange(GameObject.FindGameObjectsWithTag("Start Codon"));
			moveables.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
			moveables.AddRange(GameObject.FindGameObjectsWithTag("Intron Flag"));
			moveables.AddRange(GameObject.FindGameObjectsWithTag("Exon Flag"));
			moveables.AddRange(GameObject.FindGameObjectsWithTag("End Codon"));
		}

		//Debug.Log ("Initializing Moveables : "+moveables.Count+" items");
    }

	public void resetSplicing(){
		toMoves = new List<GameObject>();

		lastExonFlag = null;
		lastIntronFlag = null;
		scriptCounter = 0;
	}

    void Update()
    {
		if (shouldTrigger) {

			if (toMoveDistance == 0) {

				//Calcul de la distance entre les deux drapeaux OU autre solution possible calculer la taille de l'intron que l'on vient de franchir, cela implique de raycast aussi l'intron en plus des drapeaux
				if(_lastExonFlag == null || _lastIntronFlag == null) {
					return;
				}
				toMoveDistance = _lastExonFlag.transform.position.x - _lastIntronFlag.transform.position.x;

				scriptCounter++;


				//Recupération de tous les éléments avant le drapeau d'exon, pour les déplacer
				foreach (GameObject moveable in moveables) {
					if (moveable.transform.position.x < _lastIntronFlag.transform.position.x+0.5f) {
						toMoves.Add (moveable);
					}
				}

				//Duplication du script pour le prochain Splicing à venir
				gameObject.AddComponent<LevelSplicing>();

				//Debug.Log ("Initializing distance to move "+toMoveDistance);

			} else {
				//Distance déjà calculée
				foreach (GameObject toMove in toMoves) {
					toMove.transform.position =		new Vector3(toMove.transform.position.x + (movingSpeed * Time.deltaTime),toMove.transform.position.y,toMove.transform.position.z);
				}

				toMoveInc += (movingSpeed * Time.deltaTime);

				if (toMoveInc >= toMoveDistance) {
					//GetComponents<LevelSplicing>()[currentScriptCounter].enabled=false;
					this.enabled = false;
				}
			}
		} else {

			Vector3 FixedRay = new Vector3(transform.position.x, (transform.position.y - 0.1770639f) - 0.1f, transform.position.z);
			Debug.DrawRay(FixedRay, Vector3.down * 5f, Color.green);
			//RaycastHit2D ray = Physics2D.Raycast(FixedRay, Vector3.down * 0.2f);
			RaycastHit2D[] raycastAll= Physics2D.RaycastAll(FixedRay, Vector3.down * 5f);

			for (var i = 0; i < raycastAll.Length; i++) {
				RaycastHit2D raycast = raycastAll [i];
				if (raycast.collider.tag == "Exon Flag") {

					_lastExonFlag = raycast.transform.gameObject;

					//On compare la variable statique de tous les script level splicing avec la variable locale
					if (_lastExonFlag != lastExonFlag) {
						lastExonFlag = _lastExonFlag;

						shouldTrigger = true;

					}

				} else if (raycast.collider.tag == "Intron Flag") {

					_lastIntronFlag = raycast.transform.gameObject;

					//On compare la variable statique de tous les script level splicing avec la variable locale
					if (_lastIntronFlag != lastIntronFlag) {
						lastIntronFlag = _lastIntronFlag;
					}

				}
			}
		}

    }

}
