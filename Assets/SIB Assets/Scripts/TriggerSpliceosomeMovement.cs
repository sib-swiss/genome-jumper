using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpliceosomeMovement : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.name == "EXON")
        {
            collider.gameObject.GetComponent<MovingPlatform>().enabled = true;
        }
    }
}
