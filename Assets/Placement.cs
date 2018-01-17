using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Sharing;
using System;

public class Placement : MonoBehaviour {
	bool isFirstPlayer = true;
	// Use this for initialization
	void Start () {
		
		//HoloToolkit.Sharing.Tests.CustomMessages.Instance.SendCustomMessage(parameter);

	}
	



	// Update is called once per frame
	void Update () {
		/*if (!MappingPlaceholderScript.scanning)
		{
			Vector3 retval = Camera.main.transform.position + Camera.main.transform.forward * 4;
			transform.position = Vector3.Lerp(transform.position, retval, 0.2f);
		}*/
	}
}
