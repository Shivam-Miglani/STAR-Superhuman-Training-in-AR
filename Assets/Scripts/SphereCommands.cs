using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;

public class SphereCommands : MonoBehaviour, IInputClickHandler
{
    public GameObject Shot1;
    public GameObject Shot2;
    public GameObject Wave;
    GameObject Bullet;
    GameObject focus;

    public float Disturbance = 0; public int ShotType = 0; private GameObject NowShot;


    private void Start()
    {
        NowShot = null;
        //InputManager.Instance.PushFallbackInputHandler(gameObject);
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
    void shooting()
    {
        print("shooting");
    
        GameObject wav = (GameObject)Instantiate(Wave, this.transform.position, this.transform.rotation);
        wav.transform.Rotate(Vector3.left, 90.0f);
        wav.GetComponent<BeamWave>().col = this.GetComponent<BeamParam>().BeamColor;
        Bullet = Shot2;
        NowShot = (GameObject)Instantiate(Bullet, this.transform.position, this.transform.rotation);
        print("exit shooting");

        //var bot = GameObject.Find("Target/attackBot");

      
        focus = GazeGestureManager.Instance.FocusedObject;
        print(focus.name);

        GameObject ex = focus.transform.Find("Explosion").gameObject;
      
        print(ex.name);
            ex.SetActive(true);
            Destroy(focus, 0.8f);
            
        
       // explosion.SetActive(false);
    }

}
