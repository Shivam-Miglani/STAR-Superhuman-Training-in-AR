/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SphereCommands : MonoBehaviour
{

    public GameObject Shot1;
    public GameObject Shot2;
    public GameObject Wave;
    public float Disturbance = 0;

    public int ShotType = 0;

    private GameObject NowShot;

    void Start()
    {
        NowShot = null;
    }
    // Called by GazeGestureManager when the user performs a Select gesture

    void Update()
    {
        if (NowShot != null)
        {
            NowShot.GetComponent<BeamParam>().bEnd = true;
        }
    }
    void OnSelect()
    {

        //float Start = System.DateTime.Now.Second;
        //float end = 0;
        //print("on select");
        // Invoke("Shooting",1);
        // print("invoke");
        // bool flag = true;

         GameObject Bullet;
         GameObject wav = (GameObject)Instantiate(Wave, this.transform.position, this.transform.rotation);
         wav.transform.Rotate(Vector3.left, 90.0f);
         wav.GetComponent<BeamWave>().col = this.GetComponent<BeamParam>().BeamColor;
         Bullet = Shot2;
         NowShot = (GameObject)Instantiate(Bullet, this.transform.position, this.transform.rotation);
  
        }
       

 

    IEnumerator Wait()
    {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
    }
}*/
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;

public class SphereCommands : MonoBehaviour, IInputClickHandler
{
    public GameObject Shot1;
    public GameObject Shot2;
    public GameObject Wave;
    public GameObject Bullet;
    public float Disturbance = 0; public int ShotType = 0; private GameObject NowShot;


    private void Start()
    {
        NowShot = null;
        InputManager.Instance.PushFallbackInputHandler(gameObject);
    }
    // Called by GazeGestureManager when the user performs a Select gesture    
    void Update()
    {
        if (NowShot != null)
        {
            NowShot.GetComponent<BeamParam>().bEnd = true;
        }
    }
    public void OnInputClicked(InputClickedEventData eventData)
    {
        GameObject wav = (GameObject)Instantiate(Wave, this.transform.position, this.transform.rotation);
        wav.transform.Rotate(Vector3.left, 90.0f);
        wav.GetComponent<BeamWave>().col = this.GetComponent<BeamParam>().BeamColor;
        Bullet = Shot2;  
        NowShot = (GameObject)Instantiate(Bullet, this.transform.position, this.transform.rotation);

    }
    IEnumerator Wait()
    {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
    }
    void Shooting()
    {
        print("shooting");
    
        GameObject wav = (GameObject)Instantiate(Wave, this.transform.position, this.transform.rotation);
        wav.transform.Rotate(Vector3.left, 90.0f);
        wav.GetComponent<BeamWave>().col = this.GetComponent<BeamParam>().BeamColor;
        Bullet = Shot2;
        NowShot = (GameObject)Instantiate(Bullet, this.transform.position, this.transform.rotation);
        print("exit shooting");
    }
}
