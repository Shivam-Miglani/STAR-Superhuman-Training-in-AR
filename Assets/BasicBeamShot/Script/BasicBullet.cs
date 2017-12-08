using UnityEngine;
using System.Collections;

public class BasicBullet : MonoBehaviour {

	private Vector3 Vec = new Vector3(0,0,0.00005f);

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().AddForce(Vec);
	}
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag != "Bullet")
		{
			Destroy(this.gameObject);
		}
	}
	// Update is called once per frame
	void Update () {


	}
}
