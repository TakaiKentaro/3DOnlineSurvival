using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CraftingSystem))]
public class MyWindow : EditorWindow
{
    [MenuItem("Window/CraftRecipeGenerator")]
	static void Open()
	{
		var window = GetWindow<MyWindow>();
		window.titleContent = new GUIContent("CraftRecipeGenerator");
	}

	void OnGUI()
	{
		EditorGUILayout.LabelField( "エディタ拡張はじめました" );
		
		EditorGUILayout.BeginVertical(GUI.skin.box);
		{
			EditorGUILayout.LabelField( "SelectGridItem" );
			
			EditorGUILayout.BeginHorizontal( GUI.skin.box );
			{
				if( GUILayout.Button("Grid_0_4" ) ){Debug.Log( "Grid_0_4" );}
				if( GUILayout.Button("Grid_1_4" ) ){Debug.Log( "Grid_1_4" );}
				if( GUILayout.Button("Grid_2_4" ) ){Debug.Log( "Grid_2_4" );}
				if( GUILayout.Button("Grid_3_4" ) ){Debug.Log( "Grid_3_4" );}
				if( GUILayout.Button("Grid_4_4" ) ){Debug.Log( "Grid_4_4" );}
			}
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal( GUI.skin.box );
			{
				if( GUILayout.Button("Grid_0_3" ) ){Debug.Log( "Grid_0_3" );}
				if( GUILayout.Button("Grid_1_3" ) ){Debug.Log( "Grid_1_3" );}
				if( GUILayout.Button("Grid_2_3" ) ){Debug.Log( "Grid_2_3" );}
				if( GUILayout.Button("Grid_3_3" ) ){Debug.Log( "Grid_3_3" );}
				if( GUILayout.Button("Grid_4_3" ) ){Debug.Log( "Grid_4_3" );}
			}
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal( GUI.skin.box );
			{
				if( GUILayout.Button("Grid_0_2" ) ){Debug.Log( "Grid_0_2" );}
				if( GUILayout.Button("Grid_1_2" ) ){Debug.Log( "Grid_1_2" );}
				if( GUILayout.Button("Grid_2_2" ) ){Debug.Log( "Grid_2_2" );}
				if( GUILayout.Button("Grid_3_2" ) ){Debug.Log( "Grid_3_2" );}
				if( GUILayout.Button("Grid_4_2" ) ){Debug.Log( "Grid_4_2" );}
			}
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal( GUI.skin.box );
			{
				if( GUILayout.Button("Grid_0_1" ) ){Debug.Log( "Grid_0_1" );}
				if( GUILayout.Button("Grid_1_1" ) ){Debug.Log( "Grid_1_1" );}
				if( GUILayout.Button("Grid_2_1" ) ){Debug.Log( "Grid_2_1" );}
				if( GUILayout.Button("Grid_3_1" ) ){Debug.Log( "Grid_3_1" );}
				if( GUILayout.Button("Grid_4_1" ) ){Debug.Log( "Grid_4_1" );}
			}
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal( GUI.skin.box );
			{
				if( GUILayout.Button("Grid_0_0" ) ){Debug.Log( "Grid_0_0" );}
				if( GUILayout.Button("Grid_1_0" ) ){Debug.Log( "Grid_1_0" );}
				if( GUILayout.Button("Grid_2_0" ) ){Debug.Log( "Grid_2_0" );}
				if( GUILayout.Button("Grid_3_0" ) ){Debug.Log( "Grid_3_0" );}
				if( GUILayout.Button("Grid_4_0" ) ){Debug.Log( "Grid_4_0" );}
			}
			EditorGUILayout.EndHorizontal();
			
			if(GUILayout.Button("生成するアイテム",GUILayout.Width(100),GUILayout.Height(100))){Debug.Log("アイテム生成");}
			
		}
		EditorGUILayout.EndVertical();
	}
}