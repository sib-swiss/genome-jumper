using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjects : MonoBehaviour {

    [Header("Reference time")]
    public float referenceTime;

    [Header("Level informations")]
    public int variantesCount;
    public int startingHealth;
    public int coinsCount;
    

    void Update()
    {
        variantesCount = GameObject.FindGameObjectsWithTag("SuperVariante").Length;
        startingHealth = (int)GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().InitialHealth;
        coinsCount = GameObject.FindGameObjectsWithTag("Coins").Length;
    }
}
