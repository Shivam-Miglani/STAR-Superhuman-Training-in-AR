using HoloToolkit.Sharing;
using HoloToolkit.Sharing.Spawning;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
#if UNITY_WSA && !UNITY_EDITOR
using System.Collections.Generic;
#if UNITY_2017_2_OR_NEWER
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Persistence;
using UnityEngine.XR.WSA.Sharing;
#else
using UnityEngine.VR.WSA;
using UnityEngine.VR.WSA.Persistence;
using UnityEngine.VR.WSA.Sharing;
#endif
#endif


public class TappedHandler : MonoBehaviour
{
	public PrefabSpawnManager spawnManager;
	public static bool isCorePlaced = false;
	public static bool isFirstPlayer = false;
	public GameObject core;
	public bool idsFound = false;
	public GameObject path;
	/// <summary>
	/// Debug text for displaying information.
	/// </summary>
	public TextMesh AnchorDebugText;



	void Start()
  {
		this.recognizer = new UnityEngine.XR.WSA.Input.GestureRecognizer();
		this.recognizer.StartCapturingGestures();
		
		//if(HoloToolkit.Sharing.Tests.ImportExportAnchorManager.AnchorDebugText != null & isFirstPlayer){
		//	HoloToolkit.Sharing.Tests.ImportExportAnchorManager.AnchorDebugText.text += "\nYou are player 1";
		//}
		//else { HoloToolkit.Sharing.Tests.ImportExportAnchorManager.AnchorDebugText.text += "\nYou are player 2"; }
	}

	private void Update()
	{
		if (SharingStage.Instance.SessionUsersTracker.CurrentUsers.Count > 0)
		{
			idsFound = true;
		} else
		{
			idsFound = false;
		}
		if (idsFound)
		{
			isFirstPlayer = CheckFirstPlayer();
			
			if (!MappingPlaceholderScript.scanning & !isCorePlaced & isFirstPlayer)
			{
				Debug.Log("ONLY FIRST PLAYER CAN PLACE THE CORE *****************************");
				Debug.Log(isCorePlaced);
				Debug.Log(isFirstPlayer);
				if (AnchorDebugText != null)
				{
					AnchorDebugText.text = "\nFIRST PLAYER";
					
				}
				
				Vector3 retval = Camera.main.transform.position + Camera.main.transform.forward * 2;
				retval = new Vector3(retval.x, path.transform.position.y + 0.02f, retval.z);
				core.transform.position = Vector3.Lerp(core.transform.position, retval, 0.2f);
				this.recognizer.TappedEvent += OnTapped;
			}
			if (AnchorDebugText != null & !isFirstPlayer)
			{
				AnchorDebugText.text = "\nNOT FIRST PLAYER";
				
			}

		}
	}
	void OnTapped(UnityEngine.XR.WSA.Input.InteractionSourceKind source, int tapCount, Ray headRay)
	{
	
    // If we're networking...
    if (SharingStage.Instance.IsConnected & !isCorePlaced)
    {

			// Make a new cube that is 2m away in direction of gaze but then get that position
			// relative to the object that we are attached to (which is world anchor'd across
			// our devices).
			var corePosition =
			  this.gameObject.transform.InverseTransformPoint(
				  core.transform.position);
			  // (GazeManager.Instance.GazeOrigin + GazeManager.Instance.GazeNormal * 4.0f));

			// Use the span manager to span a 'SyncSpawnedObject' at that position with
			// some random rotation, parent it off our gameObject, give it a base name (MyCube)
			// and do not claim ownership of it so it stays behind in the scene even if our
			// device leaves the session.
			// var corePosition = core.transform.InverseTransformPoint(core.transform.position);

			this.spawnManager.Spawn(
			  new SyncSpawnedObject(),
			  corePosition,
			  core.transform.rotation,
			  this.gameObject,
			  "MyCube",
			  false);

			core.SetActive(false);
			isCorePlaced = true;
    }


  }



	public bool CheckFirstPlayer()
	{
		Debug.Log("CHECK FIRST PLAYER =================*********");
		long localUserId;
		using (User localUser = SharingStage.Instance.Manager.GetLocalUser())
		{
			localUserId = localUser.GetID();
			Debug.Log(localUserId);
		}

		if (SharingStage.Instance.SessionUsersTracker.CurrentUsers[0].GetID() < localUserId)
		{
			Debug.Log(SharingStage.Instance.SessionUsersTracker.CurrentUsers[0].GetID());
			
			//Debug.Log(SharingStage.Instance.SessionUsersTracker.CurrentUsers[1].GetID());
			Debug.Log(SharingStage.Instance.SessionUsersTracker.CurrentUsers.Count);
			
			Debug.Log("not first player");
			isCorePlaced = true;
			return false;
		}
		else { Debug.Log("first player"); return true; }
			

	}
	UnityEngine.XR.WSA.Input.GestureRecognizer recognizer;
}