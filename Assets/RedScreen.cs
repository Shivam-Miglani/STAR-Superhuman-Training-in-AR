using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedScreen : MonoBehaviour
{

    public GameObject redCanvas;
	private int counter = 0;

    // Use this for initialization
    void Start()
    {
		redCanvas.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
		if (!MappingPlaceholderScript.scanning)
		{
			if (OnPath.onPath)
			{
				Debug.Log("Inside RedScreen's update function onPath true");
				redCanvas.SetActive(false);
			}
			else
			{
				Debug.Log("Inside RedScreen's update function onPath false");
				redCanvas.SetActive(true);
				counter++;
				if (counter >= 50)
				{
					Health.DecreaseHealth(10);
					counter = 0;
				}
			}
		}
    }
}
