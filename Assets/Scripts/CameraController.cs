using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public bool gameOver = true;
    public MindTracking myTracker;
    Vector3 startPos = new Vector3(0, 1.36f, -7.61f);
    // Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameOver = false;
            }

        } else {
            transform.Translate(Vector3.forward * Time.deltaTime * myTracker.speed, Space.World);
        }

	}

    public void endGame () {
        gameOver = true;
        transform.position = startPos;
    }

}
