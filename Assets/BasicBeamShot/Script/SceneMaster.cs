using UnityEngine;
using System.Collections;

public class SceneMaster : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnControllerColliderHit(ControllerColliderHit hit){
		if(hit.gameObject.tag == "SceneChanger"){
			print("SceneChanger_Detected");
			JumpScene js = hit.transform.GetComponent<JumpScene>();
			if(js != null)
			{
				js.ChangeScene();
			}
		}
	}

}
