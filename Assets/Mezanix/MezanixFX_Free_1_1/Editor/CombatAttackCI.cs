using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects]
[CustomEditor(typeof(CombatAttack), true)]
public class CombatAttackCI : Editor
{
	public override void OnInspectorGUI ()
	{
		serializedObject.Update();


		CombatAttack myTarget = (CombatAttack)target;

		/*
		EditorGUILayout.LabelField("Space:");

		myTarget.velocityZ = EditorGUILayout.Slider("Velocity Z", myTarget.velocityZ, 0.1f, 100f);


		EditorGUILayout.LabelField("Time:");

		myTarget.lifeTime = EditorGUILayout.Slider("Life Time:", myTarget.lifeTime, 1f, 60f);
		*/

		//Space
		SerializedProperty takeVelocityZFromParent = serializedObject.FindProperty("takeVelocityZFromParent");

		SerializedProperty velocityZ = serializedObject.FindProperty("velocityZ");

		SerializedProperty rotationVelocityY = serializedObject.FindProperty("rotationVelocityY");

		SerializedProperty rotationVelocityZ = serializedObject.FindProperty("rotationVelocityZ");

		//Time
		SerializedProperty lifeTime = serializedObject.FindProperty("lifeTime");

		//SerializedProperty hit = serializedObject.FindProperty("hit");

		//Hit
		SerializedProperty haveToCollide = serializedObject.FindProperty("haveToCollide");


		//Sound
		SerializedProperty haveSpawnSound = serializedObject.FindProperty("haveSpawnSound");

		SerializedProperty spawnSound = serializedObject.FindProperty("spawnSound");


		////////////
		EditorGUI.BeginChangeCheck();

		//Space
		EditorGUILayout.PropertyField(takeVelocityZFromParent, false);

		if( ! myTarget.takeVelocityZFromParent)
			EditorGUILayout.PropertyField(velocityZ, false);

		if( rotationVelocityY != null )
			EditorGUILayout.PropertyField(rotationVelocityY, false);

		if( rotationVelocityZ != null )
			EditorGUILayout.PropertyField(rotationVelocityZ, false);

		//Time
		EditorGUILayout.PropertyField(lifeTime, false);

		//Hit
		EditorGUILayout.PropertyField(haveToCollide, false);

		if(myTarget.haveToCollide)
		{
			EditorGUILayout.PropertyField (serializedObject.FindProperty ("haveToInstantiateSomthingOnHit"));

			if (myTarget.haveToInstantiateSomthingOnHit)
			{
				myTarget.toInstantiateOnHit = EditorGUILayout.ObjectField ("To Instantiate On Hit", myTarget.toInstantiateOnHit, typeof(GameObject), false) as GameObject;

				EditorGUILayout.HelpBox (
					"'To Instantiate On Hit' accept only project prefabs. " +
					"After modifying this field " +
					"click on the 'Apply' button (if it exist at the top right of this inspector), " +
					"otherwise the change is not reflercted in play mode.", MessageType.Info, true);
			}


			EditorGUILayout.PropertyField (serializedObject.FindProperty ("haveToAssignTemporaryMaterialToHittedObject"));

			if (myTarget.haveToAssignTemporaryMaterialToHittedObject) 
			{
				EditorGUILayout.PropertyField (serializedObject.FindProperty ("toAssignToHittedObject"));

				EditorGUILayout.PropertyField (serializedObject.FindProperty ("toAssignToHittedObjectDuration"));
			}
		}


		//Sound
		EditorGUILayout.PropertyField(haveSpawnSound, false);

		if(myTarget.haveSpawnSound)
			EditorGUILayout.PropertyField(spawnSound, false);



		EditorGUILayout.Separator ();

		if (myTarget.haveToCollide) 
		{
			EditorGUILayout.PropertyField (serializedObject.FindProperty ("haveHitSound"));

			if (myTarget.haveHitSound)
				EditorGUILayout.PropertyField (serializedObject.FindProperty ("hitSound"));
		}




		if(EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
	}
}
