using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksOnCollide : MonoBehaviour
{

    public AudioClip spark;
    public GameObject sparks;

    void OnTriggerEnter2D(Collider2D collider)
    {
        // We check if we don't collider with anything else than Player (Usefull when multiple objects with this script attached are close, prevent from triggering every frame a useless function)
        if (collider.gameObject.tag.Equals("Player"))
        {
            sparks.GetComponent<ParticleSystem>().Play();
            AudioSource.PlayClipAtPoint(spark, transform.position, 0.6f);
        }
    }
}
