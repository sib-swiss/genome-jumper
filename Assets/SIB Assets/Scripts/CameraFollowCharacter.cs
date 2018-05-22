using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCharacter : MonoBehaviour {

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

	// Update is called once per frame
	void Update () {
        if(player.transform.position.x + 4f > 18.9174f)
        {
            GetComponent<Camera>().transform.position = new Vector3(player.transform.position.x+8f, GetComponent<Camera>().transform.position.y, GetComponent<Camera>().transform.position.z);
        }
        

	}
}
