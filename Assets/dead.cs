using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dead : MonoBehaviour
{
	Text text;

	// Use this for initialization
	void Start()
	{
		text = GetComponent<Text>();
		text.text = "";
	}

	// Update is called once per frame
	void Update()
	{
		if (SphereCommands.win)
			text.text = "YOU WIN! Your Coop Score is: " + SphereCommands.coopscore;
		else if (Health.CurrentHealth == 0)
		{
			text.text = "GAME OVER";
		} 
	}
}
