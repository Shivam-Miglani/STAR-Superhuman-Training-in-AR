using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects]
[CustomEditor(typeof(Hit), true)]
public class HitCI : Editor
{
	public override void OnInspectorGUI ()
	{
		serializedObject.Update();

		Hit myTarget = (Hit)target;


		SerializedProperty haveSound = serializedObject.FindProperty("haveSound");

		SerializedProperty spawnSound = serializedObject.FindProperty("spawnSound");


		////////////
		EditorGUI.BeginChangeCheck();

		EditorGUILayout.PropertyField(haveSound);

		if(myTarget.haveSound)
			EditorGUILayout.PropertyField(spawnSound);


		if(EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
	} 
}
