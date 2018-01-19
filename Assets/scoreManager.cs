using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour {
	
	Text text;
	// Use this for initialization
	void Start () {
		SphereCommands.score = 0;
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = SphereCommands.score.ToString();
	}
}
