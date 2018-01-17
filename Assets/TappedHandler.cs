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
	public bool isCorePlaced = false;
	public static bool isFirstPlayer = false;
	public GameObject core;




  void Start()
  {
		this.recognizer = new UnityEngine.XR.WSA.Input.GestureRecognizer();
		this.recognizer.StartCapturingGestures();
		bool isFirstPlayer = CheckFirstPlayer();
		//if(HoloToolkit.Sharing.Tests.ImportExportAnchorManager.AnchorDebugText != null & isFirstPlayer){
		//	HoloToolkit.Sharing.Tests.ImportExportAnchorManager.AnchorDebugText.text += "\nYou are player 1";
		//}
		//else { HoloToolkit.Sharing.Tests.ImportExportAnchorManager.AnchorDebugText.text += "\nYou are player 2"; }
	}

	private void Update()
	{
		if (!MappingPlaceholderScript.scanning & !isCorePlaced & isFirstPlayer)
		{
			Vector3 retval = Camera.main.transform.position + Camera.main.transform.forward * 4;
			core.transform.position = Vector3.Lerp(core.transform.position, retval, 0.2f);
			this.recognizer.TappedEvent += OnTapped;
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

		long localUserId;
		using (User localUser = SharingStage.Instance.Manager.GetLocalUser())
		{
			localUserId = localUser.GetID();
		}

		if (SharingStage.Instance.SessionUsersTracker.CurrentUsers[0].GetID() < localUserId)
		{
			return false;
		}
		else
			return true;

	}
	UnityEngine.XR.WSA.Input.GestureRecognizer recognizer;
}