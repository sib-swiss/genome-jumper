/* * * * *
 * Automated Scene Builder
 * 
 * Reads in a json file and places gameobjects into your scene.
 *
 * Json format is as follows:
 * 	{
 *    "sceneObjects":[
 *      {"machineName":"Rock","x":4,"y":0,"z":0},
 *      {"machineName":"Apple","x":1,"y":1,"z":0,"isAbsolute":1}
 *    ]
 *  }
 * 
 * 	- machineName matches up to the ISceneObjects's machineName property.
 *  - x, y, and z are the x,y,z to position the game object at.
 * 	  These can either be an units relative to the GetPosition method of your concrete ISceneObject
 * 	  or they can be an absolute world positions.
 *  - isAbsolute specifies if the x,y,z are to be absolute or processed by GetPosition
 *    in your concrete ISceneObject classes.
 *
 * 
 * ------------------------------
 * Written by Josh Robison <joshua.a.robison@gmail.com>
 * Team Robison
 * 2015-02-04
 * 
 * Modified by Medaille Christopher <christopher@pinterac.net>
 * Pinterac - SIB Genome Jumper Project
 * 2017-12-31
 * * * * */

#if (UNITY_EDITOR)
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using SimpleJSON;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;
using MoreMountains.MMInterface;
using MoreMountains.CorgiEngine;
using NUnit.Framework.Internal.Filters;
using Mr1;

public class levelY : MonoBehaviour{
    public static float positionY = 0f;
    public static bool upPrefab;
    public static bool downPrefab;
    public static Vector3 objPosCorrectionUp;
    public static Vector3 objPosCorrectionDown;
    public static float levelLength = 0f;
    public static float levelLengthLastObjScale = 0f;
}
public class AutomatedSceneBuilder : EditorWindow {

	static private Vector3 previousPosition = Vector3.zero;
    static public string path;
	static public GameObject origin = null;
    static private int wpNb = 0;
	public static IDictionary<string,ISceneObject> GetSceneObjects()
	{
		IDictionary<string,ISceneObject> result = new Dictionary<string,ISceneObject>();
		Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
		foreach (Assembly assemblyObj in assemblies)
		{
			System.Type[] types = assemblyObj.GetTypes();
			foreach (System.Type t in types)
			{
				if (t.GetInterface("ISceneObject") != null) {

					// Create an instance of the object
					ISceneObject obj = (ISceneObject)assemblyObj.CreateInstance(t.FullName);

					// Now add it to the dictionary by its machine name for handling events
					string machineName = obj.machineName;
					KeyValuePair<string,ISceneObject> kvp = new KeyValuePair<string,ISceneObject>(
						machineName,
						obj
					);

					// Overwrite or add
					if(result.ContainsKey(machineName)) {
						result[machineName] = obj;
					} else {
						result.Add(kvp);
					}

				}
			}
		}
		return result;
	}
	
	[MenuItem ("Tools/Automated Scene Builder/Create LevelsJson from JsonOrigin")]
	static void CreateLevelsJson (){
		CreateLvlsJson();
	}
	
	[MenuItem ("Tools/Automated Scene Builder/Read Template File")]
	static void ReadTemplateFile () {
		path = EditorUtility.OpenFilePanel(
			"Open Scene Template",
			"",
			"json"
		);
	}

    [MenuItem ("Tools/Automated Scene Builder/Build")]
	static void Build () {

		// Reset previous position
		previousPosition = Vector3.zero;

		// Grab the origin
		if(origin == null) {
			Debug.Log("Origin was not set.  Defaulting to (0,0,0).");
		}

		// No previous path?
		if(string.IsNullOrEmpty(path)) {
			path = EditorUtility.OpenFilePanel(
				"Open Scene Template",
				"",
				"json"
			);
		}

		// Did we get a path?
		if(!string.IsNullOrEmpty(path)) {
			StreamReader sr = new StreamReader(path);
			string fileContents = sr.ReadToEnd();
			sr.Close();
		
			// Parse the contents
			if(!string.IsNullOrEmpty(fileContents)) {
				ParseFileContents(fileContents);
			}
		}
            Debug.Log("Saving Generated level ...");
            Scene scene = SceneManager.GetActiveScene();
            EditorSceneManager.SaveScene(scene);
            Debug.Log("Generated Level saved");
	}

	[MenuItem ("Tools/Automated Scene Builder/Reset")]
	static public void ClearObjects() {
		// Clear out old items from scene
    IDictionary<string,ISceneObject> sceneObjects = GetSceneObjects();
		foreach(KeyValuePair<string, ISceneObject> entry in sceneObjects) {
			ISceneObject obj = entry.Value;
			string tag = obj.tag;
            levelY.positionY = 0f;

            if (!string.IsNullOrEmpty(tag)) {
				try {
					foreach(GameObject gameObj in GameObject.FindGameObjectsWithTag(tag)) {
						GameObject.DestroyImmediate(gameObj);
					}
				} catch (UnityException e) {
					Debug.LogWarning(e.Message);
					continue;
				}
			}
		}
	}
		
	[MenuItem ("Tools/Automated Scene Builder/Reset and Build")]
	static void ResetAndBuild () {
        ClearObjects();
		Build();
	}

	[MenuItem ("Tools/Automated Scene Builder/Set Origin")]
	static void SetOrigin() {
		GetWindow<AutomatedSceneBuilder>();
	}
	
	/*
	[MenuItem ("Tools/Automated Scene Builder/Convert LevelsJson to ScriptablesObjects")]
	static void LvlJsonConverter (){
		LvlJsonConvertToScriptableObjects();
	}*/

	static private void ParseFileContents(string fileContents) {
		// Parse the json
		JSONNode parsed = JSON.Parse(fileContents);

		// Grab scene objects from json
		if(parsed["sceneObjects"] != null) {
			// Now loop through the parts and build objects
			IDictionary<string,ISceneObject> sceneObjects = GetSceneObjects();
			for(int i = 0;i < parsed["sceneObjects"].Count;i++) {
				JSONNode node = parsed["sceneObjects"][i];
				if(node != null) {
					string machineName = node["machineName"];

					// Get x
					float x = node["x"].AsFloat;
                    levelY.levelLength = x;
					// Get y
					float y = node["y"].AsFloat;
                    // Get z
                    float z = node["z"].AsFloat;
                    // Get X Scale
                    float scale = node["scale"].AsFloat;
                    float posY = levelY.positionY;
                    levelY.levelLengthLastObjScale = scale;

                    // Check for existing variables
                    if (machineName != null && !float.IsNaN(x) && !float.IsNaN(y) && !float.IsNaN(z) && !float.IsNaN(scale)) {

                        if (machineName == "exon")
                        {
                            ISceneObject obj;
                            if (sceneObjects.TryGetValue(machineName, out obj))
                            {
                                Vector3 position = Vector3.zero;
                                Vector3 size = Vector3.zero;
                                int w = 0;
                                float posXsplit = x;
                                
                                float posYsplit = y;
                                float posZsplit = z;
                                bool downToInstanciate = false;
                                bool upToInstanciate = false;

                                while (w != scale)
                                {

                                    if ((scale > 4) && ( w >=1 ) && (w < scale-1))
                                    {
                                        int rand = Random.Range(-10, 10);

                                        if (rand > 8)
                                        {
                                            levelY.positionY = levelY.positionY + 1;
                                            position = new Vector3(posXsplit, levelY.positionY, posZsplit);
                                            posXsplit = posXsplit + 1;
                                            upToInstanciate = true;
                                        }
                                        else if (rand < -9)
                                        {
                                            levelY.positionY = levelY.positionY - 1;
                                            position = new Vector3(posXsplit, levelY.positionY, posZsplit);
                                            posXsplit = posXsplit + 1;
                                            downToInstanciate = true;
                                        }
                                        else
                                        {
                                            position = new Vector3(posXsplit, levelY.positionY, posZsplit);
                                            posXsplit = posXsplit + 1;
                                        }
                                    }
                                    else
                                    {
                                        position = new Vector3(posXsplit, levelY.positionY, posZsplit);
                                        posXsplit = posXsplit + 1;
                                    }

                                    string resourcePath = obj.resourcePath;
                                    int randomNumber = Random.Range(0, 25);

                                    if (upToInstanciate == true)
                                    {
                                        GameObject gameObj1 = Resources.Load("Exon_Up", typeof(GameObject)) as GameObject;
                                        Transform gameObjTransform1 = gameObj1.GetComponentsInChildren<Transform>(true)[0];
                                        GameObject gameObjInstance1 = (GameObject)PrefabUtility.InstantiatePrefab(gameObj1);
                                        gameObjInstance1.transform.position = new Vector3(posXsplit - 1, levelY.positionY - 0.5f, posZsplit);
                                        gameObjInstance1.transform.rotation = gameObjTransform1.rotation;
                                        gameObjInstance1.transform.localScale = new Vector3(1, 1, 1);
                                        upToInstanciate = false;
                                        w++;
                                    }
                                    else if (downToInstanciate == true)
                                    {
                                        GameObject gameObj1 = Resources.Load("Exon_Down", typeof(GameObject)) as GameObject;
                                        Transform gameObjTransform1 = gameObj1.GetComponentsInChildren<Transform>(true)[0];
                                        GameObject gameObjInstance1 = (GameObject)PrefabUtility.InstantiatePrefab(gameObj1);
                                        gameObjInstance1.transform.position = new Vector3(posXsplit - 1, levelY.positionY + 1.5f, posZsplit);
                                        gameObjInstance1.transform.rotation = gameObjTransform1.rotation;
                                        gameObjInstance1.transform.localScale = new Vector3(1, 1, 1);
                                        downToInstanciate = false;
                                        w++;
                                    }
                                    else
                                    {
                                        // Now load the game object and add to the scene
                                        GameObject gameObj = Resources.Load("Exon_Platform" + randomNumber, typeof(GameObject)) as GameObject;
                                        Transform gameObjTransform = gameObj.GetComponentsInChildren<Transform>(true)[0];
                                        GameObject gameObjInstance = (GameObject)PrefabUtility.InstantiatePrefab(gameObj);
                                        gameObjInstance.transform.position = position;
                                        gameObjInstance.transform.rotation = gameObjTransform.rotation;
                                        gameObj.name = "Exon_Plateform" + w;
                                        gameObjInstance.transform.localScale = new Vector3(1, 1, 1);
                                        w++;
                                    }
                                 }
                            }
                        }
                        // If the Json line contain "Exon" as machineName , then we start checking if he's a large exon so we can split it in 3 parts
                        //if (machineName == "exon" && scale > 12)
                        //{
                        //    for (int p = 1; p < 4 ; p++)
                        //    {
                        //        ISceneObject obj;
                        //        if (sceneObjects.TryGetValue(machineName, out obj))
                        //        {

                        //            // Grab position
                        //            Vector3 position = Vector3.zero;
                        //            Vector3 size = Vector3.zero;


                        //            // If absolute, then use the coordinates as they are
                        //            bool isAbsolute = node["isAbsolute"].AsBool;
                        //            if (!isAbsolute)
                        //            {
                        //                // We start to splice the exon in 3 different parts to change it's Y position
                        //                if (p == 1 ) {
                        //                    position = new Vector3(x, posY, z);
                        //                }
                        //                if (p == 2)  {
                        //                    // Create the 2nd part of the exon, with a random Y position depending on Random.range
                        //                    float posX = x + (scale * 0.3333f);
                        //                    int rand = Random.Range(-10, 10);
                        //                    Debug.Log("Rand Gen : " + rand);
                        //                    if(rand > 0)
                        //                    {
                        //                        posY = posY + 1;
                        //                        levelY.upPrefab = true;
                        //                    }
                        //                    else if (rand < -1)
                        //                    {
                        //                        posY = posY - 1;
                        //                        levelY.downPrefab = true;
                        //                    }
                        //                    else
                        //                    {
                        //                       levelY.upPrefab = false;
                        //                       levelY.downPrefab = false;
                        //                    }
                        //                    levelY.positionY = posY;
                        //                    position = new Vector3(posX, posY, z);

                        //                    // Positions of Gameobjects wrappers need to be posY : +- 0.5 for stairs
                        //                    levelY.objPosCorrectionUp = new Vector3(posX-1, posY , z);
                        //                    levelY.objPosCorrectionDown = new Vector3(posX, posY+1, z);



                        //                }
                        //                if (p == 3) {
                        //                    float posX = x + ( scale * 0.3333f * 2);
                        //                    position = new Vector3(posX, posY , z);
                        //                }

                        //                float scale_third = (scale * 0.3333f);
                        //                size = new Vector3(scale_third, y, z);
                        //            }
                        //            // Otherwise, use position magic from scene objects
                        //            else
                        //            {
                        //                position = obj.GetPosition(x, y, z);
                        //                size = obj.GetScale(scale * 0.3333f, 1, 1);
                        //            }
                        //            if(levelY.upPrefab == true)
                        //            {
                        //                Debug.Log("top");
                        //                Debug.Log("Prev pos : " + levelY.objPosCorrectionUp);
                        //                GameObject gameObj1 = Resources.Load("Up") as GameObject;
                        //               GameObject.Instantiate(gameObj1, levelY.objPosCorrectionUp, Quaternion.identity);
                        //            }
                        //            if (levelY.downPrefab == true)
                        //            {
                        //                Debug.Log("down");
                        //                Debug.Log("Prev pos : " + levelY.objPosCorrectionDown);
                        //                GameObject gameObj2 = Resources.Load("Down") as GameObject;
                        //                GameObject.Instantiate(gameObj2, levelY.objPosCorrectionDown, Quaternion.identity);
                        //            }
                        //            levelY.upPrefab = false;
                        //            levelY.downPrefab = false;

                        //            // Grab resource path
                        //            string resourcePath = obj.resourcePath;

                        //            // Now load the game object and add to the scene
                        //            GameObject gameObj = Resources.Load(resourcePath, typeof(GameObject)) as GameObject;
                        //            Transform gameObjTransform = gameObj.GetComponentsInChildren<Transform>(true)[0];
                        //            GameObject gameObjInstance = (GameObject)GameObject.Instantiate(gameObj, position, gameObjTransform.rotation);
                        //            gameObjInstance.transform.localScale = new Vector3(scale * 0.3333f, 1, 1);

                        //            /* Set parent object */

                        //            // Check for parent tag
                        //            if (obj.parentTag != null)
                        //            {
                        //                GameObject parentWithTag = GameObject.FindGameObjectWithTag(obj.parentTag);
                        //                if (parentWithTag != null)
                        //                {
                        //                    gameObjInstance.transform.parent = parentWithTag.transform;
                        //                }
                        //            }

                        //            // Check for parent name
                        //            else if (obj.parentName != null)
                        //            {
                        //                GameObject parentWithName = GameObject.Find(obj.parentName);
                        //                if (parentWithName != null)
                        //                {
                        //                    gameObjInstance.transform.parent = parentWithName.transform;
                        //                }
                        //            }

                        //            // Is there a Game Objects folder?
                        //            else if (GameObject.Find("Game Objects") != null)
                        //            {
                        //                gameObjInstance.transform.parent = GameObject.Find("Game Objects").transform;
                        //            }

                        //            // Set previous position, unless position was absolute
                        //            if (!isAbsolute)
                        //            {
                        //                previousPosition = position;
                        //            }
                        //        }
                        //    }
                        //}

                        else if (machineName == "utr")
                        {
                            ISceneObject obj4;
                            if (sceneObjects.TryGetValue(machineName, out obj4))
                            {
                                Vector3 position = Vector3.zero;
                                Vector3 size = Vector3.zero;

                                int w = 0;
                                float posXsplit = x;
                                float posYsplit = y;
                                float posZsplit = z;

                                while (w != scale)
                                {
                                    position = new Vector3(posXsplit, levelY.positionY, posZsplit);

                                    string resourcePath = obj4.resourcePath;

                                    // Now load the game object and add to the scene
                                    GameObject gameObj = Resources.Load(resourcePath, typeof(GameObject)) as GameObject;
                                    Transform gameObjTransform = gameObj.GetComponentsInChildren<Transform>(true)[0];
                                    GameObject gameObjInstance = (GameObject)PrefabUtility.InstantiatePrefab(gameObj);
                                    gameObjInstance.transform.position = position;
                                    gameObjInstance.transform.rotation = gameObjTransform.rotation;
                                    gameObj.name = "Utr" + w;
                                    gameObjInstance.transform.localScale = new Vector3(1, 1, 1);
                                    w++;
                                    posXsplit = posXsplit + 1;
                                }
                            }
                        }
                        else if ((machineName == "small_intron") || (machineName == "medium_intron") || (machineName == "large_intron") || (machineName == "huge_intron") || (machineName == "big_intron"))
                        {
                            ISceneObject obj3;
                            if (sceneObjects.TryGetValue(machineName, out obj3))
                            {
                                Vector3 position1 = Vector3.zero;
                                Vector3 position2 = Vector3.zero;
                                Vector3 position3 = Vector3.zero;
                                Vector3 position4 = Vector3.zero;
                                Vector3 position5 = Vector3.zero;
                                Vector3 position6 = Vector3.zero;
                                Vector3 position7 = Vector3.zero;
                                Vector3 position8 = Vector3.zero;
                                Vector3 size = Vector3.zero;

                                position1 = new Vector3(x-0.1f, levelY.positionY+1, z);
                                position7 = new Vector3(x - 0.1f, levelY.positionY + 0.85f, z);
                                if (machineName == "small_intron")
                                {
                                    position2 = new Vector3((x + scale - 0.4f) + 0.365f, levelY.positionY, z);
                                    position4 = new Vector3(x + 0.5f, levelY.positionY + 0.5f, z);
                                    position5 = new Vector3(x + 1f, levelY.positionY+1, z);
                                    position8 = new Vector3(x + 1f, levelY.positionY + 0.85f, z);
                                    GameObject gameObj3 = Resources.Load("Small_Intron", typeof(GameObject)) as GameObject;
                                    Transform gameObjTransform3 = gameObj3.GetComponentsInChildren<Transform>(true)[0];
                                    GameObject gameObjInstance3 = (GameObject)PrefabUtility.InstantiatePrefab(gameObj3);
                                    gameObjInstance3.transform.position = position4;
                                    gameObjInstance3.transform.rotation = gameObjTransform3.rotation;
                                }
                                if (machineName == "medium_intron")
                                {
                                    position2 = new Vector3((x + scale), levelY.positionY, z);
                                    position3 = new Vector3(x - 1, levelY.positionY + 0.5f + 0.12f, z);
                                    position4 = new Vector3(x + 2.65f, levelY.positionY, z);
                                    position5 = new Vector3(x + 5f, levelY.positionY+1, z);
                                    position8 = new Vector3(x + 5f, levelY.positionY + 0.85f, z);
                                    GameObject gameObj3 = Resources.Load("SRProteineMedium", typeof(GameObject)) as GameObject;
                                    Transform gameObjTransform3 = gameObj3.GetComponentsInChildren<Transform>(true)[0];
                                    //GameObject gameObjInstance3 = (GameObject)PrefabUtility.InstantiatePrefab(gameObj3);
                                    //gameObjInstance3.transform.position = position3;
                                    //gameObjInstance3.transform.rotation = gameObjTransform3.rotation;
                                    GameObject gameObjInstance3 = Instantiate(gameObj3, position3, gameObjTransform3.rotation);
                                    gameObjInstance3.transform.localScale = new Vector3(0.1f, 0.1f, 1);

                                    PathData srPath = new PathData();
                                    srPath.pathName = "SRPath"; 
                                    srPath.lineType = PathLineType.CatmullRomCurve;

                                    srPath.linePoints = new PathData().linePoints;
                                    srPath.linePoints.Add(position3);
                                    srPath.linePoints.Add(new Vector3((((position8.x - position3.x) / 2) + position3.x), (position3.y + 1.5f), position3.z));
                                    srPath.linePoints.Add(new Vector3((position8.x + 0.75f), position8.y, position8.z));

                                    srPath.points = new PathData().points;
                                    srPath.points.Add(position3);
                                    srPath.points.Add(new Vector3((((position8.x - position3.x) / 2) + position3.x), (position3.y + 1.5f), position3.z));
                                    srPath.points.Add(new Vector3((position8.x + 0.75f), position8.y, position8.z));
                                    gameObjInstance3.transform.GetChild(0).GetComponent<WaypointManager>().pathList.Add(srPath);

                                    GameObject gameObj4 = Resources.Load("Medium_Intron", typeof(GameObject)) as GameObject;
                                    Transform gameObjTransform4 = gameObj4.GetComponentsInChildren<Transform>(true)[0];
                                    GameObject gameObjInstance4 = (GameObject)PrefabUtility.InstantiatePrefab(gameObj4);
                                    gameObjInstance4.transform.position = position4;
                                    gameObjInstance4.transform.rotation = gameObjTransform4.rotation;

                                }
                                if (machineName == "large_intron")
                                {
                                    position2 = new Vector3((x + scale), levelY.positionY, z);
                                    position3 = new Vector3(x - 1, levelY.positionY + 0.5f + 0.12f, z);
                                    position4 = new Vector3(x + 5.15f, levelY.positionY + 0.54f, z);
                                    position5 = new Vector3(x + 10f, levelY.positionY+1, z);
                                    position8 = new Vector3(x + 10f, levelY.positionY + 0.85f, z);
                                    GameObject gameObj4 = Resources.Load("Large_Intron", typeof(GameObject)) as GameObject;
                                    Transform gameObjTransform4 = gameObj4.GetComponentsInChildren<Transform>(true)[0];
                                    GameObject gameObjInstance4 = (GameObject)PrefabUtility.InstantiatePrefab(gameObj4);
                                    gameObjInstance4.transform.position = position4;
                                    gameObjInstance4.transform.rotation = gameObjTransform4.rotation;

                                    GameObject gameObj3 = Resources.Load("SRProteineLarge", typeof(GameObject)) as GameObject;
                                    Transform gameObjTransform3 = gameObj3.GetComponentsInChildren<Transform>(true)[0];
                                    GameObject gameObjInstance3 = Instantiate(gameObj3, position3, gameObjTransform3.rotation);
                                    //gameObjInstance3.transform.position = position3;
                                    //gameObjInstance3.transform.rotation = gameObjTransform3.rotation;
                                    //gameObjInstance3.transform.localScale = new Vector3(0.1f, 0.1f, 1);

                                    PathData srPath = new PathData();
                                    srPath.pathName = "SRPath";
                                    srPath.lineType = PathLineType.CatmullRomCurve;

                                    srPath.linePoints.Add(position3);
                                    srPath.linePoints.Add(new Vector3((((position8.x - position3.x) / 2) + position3.x), (position3.y + 1.5f), position3.z));
                                    srPath.linePoints.Add(new Vector3((position8.x + 0.75f), position8.y, position8.z));

                                    srPath.points.Add(position3);
                                    srPath.points.Add(new Vector3((((position8.x - position3.x) / 2) + position3.x), (position3.y + 1.5f), position3.z));
                                    srPath.points.Add(new Vector3((position8.x + 0.75f), position8.y, position8.z));
                                    gameObjInstance3.transform.GetChild(0).GetComponent<WaypointManager>().pathList.Add(srPath);

                                }
                                if (machineName == "big_intron")
                                {
                                    position2 = new Vector3((x + scale), levelY.positionY, z);
                                    position3 = new Vector3(x - 1, levelY.positionY + 0.5f, z);
                                    position4 = new Vector3(x + 10, levelY.positionY + 0.5f, z);
                                    position5 = new Vector3(x + 20f, levelY.positionY+1, z);
                                    position6 = new Vector3(x + 1.5f, levelY.positionY + 0.5f, z);
                                    position8 = new Vector3(x + 20f, levelY.positionY + 0.85f, z);
                                    GameObject gameObj4 = Resources.Load("Big_Intron", typeof(GameObject)) as GameObject;
                                    Transform gameObjTransform4 = gameObj4.GetComponentsInChildren<Transform>(true)[0];
                                    GameObject gameObjInstance4 = (GameObject)PrefabUtility.InstantiatePrefab(gameObj4);
                                    gameObjInstance4.transform.position = position4;
                                    gameObjInstance4.transform.rotation = gameObjTransform4.rotation;

                                    GameObject spliceosome = Resources.Load("Spliceosome", typeof(GameObject)) as GameObject;
                                    Transform spliceosomeTransform = spliceosome.GetComponentsInChildren<Transform>(true)[0];
                                    GameObject spliceosomeInstance = (GameObject)PrefabUtility.InstantiatePrefab(spliceosome);
                                    spliceosomeInstance.transform.position = position6;
                                    spliceosomeInstance.transform.rotation = spliceosomeTransform.rotation;
                                }
                                if (machineName == "huge_intron")
                                {
                                    position2 = new Vector3((x + scale), levelY.positionY, z);
                                    position3 = new Vector3(x - 1, levelY.positionY + 0.5f, z);
                                    position4 = new Vector3(x + 20.2f, levelY.positionY, z);
                                    position5 = new Vector3(x + 50f, levelY.positionY+1, z);
                                    position6 = new Vector3(x + 1.5f, levelY.positionY + 0.5f, z);
                                    position8 = new Vector3(x + 30f, levelY.positionY + 0.85f, z);
                                    GameObject gameObj4 = Resources.Load("Huge_Intron", typeof(GameObject)) as GameObject;
                                    Transform gameObjTransform4 = gameObj4.GetComponentsInChildren<Transform>(true)[0];
                                    GameObject gameObjInstance4 = (GameObject)PrefabUtility.InstantiatePrefab(gameObj4);
                                    gameObjInstance4.transform.position = position4;
                                    gameObjInstance4.transform.rotation = gameObjTransform4.rotation;

                                    GameObject spliceosome = Resources.Load("Spliceosome1", typeof(GameObject)) as GameObject;
                                    Transform spliceosomeTransform = spliceosome.GetComponentsInChildren<Transform>(true)[0];
                                    GameObject spliceosomeInstance = (GameObject)PrefabUtility.InstantiatePrefab(spliceosome);
                                    spliceosomeInstance.transform.position = position6;
                                    spliceosomeInstance.transform.rotation = spliceosomeTransform.rotation;

                                }

                                // Now load the game object and add to the scene
                                GameObject gameObj1 = Resources.Load("Intron Flag", typeof(GameObject)) as GameObject;
                                Transform gameObjTransform1 = gameObj1.GetComponentsInChildren<Transform>(true)[0];
                                GameObject gameObjInstance1 = (GameObject)PrefabUtility.InstantiatePrefab(gameObj1);
                                gameObjInstance1.transform.position = position7;
                                gameObjInstance1.transform.rotation = gameObjTransform1.rotation;
                                gameObjInstance1.transform.localScale = new Vector3(0.3f, 0.3f, 1);

                                GameObject gameObj2 = Resources.Load("Exon Flag", typeof(GameObject)) as GameObject;
                                Transform gameObjTransform2 = gameObj2.GetComponentsInChildren<Transform>(true)[0];
                                GameObject gameObjInstance2 = (GameObject)PrefabUtility.InstantiatePrefab(gameObj2);
                                gameObjInstance2.transform.position = position8;
                                gameObjInstance2.transform.rotation = gameObjTransform2.rotation;
                                gameObjInstance2.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                            }
                        }

                        else if ((machineName == "start_codon"))
                        {
                            ISceneObject obj;
                            if (sceneObjects.TryGetValue(machineName, out obj))
                            {
                                Vector3 position1 = Vector3.zero;
                                Vector3 position2 = Vector3.zero;
                                Vector3 size = Vector3.zero;

                                position1 = new Vector3(x, levelY.positionY, z);
                                position2 = new Vector3(x, levelY.positionY  - 0.165f, z);

                                string resourcePath = obj.resourcePath;

                                // Now load the game object and add to the scene
                                GameObject gameObj1 = Resources.Load(resourcePath, typeof(GameObject)) as GameObject;
                                Transform gameObjTransform1 = gameObj1.GetComponentsInChildren<Transform>(true)[0];
                                GameObject gameObjInstance1 = (GameObject)PrefabUtility.InstantiatePrefab(gameObj1);
                                gameObjInstance1.transform.position = position1;
                                gameObjInstance1.transform.rotation = gameObjTransform1.rotation;
                                gameObjInstance1.transform.localScale = new Vector3(scale, 1, 1);

                                GameObject gameObj2 = Resources.Load("Start", typeof(GameObject)) as GameObject;
                                Transform gameObjTransform2 = gameObj2.GetComponentsInChildren<Transform>(true)[0];
                                GameObject gameObjInstance2 = (GameObject)PrefabUtility.InstantiatePrefab(gameObj2);
                                gameObjInstance2.transform.position = position2;
                                gameObjInstance2.transform.rotation = gameObjTransform2.rotation;
                            }
                        }

                        else if ((machineName == "stop_codon"))
                        {
                            ISceneObject obj;
                            if (sceneObjects.TryGetValue(machineName, out obj))
                            {
                                Vector3 position1 = Vector3.zero;
                                Vector3 position2 = Vector3.zero;
                                Vector3 size = Vector3.zero;

                                position1 = new Vector3(x, levelY.positionY, z);
                                position2 = new Vector3(x + 0.5f, levelY.positionY + 0.45f, z);

                                string resourcePath = obj.resourcePath;
                                string prefab1 = node["prefab"];

                                GameObject gameObj2 = Resources.Load("Stop", typeof(GameObject)) as GameObject;
                                Transform gameObjTransform2 = gameObj2.GetComponentsInChildren<Transform>(true)[0];
                                GameObject gameObjInstance2 = (GameObject)PrefabUtility.InstantiatePrefab(gameObj2);
                                gameObjInstance2.transform.position = position2;
                                gameObjInstance2.transform.rotation = gameObjTransform2.rotation;

                                if (prefab1 == "UAA")
                                {
                                    gameObjInstance2.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Stop_UAA_long");
                                    gameObjInstance2.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("bubble_stop_UAA_txt");
                                }
                                if (prefab1 == "UAG")
                                {
                                    gameObjInstance2.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Stop_UAG_long");
                                    gameObjInstance2.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("bubble_stop_UAG_txt");
                                }
                                if (prefab1 == "UGA")
                                {
                                    gameObjInstance2.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Stop_UGA_long");
                                    gameObjInstance2.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("bubble_stop_UGA_txt");
                                }
                            }
                        }

                        else if ((machineName == "snp"))
                        {
                            ISceneObject obj;
                            if (sceneObjects.TryGetValue(machineName, out obj))
                            {
                                Vector3 position1 = Vector3.zero;
                                //Vector3 position2 = Vector3.zero;
                                Vector3 position3 = Vector3.zero;
                                Vector3 size = Vector3.zero;

                                position1 = new Vector3(x, levelY.positionY, z);
                                //position2 = obj.GetPosition(x, posY + 1, z);
                                position3 = new Vector3(x + 0.125f, levelY.positionY, z);

                                string resourcePath = obj.resourcePath;
                                string prefab1 = node["prefab1"];
                                string prefab2 = node["prefab2"];

                                // Now load the game object and add to the scene

                                GameObject gameObj2 = Resources.Load("Snp", typeof(GameObject)) as GameObject;
                                Transform gameObjTransform2 = gameObj2.GetComponentsInChildren<Transform>(true)[0];
                                GameObject gameObjInstance2 = (GameObject)PrefabUtility.InstantiatePrefab(gameObj2);
                                gameObjInstance2.transform.position = position3;
                                gameObjInstance2.transform.rotation = gameObjTransform2.rotation;
                                if (prefab1 == "A")
                                {
                                    gameObjInstance2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("A-fullblock-letter");
                                }
                                if (prefab1 == "C")
                                {
                                    gameObjInstance2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("C-fullblock-letter");
                                }
                                if (prefab1 == "G")
                                {
                                    gameObjInstance2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("G-fullblock-letter");
                                }
                                if (prefab1 == "U")
                                {
                                    gameObjInstance2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("U-fullblock-letter");
                                }

                                if (prefab2 == "A")
                                {
                                    gameObjInstance2.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("A-fullblock-letter");
                                    gameObjInstance2.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("adenin");
                                }
                                if (prefab2 == "C")
                                {
                                    gameObjInstance2.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("C-fullblock-letter");
                                    gameObjInstance2.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("cytosin");
                                }
                                if (prefab2 == "G")
                                {
                                    gameObjInstance2.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("G-fullblock-letter");
                                    gameObjInstance2.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("guanin");
                                }
                                if (prefab2 == "U")
                                {
                                    gameObjInstance2.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("U-fullblock-letter");
                                    gameObjInstance2.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("uracil");
                                }
                            }
                        }


                        else
                        {
                            ISceneObject obj;
                            if (sceneObjects.TryGetValue(machineName, out obj))
                            {

                                // Grab position
                                Vector3 position = Vector3.zero;
                                Vector3 size = Vector3.zero;

                                // If absolute, then use the coordinates as they are
                                bool isAbsolute = node["isAbsolute"].AsBool;
                                if (!isAbsolute)
                                {
                                    position = new Vector3(x, levelY.positionY, z);
                                    size = new Vector3(scale, y, z);
                                }

                                // Otherwise, use position magic from scene objects
                                else
                                {
                                    position = obj.GetPosition(x, levelY.positionY, z);
                                    size = obj.GetScale(scale, 1, 1);
                                }

                                // Grab resource path
                                string resourcePath = obj.resourcePath;

                                // Now load the game object and add to the scene
                                GameObject gameObj = Resources.Load(resourcePath, typeof(GameObject)) as GameObject;
                                Transform gameObjTransform = gameObj.GetComponentsInChildren<Transform>(true)[0];
                                GameObject gameObjInstance = (GameObject)PrefabUtility.InstantiatePrefab(gameObj);
                                gameObj.transform.position = position;
                                gameObj.transform.rotation = gameObjTransform.rotation;
                                gameObjInstance.transform.localScale = new Vector3(scale, 1, 1);

                                /* Set parent object */

                                // Check for parent tag
                                if (obj.parentTag != null)
                                {
                                    GameObject parentWithTag = GameObject.FindGameObjectWithTag(obj.parentTag);
                                    if (parentWithTag != null)
                                    {
                                        gameObjInstance.transform.parent = parentWithTag.transform;
                                    }
                                }

                                // Check for parent name
                                else if (obj.parentName != null)
                                {
                                    GameObject parentWithName = GameObject.Find(obj.parentName);
                                    if (parentWithName != null)
                                    {
                                        gameObjInstance.transform.parent = parentWithName.transform;
                                    }
                                }

                                // Is there a Game Objects folder?
                                else if (GameObject.Find("Game Objects") != null)
                                {
                                    gameObjInstance.transform.parent = GameObject.Find("Game Objects").transform;
                                }

                                // Set previous position, unless position was absolute
                                if (!isAbsolute)
                                {
                                    previousPosition = position;
                                }
                            }
                        }
					}
                }
            }
		}
        // Instantiate the Go text
        GameObject goText = Resources.Load("Go-text", typeof(GameObject)) as GameObject;
        Transform goTextTransform = goText.GetComponentsInChildren<Transform>(true)[0];
        GameObject goTextInstance = (GameObject)PrefabUtility.InstantiatePrefab(goText);
        goTextInstance.transform.position = new Vector3(0.5f, 1, 0);
        goTextInstance.transform.rotation = Quaternion.identity;

        // Adjust the levelBound inside "LevelManager" , according to levelLength
        Vector3 vectorBoundsPos = new Vector3((levelY.levelLength + levelY.levelLengthLastObjScale) / 2, 0f, 0f);
        Vector3 vectorBoundsSize = new Vector3(((levelY.levelLength + levelY.levelLengthLastObjScale) / 2)*1.02f, 12f, 5f);
        GameObject.Find("LevelManager").GetComponent<LevelManager>().LevelBounds = new Bounds(vectorBoundsPos, vectorBoundsSize*2);

        // Instantiate the end Point depending on levelLength
        Vector3 endPointPos = new Vector3((levelY.levelLength + levelY.levelLengthLastObjScale - 0.5f), levelY.positionY, 0f);
        GameObject endPoint = Resources.Load("EndPoint") as GameObject;
        endPoint.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        GameObject endPointInstance = (GameObject)PrefabUtility.InstantiatePrefab(endPoint);
        endPoint.transform.position = endPointPos;
        endPoint.transform.rotation = endPoint.transform.rotation;
	}

	static public Vector3 GetPreviousPosition() {
		if(origin != null && previousPosition == Vector3.zero) {
			previousPosition = origin.transform.position;
		}
		return previousPosition;
	}


    static public Vector3 GetOriginPosition() {
		Vector3 originPosition = Vector3.zero;
		if(origin != null) {
			originPosition = origin.transform.position;
		}
		return originPosition;
	}

	static public void SetPreviousPosition(Vector3 positionIn) {
		previousPosition = positionIn;
	}

	void OnGUI() {
		if (GUILayout.Button ("ShowObjectPicker")) {
			int controlID = EditorGUIUtility.GetControlID (FocusType.Passive);
			EditorGUIUtility.ShowObjectPicker<GameObject> (null, true, "", controlID);
		}
		
		string commandName = Event.current.commandName;
		if (commandName == "ObjectSelectorUpdated") {
			AutomatedSceneBuilder.origin = (GameObject)EditorGUIUtility.GetObjectPickerObject();
			Repaint ();
		} else if (commandName == "ObjectSelectorClosed") {
			AutomatedSceneBuilder.origin = (GameObject)EditorGUIUtility.GetObjectPickerObject();
		}

		EditorGUILayout.ObjectField (AutomatedSceneBuilder.origin, typeof(GameObject), true);
	}

    /* Automatisation des jsons :
	 * Récupérations des jsons Originaux pour les convertir en LevelsJSON automatiquement
	 * @Seb
	 */

    static private void CreateLvlsJson()
	{
		
		float scale = 0;
		float startStopCodonScale = 0.75f;
		float snpScale = 0.25f;
			
		string path = EditorUtility.OpenFilePanel(
			"Open Scene Template",
			"Assets/Plugins Ext_/Automated Scene Builder/JsonOrigin","json"
		);
		
		string jsonToParse = File.ReadAllText(path);
		
		JsonOriginData jsonOriginData = JsonUtility.FromJson<JsonOriginData>(jsonToParse);
		JsonOrigin[] jsonOrigins = jsonOriginData.jsonOrigin;

		int jsonX = 0;

		foreach (var jsonOrigin in jsonOrigins)
		{

			List<LevelData> lvlData = jsonOrigin.levelData;
			string sceneObjects = "{\"sceneObjects\":[";
			float x = 0;
			foreach (var d in lvlData)
			{
				if (d.@class.Equals("start_codon") || d.@class.Equals("stop_codon"))
					scale = startStopCodonScale;
				else if (d.@class.Equals("snp"))
					scale = snpScale;
				else if (d.@class.Equals("utr"))
					scale = 3;
				else
					scale = d.normalizedLength;

				sceneObjects += "{\"machineName\": \"" + d.@class + "\", \"x\":" + x + ", \"y\":1, \"z\":0, \"scale\":" + scale;

				if (d.@class.Equals("snp"))
				{
					MutationInfo mutationInfos = jsonOrigin.mutationInfo;
					string prefab1 = CheckPrefabNucleotideChange(mutationInfos.nucleotideChange[0]);
					string prefab2 = CheckPrefabNucleotideChange(mutationInfos.nucleotideChange[1]);
					sceneObjects += ", \"prefab1\":\"" + prefab1 + "\", \"prefab2\":\"" + prefab2 + "\"";
				}
				else if (d.@class.Equals("stop_codon"))
					sceneObjects += ", \"prefab\": \"" + jsonOrigin.stopCodon + "\"";

				sceneObjects += "},";

				x = x + scale;
			}

			// suppression de la derniere virgule
			sceneObjects = sceneObjects.Substring(0, sceneObjects.Length - 1);
			sceneObjects += "]}";

			string geneFullName = jsonOrigin.geneName + " - " + jsonOrigin.geneRealLength +
								  " bases - Chr. " + jsonOrigin.chromosome;
			string snp = jsonOrigin.mutationInfo.nucleotideChange[0] + " -> " +
						 jsonOrigin.mutationInfo.nucleotideChange[1] + " (" + jsonOrigin.mutationInfo.type + ")";
			string population;
			int origins = jsonOrigin.mutationInfo.populationBackground.Count;
			int sex = jsonOrigin.mutationInfo.sex.Count;

			if (origins == 2) population = "All";
			else if (jsonOrigin.mutationInfo.populationBackground[0].Equals("caucasian"))
				population = "Causasian";
			else
				population = "Asian";

			if (sex == 2) population += "Both";
			else if (jsonOrigin.mutationInfo.sex[0].Equals("H"))
				population += "Male";
			else
				population += "Female";

			File.WriteAllText("Assets/Plugins Ext_/Automated Scene Builder/LevelsJson/" + jsonOrigin.geneName + ".json",
				sceneObjects);

			// CONSTRUCTION DU SCRIPATBLE OBJECT
			string pathAsset = "Assets/Resources/ScriptableObjects/" + jsonOrigin.geneName + ".asset";
			GenePopupCreator gene = ScriptableObject.CreateInstance<GenePopupCreator>();
			gene.geneName = jsonOrigin.geneName;
			gene.geneFullName = geneFullName;
			gene.proteinLength = jsonOrigin.proteinLength;
			gene.snp = snp;
			gene.population = population;
			gene.transcript = jsonOrigin.transcript;
			gene.snpName = jsonOrigin.mutationInfo.rsName;
			gene.chromosomeLink = jsonOrigin.externalLinks.chromosomeWalk;
			gene.nextprotLink = jsonOrigin.externalLinks.nextprot;
			gene.pubmedLink = jsonOrigin.externalLinks.pubmed;
			gene.snpediaLink = jsonOrigin.externalLinks.snpedia;
			gene.transcriptLink = jsonOrigin.externalLinks.transcriptLink;
			gene.uniprotLink = jsonOrigin.externalLinks.uniprot;
			gene.geneDescENKey = jsonOrigin.description;
			gene.objectiveTimeSeconds = 0;
			gene.score1Star = 0;
			gene.score2Stars = 0;
			gene.score3Stars = 0;
			AssetDatabase.CreateAsset(gene, pathAsset);
			AssetDatabase.SaveAssets();
			jsonX++;
			if (jsonX == jsonOrigins.Length) Debug.Log("Créations des lvls terminés avec succès");
		}
	}

	static private string CheckPrefabNucleotideChange(string nucleotide)
	{
		string prefab = "";
		switch (nucleotide)
		{
			case "-" : prefab = "DEL";
				break;
			case "T" : prefab = "U";
				break;
			case "TA" : prefab = "Multi";
				break;
			default: prefab = nucleotide;
				break;
		}

		return prefab;
	}
}

#endif