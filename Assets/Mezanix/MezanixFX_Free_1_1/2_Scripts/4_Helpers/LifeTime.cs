using UnityEngine;
using System.Collections;

public class LifeTime : MonoBehaviour 
{
	[Range(0.1f, 3600f)]
	public float lifeTime = 2f;

	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject, lifeTime);	
	}
}
