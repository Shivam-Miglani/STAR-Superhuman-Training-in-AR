using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Sharing.Tests;
using HoloToolkit.Sharing;

public class SphereCommands : MonoBehaviour, IInputClickHandler
{
	public static int score;
	public static int coopscore;
	public static bool win;
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
	// public GameObject planeb1;
	private bool pb1 = false;
	private bool pb2 = false;
	private bool pr1 = false;
	private bool pr2 = false;
	public GameObject core;
	CustomMessages customMessages;


	private void Start()
    {
		score = 0;
		coopscore = 0;
		win = false;
		NowShot = null;
        count = 0;
		//InputManager.Instance.PushFallbackInputHandler(gameObject);
		customMessages = CustomMessages.Instance;
		customMessages.MessageHandlers[CustomMessages.TestMessageID.BlueShieldsDestroyed] = this.UpdateCoreDestructionBlue;
		customMessages.MessageHandlers[CustomMessages.TestMessageID.RedShieldsDestroyed] = this.UpdateCoreDestructionRed;
		
	}
    // Called by GazeGestureManager when the user performs a Select gesture    
    void Update()
    {
        if (NowShot != null)
        {
            NowShot.GetComponent<BeamParam>().bEnd = true;
        }

		if (pb1 && pb2 && pr1 && pr2)
		{
			win = true;
			coopscore = 100;
			GameObject ex = core.transform.Find("Explosion").gameObject;
			ex.SetActive(true);
			Destroy(core, 1f);
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
		BeamParam bp = Shot2.GetComponent<BeamParam>();
		if (TappedHandler.isFirstPlayer)
			bp.BeamColor = Color.blue;
		else
			bp.BeamColor = Color.red;

		if (energy.CurrentEnergy >= 10f && Health.CurrentHealth > 0f)
		{
			energy.DecreaseEnergy(10);
			Debug.Log("enenergy derease !!!");
			GameObject wav = (GameObject)Instantiate(Wave, this.transform.position, this.transform.rotation);
			wav.transform.Rotate(Vector3.left, 90.0f);
			wav.GetComponent<BeamWave>().col = this.GetComponent<BeamParam>().BeamColor;
			Bullet = Shot2;
			NowShot = (GameObject)Instantiate(Bullet, this.transform.position, this.transform.rotation);
			print("exit shooting");

			focus = GazeGestureManager.Instance.FocusedObject;
			
			if (TappedHandler.isFirstPlayer)
			{
				if (focus.name == "Planeb1")
				{
					focus.SetActive(false);
					pb1 = true;

				} else
				if (focus.name == "Planeb2")
				{
					focus.SetActive(false);
					pb2 = true;
				}
				else
				{
					GameObject ex = focus.transform.Find("Explosion").gameObject;
					if (ex != null)
					{
						score += 10;
					}

					ex.SetActive(true);
					Destroy(focus, 0.8f);
				}

				if (pb1 && pb2)
				{

					CustomMessages.Instance.SendBlueShieldsDestroyed();
					
				}

			} else if (!TappedHandler.isFirstPlayer)
			{
				if (focus.name == "Planer1")
				{
					focus.SetActive(false);
					pr1 = true;
				}
				else if (focus.name == "Planer2")
				{
					focus.SetActive(false);
					pr2 = true;
				}
				else
				{
					GameObject ex = focus.transform.Find("Explosion").gameObject;
					if (ex != null)
					{
						score += 10;
					}

					ex.SetActive(true);
					Destroy(focus, 0.8f);
				}

				if (pr1 && pr2)
				{
					CustomMessages.Instance.SendRedShieldsDestroyed();
				}
			} 
            

           // focus.SetActive(false);

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

	void UpdateCoreDestructionRed(NetworkInMessage msg)
	{
		Debug.Log("Core Destruction will happen");
		// Parse the message
		long userID = msg.ReadInt64();
		pr1 = true;
		pr2 = true;
	}

	void UpdateCoreDestructionBlue(NetworkInMessage msg)
	{
		Debug.Log("Core Destruction will happen");
		// Parse the message
		long userID = msg.ReadInt64();
		pb1 = true;
		pb2 = true;
	}

}
