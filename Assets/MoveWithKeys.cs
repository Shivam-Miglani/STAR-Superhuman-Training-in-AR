using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithKeys : MonoBehaviour {

	public float speed = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.RightArrow)) {
			transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
		}
		if(Input.GetKey(KeyCode.LeftArrow)) {
			transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
		}
		if(Input.GetKey(KeyCode.DownArrow)) {
			transform.position = new Vector3(transform.position.x ,transform.position.y, transform.position.z - speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.UpArrow)) {
			transform.position = new Vector3(transform.position.x ,transform.position.y, transform.position.z + speed * Time.deltaTime);
		}
	}
}
