using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFading : MonoBehaviour {

		public float minSize;
    public float decreaseFactor;

    void Start()
    {
        StartCoroutine(Scale());
    }

    IEnumerator Scale()
    {

			AudioSource audio = GetComponent<AudioSource>();
      audio.Play();

			while(transform.localScale.x > minSize)
			{
					//timer += Time.deltaTime;
					transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * decreaseFactor;
					yield return null;
			}

  		transform.localScale = new Vector3(0, 0, 0);

			gameObject.SetActive(false);



			/*
        float timer = 0;

        while(true) // this could also be a condition indicating "alive or dead"
        {
            // we scale all axis, so they will have the same value,
            // so we can work with a float instead of comparing vectors
            while(maxSize > transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
                yield return null;
            }
            // reset the timer

            yield return new WaitForSeconds(waitTime);

            timer = 0;
            while(1 < transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
                yield return null;
            }

            timer = 0;
            yield return new WaitForSeconds(waitTime);
        }*/
    }
}
