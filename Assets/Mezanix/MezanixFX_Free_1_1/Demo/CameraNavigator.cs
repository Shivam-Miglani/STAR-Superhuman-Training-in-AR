using UnityEngine;
using System.Collections;

public class CameraNavigator : MonoBehaviour 
{
	public Vector3 rotationSpeeds = Vector3.one * 100f;

	public Vector3 translationSpeeds = Vector3.one * 10f;

	Vector3 initPosition = Vector3.one;

	Quaternion initRotation = Quaternion.identity;

	// Use this for initialization
	void Start () 
	{
		initPosition = transform.position;

		initRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Move ();

		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
	}

	void Move ()
	{
		if (Input.GetKeyDown (KeyCode.O)) 
		{
			transform.position = initPosition;

			transform.rotation = initRotation;
		}

		if ( ! (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) ) 
		{
			if (Input.GetKey (KeyCode.W)) 
			{
				transform.Translate (Vector3.forward * translationSpeeds.z * Time.deltaTime);
			} 
			else if (Input.GetKey (KeyCode.S)) 
			{
				transform.Translate (-Vector3.forward * translationSpeeds.z * Time.deltaTime);
			}

			if (Input.GetKey (KeyCode.A)) 
			{
				transform.Translate (-Vector3.right * translationSpeeds.x * Time.deltaTime);
			} 
			else if (Input.GetKey (KeyCode.D)) 
			{
				transform.Translate (Vector3.right * translationSpeeds.x * Time.deltaTime);
			}

			if (Input.GetKey (KeyCode.Q))
			{
				transform.Translate (Vector3.up * translationSpeeds.y * Time.deltaTime);
			}
			else if (Input.GetKey (KeyCode.E)) 
			{
				transform.Translate (-Vector3.up * translationSpeeds.y * Time.deltaTime);
			}
		}
		else if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) 
		{
			if (Input.GetKey (KeyCode.W)) 
			{
				transform.Rotate (Vector3.right * translationSpeeds.x * Time.deltaTime);
			} 
			else if (Input.GetKey (KeyCode.S)) 
			{
				transform.Rotate (-Vector3.right * translationSpeeds.x * Time.deltaTime);
			}

			if (Input.GetKey (KeyCode.A))
			{
				transform.Rotate (-Vector3.up * translationSpeeds.y * Time.deltaTime);
			}
			else if (Input.GetKey (KeyCode.D)) 
			{
				transform.Rotate (Vector3.up * translationSpeeds.y * Time.deltaTime);
			}

			if (Input.GetKey (KeyCode.Q)) 
			{
				transform.Rotate (Vector3.forward * translationSpeeds.z * Time.deltaTime);
			} 
			else if (Input.GetKey (KeyCode.E)) 
			{
				transform.Rotate (-Vector3.forward * translationSpeeds.z * Time.deltaTime);
			}
		}
	}
}
