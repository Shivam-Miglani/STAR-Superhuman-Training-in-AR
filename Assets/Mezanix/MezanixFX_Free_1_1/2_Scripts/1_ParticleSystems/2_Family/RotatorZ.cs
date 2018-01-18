using UnityEngine;
using System.Collections;

public class RotatorZ : MonoBehaviour 
{
	[Range(-720f, 720f)]
	public float rotationVelocityZ = 100f;

	void Update ()
	{
		Move ();
	}

	void Move ()
	{
		transform.Rotate(Vector3.forward, rotationVelocityZ * Time.deltaTime);
	}
}
