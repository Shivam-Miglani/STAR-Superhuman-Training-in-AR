using UnityEngine;
using System.Collections;

public class GetColor_BeamLine : MonoBehaviour {

	// Use this for initialization
	void Start () {
		BeamParam Parent = this.transform.root.gameObject.GetComponent<BeamParam>();
		if(Parent == null) return;
		BeamLine BL = this.gameObject.GetComponent<BeamLine>();
		BL.BeamColor = Parent.BeamColor;
		
		BL.StartSize = Parent.Scale*0.5f;
		BL.AnimationSpd = Parent.AnimationSpd;
		BL.MaxLength = Parent.MaxLength;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
