using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects]
[CustomEditor(typeof(ParticleSystemSpawner))]
public class ParticleSystemSpawnerCI : Editor
{
	GUIStyle labelGuiStyle;

	public override void OnInspectorGUI ()
	{
		serializedObject.Update();

		ParticleSystemSpawner myTarget = (ParticleSystemSpawner)target;


		labelGuiStyle = new GUIStyle();

		labelGuiStyle.fontStyle = FontStyle.Bold;


		/////////////////
		EditorGUI.BeginChangeCheck();

		myTarget.particleSystemToSpawn = EditorGUILayout.ObjectField ("Particle System To Spawn",
			myTarget.particleSystemToSpawn, typeof(GameObject), false) as GameObject;

		EditorGUILayout.HelpBox(
			"'Choosen Particle System To Spawn' accept only project prefabs. " +
			"After modifying this field " +
			"click on the 'Apply' button (if it exist at the top right of this inspector), " +
			"otherwise the change is not reflercted in play mode.", MessageType.Info, true);


		if(myTarget.particleSystemToSpawn != null)
			myTarget.gameObject.name = "Pss_" + myTarget.particleSystemToSpawn.gameObject.name;


		EditorGUILayout.PropertyField(serializedObject.FindProperty("isTouchDevice"), false);

		if( ! myTarget.isTouchDevice)
		{
			EditorGUILayout.PropertyField(serializedObject.FindProperty("keyInputToSpawn"), false);
		}
		else
		{
			EditorGUILayout.PropertyField(serializedObject.FindProperty("spawnByTouchingMe"), false);

			if( ! myTarget.spawnByTouchingMe)
			{
				EditorGUILayout.HelpBox(
					"If you choose 'false' for 'Spawn By Touching Me', " +
					"you will need to call the public function: " +
					"'ParticleSystemSpawner.SpawnThat(GameObject go)' " +
					"Somewhere in your code", MessageType.Info, true);
			}
		}


		if(EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
	}


	/*
	public override void OnInspectorGUI ()
	{
		serializedObject.Update();

		//var myTarget = target as ParticleSystemSpawnerManager;

		//ParticleSystemSpawnerManager myTarget = (ParticleSystemSpawnerManager)target;

		//EditorGUIUtility.LookLikeInspector();

		//SerializedProperty particleSystems = serializedObject.FindProperty("particleSystems");

		//SerializedProperty keyInputsToSpawn = serializedObject.FindProperty("keyInputsToSpawn");

		//SerializedProperty touchInputsToSpawn = serializedObject.FindProperty("touchInputsToSpawn");

		//EditorGUI.BeginChangeCheck();




		EditorGUILayout.BeginHorizontal();

		EditorGUILayout.PropertyField(particleSystems, true);

		EditorGUILayout.PropertyField(keyInputsToSpawn, true);

		EditorGUILayout.EndHorizontal();


		for(int i = 0; i < myTarget.particleSystems.Length; i++)
		{
			myTarget.particleSystems[i] = EditorGUILayout.ObjectField(myTarget.particleSystems[i], typeof(GameObject), false) as GameObject;
		}


		if(EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
		

		//EditorGUIUtility.LookLikeControls();
	}
	*/
}

