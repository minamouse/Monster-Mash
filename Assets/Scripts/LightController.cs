using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    public Light lt;

    TGCConnectionController controller;
    private int meditation;

    void Start () {
        lt = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        controller = GameObject.Find("NeuroSkyTGCController").GetComponent<TGCConnectionController>();
        controller.UpdateMeditationEvent += OnUpdateMeditation;
        //change this as 
        //lt.intensity = meditation / 50;
        //lt.color = Color.Lerp(Color.black, Color.white, meditation / 100);
	}
    void OnUpdateMeditation(int value)
    {
        meditation = value;
    }
}
