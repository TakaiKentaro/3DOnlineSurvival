using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyWindow : EditorWindow
{
    [MenuItem("Window/MyWindow")]
    static void Open()
    {
        var window = GetWindow<MyWindow>();
        window.titleContent = new GUIContent("オリジナルウィンドウ");
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("エディタ拡張はじめました");
    }
}
