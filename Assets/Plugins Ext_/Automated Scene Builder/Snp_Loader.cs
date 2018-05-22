/* * * * 
* Automated Scene Builder Snp Object
 * ------------------------------
 * Written by Medaille Christopher <christopher@pinterac.net>
 * Pinterac - SIB Human Jumper Project
 * 2017-12-31
 * 
 * * * * */
#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnpLoader : ISceneObject
{
    public string machineName { get { return "snp"; } }
    public string tag { get { return "Snp"; } }
    public string resourcePath { get { return "Snp"; } }
    public string parentTag { get { return null; } }
    public string parentName { get { return "Game Objects"; } }

    public Vector3 GetPosition(float x, float y, float z)
    {
        return new Vector3(
            AutomatedSceneBuilder.GetPreviousPosition().x + x * 1f,
            AutomatedSceneBuilder.GetOriginPosition().y + y,
            AutomatedSceneBuilder.GetPreviousPosition().z
        );
    }

    public Vector3 GetScale(float scale, float y, float z)
    {
        return new Vector3(scale, 1, 1);

    }



}
#endif