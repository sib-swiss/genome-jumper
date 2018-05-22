/* * * * *
 * Automated Scene Builder Codon_Start Object
 * ------------------------------
 * Written by Medaille Christopher <christopher@pinterac.net>
 * Pinterac - SIB Human Jumper Project
 * 2017-12-31
 * 
 * * * * */
#if (UNITY_EDITOR)
using UnityEngine;

public class Codon_StartLoader : ISceneObject
{
    public string machineName { get { return "start_codon"; } }
    public string tag { get { return "Start Codon"; } }
    public string resourcePath { get { return "Start_Codon_base"; } }
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