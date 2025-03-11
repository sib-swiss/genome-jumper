using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEndGameItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            GameObject SIBchar = GameObject.Find("SIB Default Character");
            GameObject Snp = GameObject.Find("Snp");
            GameObject EndPoint = GameObject.Find("EndPoint");

            Snp.transform.position = SIBchar.transform.position;
            EndPoint.transform.position = new Vector3(SIBchar.transform.position.x + 1.5f, SIBchar.transform.position.y, SIBchar.transform.position.z);
        }
    }
}
