using UnityEngine;

/////
using UnityEngine.UI;

using System.Collections;

public class MultiParticleSystemSpawner : MonoBehaviour 
{
	ParticleSystemSpawner[] particleSystemSpawners = null;

	public int ParticleSystemSpawnersNumber ()
	{
		return particleSystemSpawners.Length;
	}


	ParticleSystemSpawner readyParticleSystemSpawners = null;

	public string ReadyParticleSystemName ()
	{
		return readyParticleSystemSpawners.particleSystemToSpawn.name;
	}


	int readyId = 0;

	public int ReadyId
	{
		get
		{
			return readyId;
		}
	}

	/////
	//public bool isDemo = false;


	public enum InputToChangeBetweenParticleSystems
	{
		mouseScrollWheel,

		upDownArrows,

		gui
	}

	public InputToChangeBetweenParticleSystems inputToChangeBetweenParticleSystems;

	float guiInput = 0f;


	/////
	//public Text uiActiveParticleSystemName = null;


	// Use this for initialization
	void Start () 
	{
		PopulateParticleSystemSpawners ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateReadyParticleSystemSpawners ();
	}

	void LateUpdate ()
	{
		if (guiInput != 0f)
			guiInput = 0f;
	}



	void PopulateParticleSystemSpawners ()
	{
		particleSystemSpawners = GetComponentsInChildren<ParticleSystemSpawner> (false);

		for (int i = 0; i < particleSystemSpawners.Length; i++) 
		{
			particleSystemSpawners [i].gameObject.SetActive (false);
		}

		readyParticleSystemSpawners = particleSystemSpawners [readyId];

		readyParticleSystemSpawners.gameObject.SetActive (true);

		/////
		/*if(isDemo)
		{
			readyParticleSystemSpawners.SpawnThat (readyParticleSystemSpawners.particleSystemToSpawn);

			/////
			uiActiveParticleSystemName.text = readyParticleSystemSpawners.particleSystemToSpawn.name;
		}*/
	}

	void UpdateReadyParticleSystemSpawners ()
	{
		float mouseWheel = Input.GetAxis ("Mouse ScrollWheel");


		float upDownArrows = 0f;

		if (Input.GetKeyDown (KeyCode.UpArrow))
			upDownArrows = 1f;
		else if (Input.GetKeyDown (KeyCode.DownArrow))
			upDownArrows = -1f;




		float inp = 0f;

		switch (inputToChangeBetweenParticleSystems) 
		{
		case InputToChangeBetweenParticleSystems.upDownArrows:
			inp = upDownArrows;
			break;

		case InputToChangeBetweenParticleSystems.mouseScrollWheel:
			inp = mouseWheel;
			break;

		case InputToChangeBetweenParticleSystems.gui:
			inp = guiInput;
			break;
		}


		/////
		/*if(isDemo)
		{
			if (Frequence (2.1f))
				inp = -1f;
		}*/


		if (inp == 0f)
			return;

		if (inp < 0f) 
		{
			AugmentReadyId ();
		} 
		else if (inp > 0f) 
		{
			DiminishReadyId ();
		}

		readyParticleSystemSpawners.gameObject.SetActive (false);

		readyParticleSystemSpawners = particleSystemSpawners [readyId];

		readyParticleSystemSpawners.gameObject.SetActive (true);

		/////
		/*if(isDemo)
		{
			readyParticleSystemSpawners.SpawnThat (readyParticleSystemSpawners.particleSystemToSpawn);

			/////
			uiActiveParticleSystemName.text = readyParticleSystemSpawners.particleSystemToSpawn.name;
		}*/
	}

	void AugmentReadyId ()
	{
		if (readyId == particleSystemSpawners.Length - 1) 
		{
			readyId = 0;

			return;
		}

		readyId++;
	}

	void DiminishReadyId ()
	{
		if (readyId == 0)
		{
			readyId = particleSystemSpawners.Length - 1;

			return;
		}

		readyId--;
	}


	//Buttons Input Events
	public void UpperParticleSystem ()
	{
		guiInput = 1f;
	}

	public void LowerParticleSystem ()
	{
		guiInput = -1f;
	}


	float timeForFrequence = 0f;

	bool Frequence (float period)
	{
		bool re = false;

		timeForFrequence += Time.deltaTime;

		if (timeForFrequence > period) 
		{
			re = true;

			timeForFrequence = 0f;
		}
		else
		{
			re = false;
		}

		return re;
	}
}
