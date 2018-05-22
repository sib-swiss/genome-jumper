/* * * * *
 * Automated Scene Builder SceneObject Interface
 * ------------------------------
 * Written by Josh Robison <joshua.a.robison@gmail.com>
 * Team Robison
 * 2015-02-04
 * 
 * Modified by Medaille Christopher <christopher@pinterac.net>
 * Pinterac - SIB Human Jumper project
 * 2017-31-12
 * * * * */
#if (UNITY_EDITOR)
using UnityEngine;

public interface ISceneObject {
	// This should match the machineName in your .json import file.
	string machineName {get;}

	// The tag for referencing this object in the scene.  If left blank, the Reset function will not work for this object.
	string tag {get;}

	// The resource path for the for the gameobject / prefab you want this to load into the scene.
	// This should be the relative name inside of the Resources path since it is loaded with Resources.Load()
	string resourcePath {get;}

	// The tag of the object to place this under as its parent in the scene.  Optional
	string parentTag {get;}

	// The name of the object to place this under as its parent in the scene.  Optional
	string parentName {get;}

	/*
	 * Custom positioning based on x,y,z units.  Ignored if isAbolute was set in json file.
	 */
	Vector3 GetPosition(float x, float y, float z);

    // Custom Scaling of each objects base on x, y, z units. 
    Vector3 GetScale(float x, float y, float z);
}
#endif