using UnityEngine;
using System;
using System.Collections;

public class ParticleSystemSpawner : MonoBehaviour  
{
	[Header("Spawn:")]
	public GameObject particleSystemToSpawn;

	[Header("Input:")]
	public bool isTouchDevice = false;

	public bool spawnByTouchingMe = false;

	public KeyCode keyInputToSpawn = KeyCode.Mouse1;



	void Awake ()
	{
	}

	// Use this for initialization
	void Start () 
	{
		CheckElements ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetInput ();
	}

	void GetInput ()
	{
		if(isTouchDevice)
			return;

		if(Input.GetKeyDown(keyInputToSpawn))
			SpawnThat(particleSystemToSpawn);
	}

	void OnMouseDown ()
	{
		if( ! isTouchDevice)
			return;

		if( ! spawnByTouchingMe)
			return;
		
		SpawnThat (particleSystemToSpawn);
	}

	public void SpawnThat (GameObject go)
	{
		//go.SetActive(true);

		//gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

		MonoBehaviour.Instantiate (go, transform.position, transform.rotation);
	}

	void CheckElements ()
	{
		if(particleSystemToSpawn == null)
			Debug.LogError( Strings.PublicFieldNotAssigned("Particle System To Spawn") );
	}
}
