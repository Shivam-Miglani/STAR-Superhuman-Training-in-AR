using UnityEngine;
using System.Collections;

public class BeamParam : MonoBehaviour {
	
	public Color BeamColor = Color.green;
	public float AnimationSpd = 0.1f;
	public float Scale = 1.0f;
	public float MaxLength = 32.0f;
	public bool bEnd = false;
	public bool bGero = false;

	public void SetBeamParam(BeamParam param)
	{
		this.BeamColor = param.BeamColor;
		this.AnimationSpd = param.AnimationSpd;
		this.Scale = param.Scale;
		this.MaxLength = param.MaxLength;
	}

	void Start () {
		BeamParam param = this.transform.root.gameObject.GetComponent<BeamParam>();

		if(param != null)
		{
			this.BeamColor = param.BeamColor;
			this.AnimationSpd = param.AnimationSpd;
			this.Scale = param.Scale;
			this.MaxLength = param.MaxLength;
		}

	}
}
