using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject Shot1;
   // public GameObject Shot2;
    GameObject Bullet;
    GameObject focus;
    public float nextShoot = 3f;
    public float Disturbance = 0;
   // public GameObject player;
    //private Collider playerc;
    private GameObject NowShot;
    //Transform trans;
    Vector3 Ori;
    AudioSource gunAudio;
    Vector3 dir;
    Vector3 destination;

    void Awake()
    {
        gunAudio = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        NowShot = null;
        Ori = this.transform.position;

        InvokeRepeating("enShooting", nextShoot, nextShoot);
        dir = Random.insideUnitSphere;
        destination = dir * 0.5f;

		
    }

    // Update is called once per frame
    void Update()
    {
       /* if (NowShot != null)
        {
            NowShot.GetComponent<BeamParam>().bEnd = true;
        }
        */
        MoveSmooth();
    }
    void FixedUpdate()
    {

        // Turn the enemy to face the player.
        Turning();
    }

    void MoveSmooth()
    {
        Vector3 pos = this.transform.position;
        // transform.position = Vector3.Lerp(pos, Ori + dir, 0.01f * Time.deltaTime);
        //if(pos != (Ori + dir))
        if (Vector3.Distance(Ori + dir, pos) > 0.5f)
        {
            this.transform.position = Vector3.Lerp(pos, Ori + dir, 0.5f * Time.deltaTime);
        }
        else
        {
            dir = Random.insideUnitSphere;
            destination = dir * 0.5f;
        }
    }

    void Move()
    {
        Vector3 dir = Random.insideUnitSphere * 0.5f;
        this.transform.position = Ori + dir;
    }

    void Turning()
    {
        var playerDirection = -Camera.main.transform.position + this.transform.position;
        Quaternion Rotation = Quaternion.LookRotation(playerDirection);
        this.transform.rotation = Rotation;
    }

    IEnumerator Wait()
    {
        Debug.Log(Time.time);
        yield return new WaitForSecondsRealtime(5);
    }

    void enShooting()
    {
		
		// Health h = Camera.main.GetComponent<Health>();
		if (Health.CurrentHealth > 0f)
        {
            Vector3 randomised = Random.insideUnitCircle * 0.2f;
            Vector3 playerPosition = Camera.main.transform.position - new Vector3(0f, 0.5f, 0f) + randomised;
            Vector3 playerDirection = -this.transform.position + playerPosition;

            Quaternion Rotation = Quaternion.LookRotation(playerDirection);

            // RaycastHit hitInfo;

            gunAudio.Play();
            GameObject Bullet;
            Bullet = Shot1;
            //Fire
            NowShot = (GameObject)Instantiate(Bullet, this.transform.position - new Vector3(0f, 0f, 0.5f), Rotation);

			 StartCoroutine(particleTrackWaitToSet(2.0f, playerDirection));
		/*	if (Physics.Raycast(this.transform.position, playerDirection, out hitInfo))
			{
				Debug.Log("hit on the palyer !!");
				if (Camera.main.gameObject == hitInfo.collider.gameObject)
				{
					Health.DecreaseHealth(5);
					Debug.Log("health decrease !!");
				}

			}
*/
		}
        Destroy(NowShot, 2.0f);

    }

    IEnumerator particleTrackWaitToSet(float fTime, Vector3 playerDirection)
    {
        yield return new WaitForSeconds(fTime);
        RaycastHit hitInfo;
        if (Physics.Raycast(this.transform.position, playerDirection, out hitInfo))
        {
            if (Camera.main.gameObject == hitInfo.collider.gameObject)
            {
                Health.DecreaseHealth(5);
				Debug.Log("health decrease !!");
            }
           
        }
        //Destroy(NowShot, 2.0f);
    }

}
