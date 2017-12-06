using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	public float DestroyTime = 2.0f;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, DestroyTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
