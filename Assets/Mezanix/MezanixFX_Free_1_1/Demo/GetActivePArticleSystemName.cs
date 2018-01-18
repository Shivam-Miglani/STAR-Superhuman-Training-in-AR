using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetActivePArticleSystemName : MonoBehaviour 
{
	public MultiParticleSystemSpawner multiParticleSystemSpawner = null;
	
	Text textComponent = null;

	// Use this for initialization
	void Start () 
	{
		textComponent = GetComponent<Text> ();	

		if (textComponent == null)
		{
			Debug.LogError ("Add a Text component to the game object");
		}

		if (multiParticleSystemSpawner == null) 
		{
			Debug.LogError ("The field 'multiParticleSystemSpawner' is not assigned");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		int readyId = multiParticleSystemSpawner.ReadyId + 1;

		textComponent.text = readyId + " / " + multiParticleSystemSpawner.ParticleSystemSpawnersNumber() + " \n" +
			multiParticleSystemSpawner.ReadyParticleSystemName() + "\n" + "\n" +
			"The totale effects number is 15, it's higher than " + multiParticleSystemSpawner.ParticleSystemSpawnersNumber() + ".\n" +
			"I Only putted " + multiParticleSystemSpawner.ParticleSystemSpawnersNumber() + " effects" + "\n" +
			"to be Spawned by Click in this Demo.";
	}
}
