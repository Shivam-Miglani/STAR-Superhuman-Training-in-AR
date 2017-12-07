using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedScreen : MonoBehaviour
{

    public GameObject redCanvas;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OnPath.onPath)
        {
            redCanvas.SetActive(false);
        }
        else
        {
            redCanvas.SetActive(true);
        }
    }
}
