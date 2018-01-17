using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;

public class SphereCommands : MonoBehaviour, IInputClickHandler
{
    public GameObject Shot2;
    public GameObject Wave;
    GameObject Bullet;
    GameObject focus;
    private int count;
    public float Disturbance = 0;
    public Health health;
    public Energy energy;
   // public int ShotType = 0;
    private GameObject NowShot;


    private void Start()
    {
        NowShot = null;
        count = 0;
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
        if (energy.CurrentEnergy >= 10f && Health.CurrentHealth > 0f)
        {
            energy.DecreaseEnergy(10);
            GameObject wav = (GameObject)Instantiate(Wave, this.transform.position, this.transform.rotation);
            wav.transform.Rotate(Vector3.left, 90.0f);
            wav.GetComponent<BeamWave>().col = this.GetComponent<BeamParam>().BeamColor;
            Bullet = Shot2;
            NowShot = (GameObject)Instantiate(Bullet, this.transform.position, this.transform.rotation);
            print("exit shooting");

            focus = GazeGestureManager.Instance.FocusedObject;
            print(focus.name);

            GameObject ex = focus.transform.Find("Explosion").gameObject;

            if (ex != null)
            {
                count++;
                print(count);
            }

            ex.SetActive(true);
            Destroy(focus, 0.8f);

            focus.SetActive(false);

        }
      //  else
      //  {
            //
       // }
       
            
    }

    int EnemyCount()
    {
        return count;
    }

}
