using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPath : MonoBehaviour
{

    public static bool onPath = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		//RaycastHit hit;
		if (!MappingPlaceholderScript.scanning)
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 20))
			{
				Debug.DrawLine(transform.position, hit.point);
				Debug.Log(string.Format("Position: {0}", transform.position.ToString()));
				if (!onPath)
				{
					Debug.Log("On path");
				}
				onPath = true;
			}
			else
			{
				if (onPath)
				{
					Debug.Log("Not on path");
				}
				onPath = false;
			}
		}
       
    }
}
