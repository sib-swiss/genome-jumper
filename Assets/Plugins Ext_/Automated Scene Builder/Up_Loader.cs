/* * * * *
 * Automated Scene Builder Up Object
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

public class Up_Loader : ISceneObject
{
    public string machineName { get { return "up"; } }
    public string tag { get { return "Up"; } }
    public string resourcePath { get { return "Up"; } }
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