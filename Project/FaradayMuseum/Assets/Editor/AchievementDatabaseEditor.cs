using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

[CustomEditor(typeof(AchievementDatabase))]
public class AchievementDatabaseEditor : Editor
{
    private AchievementDatabase database;

    private void OnEnable()
    {
        database = target as AchievementDatabase;
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

        string filePath = Path.Combine(Application.dataPath, "Scripts/Achievements/Achievements.cs");

        string code = "public enum Achievements {";
        
        foreach(Achievement achievement in database.achievements)
        {
            //Todo validate id is proper format
            code += achievement.id + ",";
        }
        code += "}";
        File.WriteAllText(filePath, code);
        AssetDatabase.ImportAsset("Assets/Scripts/Achievements/Achievements.cs");
    }
}
