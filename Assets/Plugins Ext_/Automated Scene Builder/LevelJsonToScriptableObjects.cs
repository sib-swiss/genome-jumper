#if (UNITY_EDITOR) 
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelJsonToScriptableObjects : MonoBehaviour
{

	// Use this for initialization
	void Start()
    {


        var GeneScriptableObject = ScriptableObject.CreateInstance(typeof(ScriptableObject));
		AssetDatabase.CreateAsset(GeneScriptableObject,
			"Assets/SIB Assets/LevelDisplay/Genes/test.asset");

    }


}
#endif