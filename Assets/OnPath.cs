using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnPath : MonoBehaviour {
	
	public static bool onPath = true;
	public Text textObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Physics.Raycast (transform.position, transform.InverseTransformDirection(Vector3.down), 20)) {
			if (!onPath) {
				Debug.Log ("On path");
			}
			onPath = true;
		}else{
			if (onPath) {
				Debug.Log ("Not on path");
			}
			onPath = false;
		}
		this.textObject.text = string.Format("Raycast: {0:G2}", onPath);
	}
}
