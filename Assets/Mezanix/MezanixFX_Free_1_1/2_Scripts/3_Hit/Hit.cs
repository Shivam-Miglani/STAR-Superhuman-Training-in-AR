using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour 
{
	[Header("Spawn Sound:")]
	public bool haveSound = false;

	public AudioClip spawnSound = null;

	AudioSource myAudioSource = null;

	// Use this for initialization
	void Start () 
	{
		AddAudioSourceAndPlaySoundOneShot ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void AddAudioSourceAndPlaySoundOneShot ()
	{
		if( ! haveSound)
			return;

		AddAudioSource ();

		PlaySoundOneShot (spawnSound);
	}

	void AddAudioSource ()
	{
		if(spawnSound != null)
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
}
