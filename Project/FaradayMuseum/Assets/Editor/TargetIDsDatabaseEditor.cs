using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

[CustomEditor(typeof(TargetIDsDatabase))]
public class TargetIDsDatabaseEditor : Editor
{
    private TargetIDsDatabase targetIDsDatabase;

    private void OnEnable()
    {
        targetIDsDatabase = target as TargetIDsDatabase;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Generate Enum", GUILayout.Height(30)))
        {
            GenerateEnum();
        }
    }

    private void GenerateEnum(){

        string filePath = Path.Combine(Application.dataPath, "Scripts/TargetID/TargetIDs.cs");

        string code = "public enum TargetIDs {";

        foreach(TargetID targetID in targetIDsDatabase.targetIDs)
        {
            //Todo validate id is proper format
            code += targetID.artifactID + ",";
        }
        code += "}";
        File.WriteAllText(filePath, code);
        AssetDatabase.ImportAsset("Assets/Scripts/TargetID/TargetIDs.cs");
    }
}
