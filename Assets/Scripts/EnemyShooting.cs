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
    Transform trans;
    AudioSource gunAudio;

    void Awake()
    {
        gunAudio = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        NowShot = null;
        trans = this.transform;
        InvokeRepeating("enShooting", nextShoot, nextShoot);
        InvokeRepeating("Move", 5f,5f);
    }
	
	// Update is called once per frame
	void Update () {
         if (NowShot != null)
         {
             NowShot.GetComponent<BeamParam>().bEnd = true;
         }
    }
    void FixedUpdate()
    {
        // Turn the enemy to face the player.
        Turning();
    }

    void Move()
    {
        Vector3 dir = Random.insideUnitSphere * 0.5f;
        this.transform.position = trans.position + dir;
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
        var playerPosition = Camera.main.transform.position;
        var playerDirection = -this.transform.position + Camera.main.transform.position;

        Quaternion Rotation = Quaternion.LookRotation(playerDirection);
        
         RaycastHit hitInfo;

        if (Physics.Raycast(this.transform.position, playerDirection, out hitInfo))
         {
            //If the raycast hit a the player...
           //GameObject player = hitInfo.collider.GetComponent<EnemyHealth>();
          // if (player != null)
          //  {
                //decrease the player health
          //  }
        }
        else
        {
        // If the raycast did not hit the player
        }

        gunAudio.Play();
        GameObject Bullet;
        Bullet = Shot1;
        //Fire
        NowShot = (GameObject)Instantiate(Bullet, this.transform.position, Rotation);
        Destroy(NowShot, 2.0f);

    }

    int EnemyCount()
    {
        return count;
    }

}
