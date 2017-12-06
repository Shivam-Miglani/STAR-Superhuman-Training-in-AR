using UnityEngine;
using System.Collections;

public class ShotParticle_Scale : MonoBehaviour {

	private LineRenderer LR;
	private float width;
	private float length;
	private float time;
	private Vector3 forwad;

	// Use this for initialization
	void Start () {
		LR = transform.GetComponent<LineRenderer>();
		width = 1.0f;
		length = 0.0f;
		time = 0.0f;
		forwad = transform.forward;

		Quaternion ParentQua = transform.parent.rotation;
		Vector3 V = ParentQua * forwad;

		LR.SetPosition(0,transform.parent.position);
		LR.SetPosition(1,transform.parent.position+V*transform.parent.localScale.z * length);
		LR.SetWidth(transform.parent.localScale.x * width,transform.parent.localScale.x * width);
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion ParentQua = transform.parent.rotation;
		Vector3 V = ParentQua * forwad;
		
		LR.SetPosition(0,transform.parent.position);
		LR.SetPosition(1,transform.parent.position+V*transform.parent.localScale.z * length);
		LR.SetWidth(transform.parent.localScale.x * width,transform.parent.localScale.x * width);

		width = Mathf.Lerp(width,0,time*time);
		length += 0.075f*1.5f;

		time += 0.05f;
	}
}
