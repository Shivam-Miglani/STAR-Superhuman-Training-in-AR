using HoloToolkit.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MappingPlaceholderScript : MonoBehaviour {

    public Text TextObject;

	// Use this for initialization
	void Start () {
        this.scanning = true;
    }

    public void ScannedEnough()
    {
        if (this.scanning)
        {
            SpatialUnderstanding.Instance.RequestFinishScan();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (this.scanning)
        {
            this.getSpatialStatistics();
        }
	}

    void getSpatialStatistics()
    {
        if (this.statsPtr == IntPtr.Zero)
        {
            this.statsPtr = SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticPlayspaceStatsPtr();
        }

        if ((++this.frameCount % 5 == 0) && SpatialUnderstandingDll.Imports.QueryPlayspaceStats(this.statsPtr) != 0)
        {
            var statistics = SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticPlayspaceStats();
            this.TextObject.text = string.Format("Total m2: {0:G2} | Horizontal m2 {1:G2}", statistics.TotalSurfaceArea, statistics.HorizSurfaceArea);
        }

    }

    int frameCount;
    IntPtr statsPtr;
    bool scanning;
}
