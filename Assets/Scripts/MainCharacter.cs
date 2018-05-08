using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour {

    Animator animator;
    int jumpHash = Animator.StringToHash("Jump");
    float goal = 0.0f;

    public GameObject goblinPrefab;
    public GameObject insectPrefab;
    public GameObject trollPrefab;
    int stopHash = Animator.StringToHash("Stop Game");
    int startHash = Animator.StringToHash("Start Game");
    bool gameOver = true;
    Vector3 startPos = new Vector3(0, 0, -5.93f);

    TGCConnectionController controller;
    private int attention;
    private int meditation;

    public CameraController myCamera;


    void Start () {
        animator = GetComponent < Animator >();
        Debug.Log(myCamera);
	}
	
	// Update is called once per frame
	void Update () {
        controller = GameObject.Find("NeuroSkyTGCController").GetComponent<TGCConnectionController>();
        controller.UpdateMeditationEvent += OnUpdateMeditation;
        controller.UpdateAttentionEvent += OnUpdateAttention;

        if (gameOver) {
            if (Input.GetKeyDown(KeyCode.Space)){
                gameOver = false;
                animator.SetTrigger(startHash);
                Instantiate(insectPrefab, transform.position + new Vector3(0, 0, 10), Quaternion.Euler(0, 180, 0));
            }
        } else {
            transform.Translate(Vector3.forward * Time.deltaTime * 3);
            Vector3 d = new Vector3(goal - transform.position.x, 0.0f, 0.0f);
            float step = Time.deltaTime * 3;

            if (transform.position.x != goal)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + d, step);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger(jumpHash);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (goal != -0.6f)
                {
                    goal -= 0.6f;
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (goal != 0.6f)
                {
                    goal += 0.6f;
                }
            }
        }


	}
    void OnUpdateAttention(int value)
    {
        attention = value;
    }
    void OnUpdateMeditation(int value)
    {
        meditation = value;
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Monster")
        {
            
            gameOver = true;
            animator.SetTrigger(stopHash);
            transform.position = startPos;
            myCamera.endGame();
        }
    }
}
