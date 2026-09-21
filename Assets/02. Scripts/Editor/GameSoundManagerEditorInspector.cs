using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameSoundManager))]
public class GameSoundManagerEditorInspector : Editor
{
    //public override void OnInspectorGUI()
    //{
    //    //base.OnInspectorGUI();

    //    serializedObject.Update();
    //    GameSoundManager controll = target as GameSoundManager;

    //    ///DoorSound
    //    EditorGUILayout.BeginHorizontal();
    //    SerializedProperty DoorSound = serializedObject.FindProperty("DoorSound");
    //    EditorGUI.BeginChangeCheck();
    //    EditorGUILayout.PropertyField(DoorSound, true);

    //    if (EditorGUI.EndChangeCheck())
    //    {
    //        serializedObject.ApplyModifiedProperties();
    //    }



    //    EditorGUILayout.EndHorizontal();

    //    //ObjectSound
    //    SerializedProperty ObjectSound = serializedObject.FindProperty("ObjectSound");
    //    EditorGUI.BeginChangeCheck();
    //    EditorGUILayout.PropertyField(ObjectSound, true);

    //    if (EditorGUI.EndChangeCheck())
    //    {
    //        serializedObject.ApplyModifiedProperties();
    //    }

    //    ///TriggerSound
    //    SerializedProperty TriggerSound = serializedObject.FindProperty("TriggerSound");
    //    EditorGUI.BeginChangeCheck();

    //    for(int i = 0; i< controll.TriggerSound.Length;i++)
    //    {

    //    }
    //    EditorGUILayout.PropertyField(TriggerSound, true);
    //    if(EditorGUI.EndChangeCheck())
    //    {
    //        serializedObject.ApplyModifiedProperties();
    //    }
    //}
}
