using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    public Light lt;
    public MindTracking myTracker;

    void Start () {
        lt = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        lt.intensity = myTracker.lightNum;
        lt.color = Color.Lerp(Color.black, Color.white, myTracker.lightNum);
	}
}
