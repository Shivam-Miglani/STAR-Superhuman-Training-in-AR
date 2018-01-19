using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour 
{
	public GameObject test = null;

	public Touch touch;

	// Use this for initialization
	void Start () 
	{
		//touch = Input.tou	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.DrawRay(transform.position, transform.forward);	
	}
}
