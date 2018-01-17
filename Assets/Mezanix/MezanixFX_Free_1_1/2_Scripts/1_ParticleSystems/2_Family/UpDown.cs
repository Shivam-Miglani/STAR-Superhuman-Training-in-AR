using UnityEngine;
using System.Collections;

public class UpDown : MonoBehaviour 
{
	public float minY = 0f;

	public float maxY = 5f;

	public float velocity = 100f;


	float halrfAmp = 5f;

	float center = 2.5f;

	float alpha = 0f;

	// Use this for initialization
	void Start () 
	{
		halrfAmp = 0.5f*(maxY - minY);

		center = 0.5f*(maxY + minY);
	}
	
	// Update is called once per frame
	void Update () 
	{
		alpha += Mathf.Deg2Rad*velocity*Time.deltaTime;

		if(alpha >= 2f*Mathf.PI)
			alpha = 0f;

		float y = center + halrfAmp*Mathf.Sin(alpha);

		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}
}
