using UnityEngine;
using System.Collections;

public class CombatAttack : MonoBehaviour 
{
	[Header("Space:")]
	public bool takeVelocityZFromParent = false;

	Vector3 hittedNormal = Vector3.one.normalized;

	Vector3 hittedPosition = Vector3.zero;

	[Range(0f, 200f)]
	public float velocityZ = 10f;


	[Header("Time:")]
	[Range(1f, 60f)]
	public float lifeTime = 10f;


	ParticleSystem ps = null;


	[Header("Hit:")]
	public bool haveToCollide = false;


	public bool haveToInstantiateSomthingOnHit = false;

	public GameObject toInstantiateOnHit = null;


	public bool haveToAssignTemporaryMaterialToHittedObject = false;

	public Material toAssignToHittedObject = null;

	[Range(0.5f, 60f)]
	public float toAssignToHittedObjectDuration = 2f;


	GameObject hittedGameObject = null;

	Material[] hittedGameObjectFirstMaterials = null;


	[Header("Sounds:")]
	public bool haveSpawnSound = false;

	public AudioClip spawnSound = null;


	public bool haveHitSound = false;

	public AudioClip hitSound = null;



	AudioSource myAudioSource = null;






	void Awake ()
	{
		ps = GetComponent<ParticleSystem>();
	}

	// Use this for initialization
	void Start () 
	{
		CheckElements ();

		VelocityZOrigine();


		if(haveSpawnSound && spawnSound != null)
			AddAudioSourceAndPlaySoundOneShot (spawnSound);


		AdaptLifeTimeAndDurationAccordingToVelocityZ ();

		Destroy (gameObject, lifeTime);	
	}

	// Update is called once per frame
	void Update ()
	{
		VelocityZOrigine ();

		Move ();	

		ManageCollisions ();
	}

	void AddAudioSourceAndPlaySoundOneShot (AudioClip ac)
	{
		if( ! haveSpawnSound )
			return;

		AddAudioSource ();

		PlaySoundOneShot (ac);
	}

	void AddAudioSource ()
	{
		if(haveSpawnSound && spawnSound != null )
		{
			myAudioSource = gameObject.GetComponent<AudioSource>();

			if(myAudioSource == null)
				myAudioSource = gameObject.AddComponent<AudioSource>();
		}
	}

	void PlaySoundOneShot (AudioClip ac)
	{
		if(myAudioSource == null)
			return;

		if(ac == null)
			return;

		myAudioSource.Stop ();

		myAudioSource.playOnAwake = false;

		myAudioSource.loop = false;

		myAudioSource.clip = ac;

		myAudioSource.PlayOneShot(myAudioSource.clip, 1f);
	}

	void VelocityZOrigine ()
	{
		if( ! takeVelocityZFromParent)
			return;

		CombatAttack combatAttack = transform.parent.gameObject.GetComponent<CombatAttack>();

		if(combatAttack != null)
		{
			velocityZ = combatAttack.velocityZ;
		}
		else
		{
			Debug.LogError("No Parent found or the Parent hasn't a component of type 'CombatAttack' or a componenet that inherit from the type 'CombatAttack'");
		}
	}

	virtual protected void Move ()
	{
		if(takeVelocityZFromParent)
			return;

		Vector3 moveDirection = Vector3.forward;

		transform.Translate(moveDirection * velocityZ * Time.deltaTime);
	}

	void ManageCollisions ()
	{
		if( ! haveToCollide)
			return;

		float forwardFactore = 1f;

		if(velocityZ >= 10f)
		{
			forwardFactore = velocityZ / 10f;
		}

		if( CheckCollisionForRay (new Ray(transform.position, transform.forward ), forwardFactore) ||
			CheckCollisionForRay (new Ray(transform.position, -transform.forward), 1f) ||

			CheckCollisionForRay (new Ray(transform.position, transform.up), 1f) ||
			CheckCollisionForRay (new Ray(transform.position, -transform.up), 1f) ||

			CheckCollisionForRay (new Ray(transform.position, transform.right), 1f) ||
			CheckCollisionForRay (new Ray(transform.position, -transform.right), 1f) ||


			CheckCollisionForRay (new Ray(transform.position, (transform.forward + transform.up).normalized), 1f) ||
			CheckCollisionForRay (new Ray(transform.position, (transform.forward - transform.up).normalized), 1f) ||
			CheckCollisionForRay (new Ray(transform.position, (-transform.forward + transform.up).normalized), 1f) ||
			CheckCollisionForRay (new Ray(transform.position, (-transform.forward - transform.up).normalized), 1f) ||
		
			CheckCollisionForRay (new Ray(transform.position, (transform.forward + transform.right).normalized), 1f) ||
			CheckCollisionForRay (new Ray(transform.position, (transform.forward - transform.right).normalized), 1f) ||
			CheckCollisionForRay (new Ray(transform.position, (-transform.forward + transform.right).normalized), 1f) ||
			CheckCollisionForRay (new Ray(transform.position, (-transform.forward - transform.right).normalized), 1f) ||
		
			CheckCollisionForRay (new Ray(transform.position, (transform.up + transform.right).normalized), 1f) ||
			CheckCollisionForRay (new Ray(transform.position, (transform.up - transform.right).normalized), 1f) ||
			CheckCollisionForRay (new Ray(transform.position, (-transform.up + transform.right).normalized), 1f) ||
			CheckCollisionForRay (new Ray(transform.position, (-transform.up - transform.right).normalized), 1f))
		{

			Vector3 hittedTangent = new Vector3(hittedNormal.z, 0f, -hittedNormal.x).normalized;  // y = 0 is an assumption, that means the tangent is always in the XZ plane


			Vector3 toHittedPoint = hittedPosition - transform.position;

			Vector3 toInstantiatePosition = hittedPosition - toHittedPoint.normalized;


			if(toInstantiateOnHit != null && haveToCollide && haveToInstantiateSomthingOnHit)
				Instantiate (toInstantiateOnHit, toInstantiatePosition, Quaternion.LookRotation(hittedNormal, hittedTangent));

			if (hittedGameObject != null && haveToCollide && haveToAssignTemporaryMaterialToHittedObject)
				AssignMaterialToHittedObject ();

			if (haveHitSound && hitSound != null)
				InstantiateGameObjectToPlayOneShotSound (hitSound);

			DestroyImmediate(gameObject);
		}
	}

	bool CheckCollisionForRay (Ray ray, float distance)
	{
		bool re = false;

		RaycastHit hit;

		if( Physics.Raycast (ray, out hit, distance) )
		{
			ParticleSystemSpawner particleSystemSpawner = hit.transform.gameObject.GetComponent<ParticleSystemSpawner>();

			if(particleSystemSpawner != null)
				re = false;
			else
				re = true;


			hittedNormal = hit.normal;

			hittedPosition = hit.point;

			hittedGameObject = hit.transform.gameObject;
		}

		return re;
	}

	void AssignMaterialToHittedObject ()
	{
		if (hittedGameObject == null)
			return;

		MeshRenderer mr = hittedGameObject.GetComponent<MeshRenderer> ();

		if (mr == null)
			return;

		hittedGameObjectFirstMaterials = mr.materials; 

		Material[] materialsToAssign = new Material[hittedGameObjectFirstMaterials.Length + 1];

		if (hittedGameObject.GetComponent<ReceiveMaterialChangeByHit> () == null) 
		{
			for (int i = 0; i < hittedGameObjectFirstMaterials.Length; i++)
				materialsToAssign [i] = hittedGameObjectFirstMaterials [i];

			materialsToAssign [materialsToAssign.Length - 1] = toAssignToHittedObject;


			hittedGameObject.AddComponent<ReceiveMaterialChangeByHit> ();

			hittedGameObject.GetComponent<ReceiveMaterialChangeByHit> ().changeDuration = toAssignToHittedObjectDuration;

			hittedGameObject.GetComponent<ReceiveMaterialChangeByHit> ().firstMaterials = hittedGameObjectFirstMaterials;

			hittedGameObject.GetComponent<ReceiveMaterialChangeByHit> ().toAssignMaterials = materialsToAssign;

			hittedGameObject.GetComponent<ReceiveMaterialChangeByHit> ().DoChange ();


			FreeHittedGameObject ();
		}


	}

	void FreeHittedGameObject ()
	{
		hittedGameObjectFirstMaterials = null;

		hittedGameObject = null;
	}

	void InstantiateGameObjectToPlayOneShotSound (AudioClip ac)
	{
		if (ac == null)
			return;

		GameObject gotposs = new GameObject ("GameObjectToPlayOneShotSound");

		gotposs.AddComponent<LifeTime> ();

		gotposs.GetComponent<LifeTime> ().lifeTime = ac.length + 0.5f;


		gotposs.AddComponent<AudioSource> ();

		gotposs.GetComponent<AudioSource> ().Stop ();

		gotposs.GetComponent<AudioSource> ().playOnAwake = false;

		gotposs.GetComponent<AudioSource> ().loop = false;

		gotposs.GetComponent<AudioSource> ().clip = ac;

		gotposs.GetComponent<AudioSource> ().PlayOneShot(gotposs.GetComponent<AudioSource> ().clip, 1f);
	}

	void CheckElements ()
	{
		if(haveToCollide)
		{
			if (haveToInstantiateSomthingOnHit)
			{
				if (toInstantiateOnHit == null)
					Debug.LogError (Strings.PublicFieldNotAssigned ("To Instantiate On Hit"));
			}

			if (haveToAssignTemporaryMaterialToHittedObject) 
			{
				if (toAssignToHittedObject == null)
					Debug.LogError (Strings.PublicFieldNotAssigned ("To Assign To Hitted Object"));
			}

			if (haveHitSound) 
			{
				if (hitSound == null)
					Debug.LogError (Strings.PublicFieldNotAssigned ("Hit Sound"));
			}
		}

		if (haveSpawnSound) 
		{
			if (spawnSound == null)
				Debug.LogError (Strings.PublicFieldNotAssigned ("Spawn Sound"));
		}
	}

	void AdaptLifeTimeAndDurationAccordingToVelocityZ ()
	{
		if(ps == null)
			return;

		if(ps.simulationSpace == ParticleSystemSimulationSpace.Local)
			return;
		

		float factor = 10f;

		float vzPositive = Mathf.Abs (velocityZ);

		float vzNotZero = vzPositive <= 0.1f? 0.1f: vzPositive;

		ps.startLifetime = factor / vzNotZero;
	}
}
