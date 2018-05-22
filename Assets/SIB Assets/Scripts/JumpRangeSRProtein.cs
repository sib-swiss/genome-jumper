using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRangeSRProtein : MonoBehaviour {

    private GameObject Player;

    void Start () {
        Player = GameObject.Find("SIB Default Character");
    }

    public void OnTriggerStay2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            transform.parent.GetChild(3).GetComponent<SrProteine>().PlayerAllowedToJump = true;
        }
    }
}
