using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindTracking : MonoBehaviour {
    
    TGCConnectionController controller;
    private int attention;
    private int meditation;

    public float speed;
    public float lightNum;
    public float floating;
    public bool spawnMonster;
    public bool spawnCoin;
    public int coinPoints;

	// Use this for initialization
	void Start () {

        speed = 3;
        lightNum = 1;
        floating = 0;
        spawnMonster = false;
        spawnCoin = false;
        coinPoints = 1;
	}
	
	// Update is called once per frame
	void Update () {
        controller = GameObject.Find("NeuroSkyTGCController").GetComponent<TGCConnectionController>();
        controller.UpdateAttentionEvent += OnUpdateAttention;
        controller.UpdateMeditationEvent += OnUpdateMeditation;

        Debug.Log(meditation);
        Debug.Log(attention);

        float newSpeed = ((1 - (meditation/100)) * 6) + 3;
        if (speed < newSpeed) {
            speed += 0.5f;
        } else if (speed > newSpeed) {
            speed -= 0.5f;
        }

        float newLight = meditation / 100;
        if (lightNum < newLight) {
            lightNum += 0.1f;
        } else if (lightNum > newLight) {
            lightNum -= 0.1f;
        }

        float newFloating = (float) Math.Pow(1.1, (100-attention))/13780;
        if (floating < newFloating) {
            floating += 0.1f;
        } else if (floating > newFloating) {
            floating -= 0.1f;
        }


        coinPoints = meditation;

        float flipMonster = UnityEngine.Random.Range(0, meditation);
        if (flipMonster > 70) {
            spawnMonster = false;
        } else {
            spawnMonster = true;
        }

        float flipCoin = UnityEngine.Random.Range(0, meditation);
        if (flipCoin > 20) {
            spawnCoin = true;
        } else {
            spawnCoin = false;
        }

        coinPoints = meditation;

	}

    void OnUpdateAttention(int value)
    {
        attention = value;
    }
    void OnUpdateMeditation(int value)
    {
        meditation = value;
    }

}
