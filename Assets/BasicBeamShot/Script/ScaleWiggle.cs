using UnityEngine;
using System.Collections;

public class ScaleWiggle : MonoBehaviour {

	public float MaxWiggle = 1.0f;
	public float WiggleSpd = 0.5f;

	public Vector3 DefScale;
	private Vector3 TgtScale;
	private Vector3 PrevScale;
	private float TgtTime;

	// Use this for initialization
	void Start () {
		DefScale = transform.localScale;
		TgtScale = DefScale;
		PrevScale = DefScale;
		TgtTime = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(TgtTime >= 1.0f)
		{
			TgtTime = 0.0f;
			float wig = Random.value*MaxWiggle;
			Vector3 wig3 = DefScale * wig;

			TgtScale = DefScale+-wig3*0.5f+wig3;
			PrevScale = transform.localScale;
		}else{
			TgtTime += WiggleSpd;
		}

		transform.localScale = Vector3.Lerp(PrevScale,TgtScale,TgtTime);
	}
}
