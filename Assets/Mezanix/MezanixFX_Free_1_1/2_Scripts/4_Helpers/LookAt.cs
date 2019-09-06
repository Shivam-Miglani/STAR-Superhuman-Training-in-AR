using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour 
{
	public GameObject lookAt = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (lookAt == null)
			return;

		transform.LookAt (lookAt.transform.position);	
	}
}
