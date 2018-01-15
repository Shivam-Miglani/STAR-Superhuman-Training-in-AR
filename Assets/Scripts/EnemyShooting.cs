using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {
    public GameObject Shot1;
    GameObject Bullet;
    GameObject focus;
    public float nextShoot = 3f;
    private int count;
    public float Disturbance = 0;
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
    void Start () {
        NowShot = null;
        Ori = this.transform.position;
        //trans = new UnityEngine.Transform
        //{
        //    position = new Vector3(0.000f, 0.000f, 2.5f), //this.transform.position;
        //    rotation = this.transform.rotation,
        //    localScale = this.transform.localScale
        //};
        InvokeRepeating("enShooting", nextShoot, nextShoot);
         dir = Random.insideUnitSphere;
         destination = dir * 0.5f;
        //InvokeRepeating("Move", 5f,5f);
    }
	
	// Update is called once per frame
	void Update () {
         if (NowShot != null)
         {
             NowShot.GetComponent<BeamParam>().bEnd = true;
         }
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

            Debug.Log(" -------------------------------------------------------------------------");
            Debug.Log("current : " + pos);
            Debug.Log("origin: " + (Ori).ToString("F8") + dir);
            Debug.Log("distance:" + Vector3.Distance(Ori + dir, pos));
            //this.transform.position += dir * Time.deltaTime;
            this.transform.position = Vector3.Lerp(pos, Ori + dir, 0.5f * Time.deltaTime);
            // this.transform.Translate(Ori + dir - this.transform.position) * 1f * time.deltatime;
            Debug.Log("translate: " + ((Ori + dir - pos) * Time.deltaTime).ToString("F8"));
            Debug.Log(" lerp" + (Vector3.Lerp(pos, Ori + dir, 1f * Time.deltaTime)).ToString("F8"));
            Debug.Log("Time.deltaTime:" + Time.deltaTime);
            Debug.Log(" -------------------------------------------------------------------------");
        }
        else
        {
            Debug.Log("<<<<<<<<<<<<<<<<<<<<");
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
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
    }

    void enShooting()
    {
        Vector3 randomised = Random.insideUnitCircle * 0.2f;
        Vector3 playerPosition = Camera.main.transform.position - new Vector3 (0f,0.5f,0f) + randomised;
        var playerDirection = -this.transform.position + playerPosition;

        Quaternion Rotation = Quaternion.LookRotation(playerDirection);
        
         RaycastHit hitInfo;

        if (Physics.Raycast(this.transform.position, playerDirection, out hitInfo))
         {
            //If the raycast hit a the player...
            // decrease health bar
        }
     
        gunAudio.Play();
        GameObject Bullet;
        Bullet = Shot1;
        //Fire
        NowShot = (GameObject)Instantiate(Bullet, this.transform.position - new Vector3 (0f,0f,0.5f), Rotation);
        Destroy(NowShot, 2.0f);

    }

    int EnemyCount()
    {
        return count;
    }

}
