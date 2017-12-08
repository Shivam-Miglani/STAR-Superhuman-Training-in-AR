using UnityEngine;
using System.Collections;

public class MouseTgt : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 dir= ray.direction.normalized;

		transform.LookAt(transform.position+dir*16.0f);
	}
}
