using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CraftingSystem))]
public class MyWindow : EditorWindow
{
    private CraftingSystem _target;
    
    [MenuItem("Window/MyWindow")]
    static void Open()
    {
        var window = GetWindow<MyWindow>();
        window.titleContent = new GUIContent("オリジナルウィンドウ");
    }

     private void OnInspectorGUI()
     {
         //var RecepiDic = _target._recipeDictionary;
    
         //RecepiDic = EditorGUILayout.Field("RecepiDic", RecepiDic);
         
         EditorGUILayout.BeginVertical("Box");
         EditorGUILayout.LabelField("エディタ拡張はじめました");
         EditorGUILayout.EndVertical();
     }
}