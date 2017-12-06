using UnityEngine;
using System.Collections;

public class GetColor_Flash : MonoBehaviour {

	// Use this for initialization
	void Start () {
		BeamParam bp = this.transform.root.gameObject.GetComponent<BeamParam>();
		SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
		sr.color = bp.BeamColor;
		Light li = this.gameObject.GetComponent<Light>();
		li.color = bp.BeamColor;
		li.range *= bp.Scale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
