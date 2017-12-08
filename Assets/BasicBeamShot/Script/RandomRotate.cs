using UnityEngine;
using System.Collections;

public class RandomRotate : MonoBehaviour {

	private float rot;
	private float add_rot;
	// Use this for initialization
	void Start () {
		rot = Random.value*360.0f;
		add_rot = Random.Range(360.0f*2,360.0f*10);
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate(0,0,rot);
		rot += add_rot;
	}
}
