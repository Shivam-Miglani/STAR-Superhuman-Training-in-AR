using UnityEngine;
using System.Collections;

public class MultiShooter : MonoBehaviour
{

    public GameObject Shot1;
    public GameObject Shot2;
    public GameObject Wave;
    public float Disturbance = 0;
    public float nextShoot = 3.0f;

    public int ShotType = 0;

    private GameObject NowShot;

    void Start()
    {
        NowShot = null;
        InvokeRepeating("Shooting", nextShoot, nextShoot);
        //Shooting();
    }

    void Shooting()
    {
        GameObject Bullet;
        //GameObject wav = (GameObject)Instantiate(Wave, this.transform.position, this.transform.rotation);
        //wav.transform.Rotate(Vector3.left, 90.0f);
        //wav.GetComponent<BeamWave>().col = this.GetComponent<BeamParam>().BeamColor;

        Bullet = Shot1;
        //Fire
        NowShot = (GameObject)Instantiate(Bullet, this.transform.position, this.transform.rotation);

        Destroy(NowShot, 1.0f);
        //Destroy(wav, 1.0f);
    }

    void Update()
    {
        /*GameObject Bullet;

        //create BasicBeamShot
        if (Input.GetButtonDown("Fire1"))
        {
            Bullet = Shot1;
            //Fire
            GameObject s1 = (GameObject)Instantiate(Bullet, this.transform.position, this.transform.rotation);
            s1.GetComponent<BeamParam>().SetBeamParam(this.GetComponent<BeamParam>());

            GameObject wav = (GameObject)Instantiate(Wave, this.transform.position, this.transform.rotation);
            wav.transform.localScale *= 0.25f;
            wav.transform.Rotate(Vector3.left, 90.0f);
            wav.GetComponent<BeamWave>().col = this.GetComponent<BeamParam>().BeamColor;

        }


        //create GeroBeam
        if (Input.GetButtonDown("Fire2"))
        {
            GameObject wav = (GameObject)Instantiate(Wave, this.transform.position, this.transform.rotation);
            wav.transform.Rotate(Vector3.left, 90.0f);
            wav.GetComponent<BeamWave>().col = this.GetComponent<BeamParam>().BeamColor;

            Bullet = Shot2;
            //Fire
            NowShot = (GameObject)Instantiate(Bullet, this.transform.position, this.transform.rotation);
        }
        //it's Not "GetButtonDown"
        if (Input.GetButton("Fire2"))
        {
            BeamParam bp = this.GetComponent<BeamParam>();
            if (NowShot.GetComponent<BeamParam>().bGero)
                NowShot.transform.parent = transform;

            Vector3 s = new Vector3(bp.Scale, bp.Scale, bp.Scale);

            NowShot.transform.localScale = s;
            NowShot.GetComponent<BeamParam>().SetBeamParam(bp);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            if (NowShot != null)
            {
                NowShot.GetComponent<BeamParam>().bEnd = true;
            }
        } */
    }
}

/*

using UnityEngine;
using System.Collections;

public class MultiShooter : MonoBehaviour {

	public GameObject Shot1;
	public GameObject Shot2;
    public GameObject Wave;
	public float Disturbance = 0;

	public int ShotType = 0;

	private GameObject NowShot;

	void Start () {
		NowShot = null;
	}

	void Update () {
		GameObject Bullet;
		
        //create BasicBeamShot
        if (Input.GetButtonDown("Fire1"))
        {
            Bullet = Shot1;
            //Fire
            GameObject s1 = (GameObject)Instantiate(Bullet, this.transform.position, this.transform.rotation);
            s1.GetComponent<BeamParam>().SetBeamParam(this.GetComponent<BeamParam>());
            
            GameObject wav = (GameObject)Instantiate(Wave, this.transform.position, this.transform.rotation);
            wav.transform.localScale *= 0.25f;
            wav.transform.Rotate(Vector3.left, 90.0f);
            wav.GetComponent<BeamWave>().col = this.GetComponent<BeamParam>().BeamColor;
            
        }


        //create GeroBeam
        if (Input.GetButtonDown("Fire2"))
        {
            GameObject wav = (GameObject)Instantiate(Wave, this.transform.position, this.transform.rotation);
            wav.transform.Rotate(Vector3.left, 90.0f);
            wav.GetComponent<BeamWave>().col = this.GetComponent<BeamParam>().BeamColor;

            Bullet = Shot2;
            //Fire
            NowShot = (GameObject)Instantiate(Bullet, this.transform.position, this.transform.rotation);
        }
            //it's Not "GetButtonDown"
        if (Input.GetButton ("Fire2"))
		{
			BeamParam bp = this.GetComponent<BeamParam>();
			if(NowShot.GetComponent<BeamParam>().bGero)
				NowShot.transform.parent = transform;

			Vector3 s = new Vector3(bp.Scale,bp.Scale,bp.Scale);

			NowShot.transform.localScale = s;
			NowShot.GetComponent<BeamParam>().SetBeamParam(bp);
		}
        if (Input.GetButtonUp ("Fire2"))
		{
			if(NowShot != null)
			{
				NowShot.GetComponent<BeamParam>().bEnd = true;
			}
		}
	}
}
*/