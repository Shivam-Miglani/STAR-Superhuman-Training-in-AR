using HoloToolkit.Sharing;
using HoloToolkit.Sharing.Spawning;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

 
public class TappedHandler : MonoBehaviour
{
  public PrefabSpawnManager spawnManager;
 
  void Start()
  {
    this.recognizer = new UnityEngine.XR.WSA.Input.GestureRecognizer();
    this.recognizer.TappedEvent += OnTapped;
    this.recognizer.StartCapturingGestures();
  }
  void OnTapped(UnityEngine.XR.WSA.Input.InteractionSourceKind source, int tapCount, Ray headRay)
  {
    // If we're networking...
    if (SharingStage.Instance.IsConnected)
    {
      // Make a new cube that is 2m away in direction of gaze but then get that position
      // relative to the object that we are attached to (which is world anchor'd across
      // our devices).
      var newCubePosition =
        this.gameObject.transform.InverseTransformPoint(
          (GazeManager.Instance.GazeOrigin + GazeManager.Instance.GazeNormal * 2.0f));
 
      // Use the span manager to span a 'SyncSpawnedObject' at that position with
      // some random rotation, parent it off our gameObject, give it a base name (MyCube)
      // and do not claim ownership of it so it stays behind in the scene even if our
      // device leaves the session.
      this.spawnManager.Spawn(
        new SyncSpawnedObject(),
        newCubePosition,
        Random.rotation,
        this.gameObject,
        "MyCube",
        false);
    }
  }
  UnityEngine.XR.WSA.Input.GestureRecognizer recognizer;
}