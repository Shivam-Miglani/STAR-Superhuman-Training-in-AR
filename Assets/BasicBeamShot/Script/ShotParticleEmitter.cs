using UnityEngine;
using System.Collections;

public class ShotParticleEmitter : MonoBehaviour {

	public GameObject ShotParticle;
	public float ShotPower;
	public float Disturbance = 0;
	private float Rld;
	public float RldTime = 2.0f;
    public Color col = new Color(1,1,1);
	// Use this for initialization
	void Start () {
		ShotPower = 0;
		Rld = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Rld -= 1.0f;
		if(Rld < 0.0f && ShotPower != 0)
		{		
			Rld = RldTime;
			float ShotPowerBuf = ShotPower;
			while(ShotPowerBuf >= 0)
			{	
				//RandX
				Quaternion q_rnd = Quaternion.AngleAxis((Random.value*Disturbance)-Disturbance*0.5f,this.transform.right);
				
				//RandZ
				q_rnd *= Quaternion.AngleAxis((Random.value*Disturbance)-Disturbance*0.5f,this.transform.up);

				GameObject pat = (GameObject)GameObject.Instantiate(ShotParticle,Vector3.zero,q_rnd);
                pat.GetComponent<LineRenderer>().SetColors(col, col);
				pat.transform.parent = this.transform.parent;
				ShotPowerBuf -= 1.0f;
			}
		}
	}
}
