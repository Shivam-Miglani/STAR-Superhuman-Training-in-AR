using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		BeamParam bp = GameObject.Find("Shooter").GetComponent<BeamParam>();

		bp.BeamColor.r = 1.1f;
		bp.BeamColor.g = 0.1f;
		bp.BeamColor.b = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		BeamParam bp = GameObject.Find("Shooter").GetComponent<BeamParam>();

		//Anm
		GameObject Anm = GameObject.Find("AnmSlider");
		Anm.GetComponent<Slider>().onValueChanged.AddListener((value) => {
			bp.AnimationSpd = value;
		});

		//Scale
		GameObject Sca = GameObject.Find("ScaleSlider");
		Sca.GetComponent<Slider>().onValueChanged.AddListener((value) => {
			bp.Scale = value;
		});

		//Length
		GameObject Len = GameObject.Find("LengthSlider");
		Len.GetComponent<Slider>().onValueChanged.AddListener((value) => {
			bp.MaxLength = value;
		});

		//Color
		GameObject Col = GameObject.Find("ColorSlider");
		float Add = (2*Mathf.PI)/3.0f;
		Col.GetComponent<Slider>().onValueChanged.AddListener((value) => {
			bp.BeamColor.r = Mathf.Max(0,Mathf.Cos(value*2*Mathf.PI))+0.05f;
			bp.BeamColor.g = Mathf.Max(0,Mathf.Cos(value*2*Mathf.PI+Add*1))+0.05f;
			bp.BeamColor.b = Mathf.Max(0,Mathf.Cos(value*2*Mathf.PI+Add*2))+0.05f;
		});
		/*
		//Camera
		GameObject Cam = GameObject.Find("CameraSlider");
		//GameObject CamOwner = Camera.main.transform.root.gameObject;
		GameObject CamOwner = GameObject.Find("CameraOwner");

		Cam.GetComponent<Slider>().onValueChanged.AddListener((value) => {
			CamOwner.transform.rotation = Quaternion.AngleAxis((1.0f-value) * -150.0f,Vector3.up);
			//CamOwner.transform.localEulerAngles.Set(0,value * -150.0f,0);
		});
		*/
	}
}
