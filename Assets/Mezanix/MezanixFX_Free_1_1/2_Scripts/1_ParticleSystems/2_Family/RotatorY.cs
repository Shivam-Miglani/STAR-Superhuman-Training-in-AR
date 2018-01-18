using UnityEngine;
using System.Collections;

public class RotatorY : MonoBehaviour 
{
	[Range(-720f, 720f)]
	public float rotationVelocityY = 100f;

	void Update ()
	{
		Move ();
	}

	void Move ()
	{
		transform.Rotate(Vector3.up, rotationVelocityY * Time.deltaTime);
	}
}
