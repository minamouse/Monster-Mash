using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour {

    Animator animator;
    int jumpHash = Animator.StringToHash("Jump");
    float goal = 0.0f;
    float yGoal = 0.0f;

    public GameObject goblinPrefab;
    public GameObject insectPrefab;
    public GameObject trollPrefab;
    public GameObject coin;

    int stopHash = Animator.StringToHash("Stop Game");
    int startHash = Animator.StringToHash("Start Game");
    bool gameOver = true;
    Vector3 startPos = new Vector3(0, 0, -5.93f);

    public List<GameObject> monsterTypes = new List<GameObject>();
    public List<float> positions = new List<float>();
    public HashSet<GameObject> monsters = new HashSet<GameObject>();
    public List<GameObject> coins = new List<GameObject>();

    public int points = 0;

    TGCConnectionController controller;
    private int attention;

    public CameraController myCamera;
    float distance;


    void Start () {
        monsterTypes.Add(goblinPrefab);
        monsterTypes.Add(insectPrefab);
        monsterTypes.Add(trollPrefab);

        positions.Add(-0.6f);
        positions.Add(0.0f);
        positions.Add(0.6f);

        animator = GetComponent < Animator >();
        float distance = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        controller = GameObject.Find("NeuroSkyTGCController").GetComponent<TGCConnectionController>();
        controller.UpdateAttentionEvent += OnUpdateAttention;

        if (gameOver) {
            foreach (GameObject monsterClone in monsters) {
                Destroy(monsterClone);
            }
            monsters = new HashSet<GameObject>();
            if (Input.GetKeyDown(KeyCode.Space)){
                gameOver = false;
                animator.SetTrigger(startHash);
            }
        } else {
            transform.Translate(Vector3.forward * Time.deltaTime * 3);
            yGoal = attention / 50;
            Vector3 d = new Vector3(goal - transform.position.x, yGoal-transform.position.y, 0.0f);
            float step = Time.deltaTime * 3;
            distance += step;

            if (distance >= 3.0f) {
                float rand = Random.value;


                if (rand <= ((attention/100)+0.5)) {
                    
                    GameObject monster = monsterTypes[(int)Random.Range(0, 2.9f)];
                    Vector3 monsterPos = new Vector3(positions[(int)Random.Range(0, 2.9f)], 0, transform.position.z + 20);
                    GameObject monsterClone = Instantiate(monster, monsterPos, Quaternion.Euler(0, 180, 0));
                    monsters.Add(monsterClone);

                } else if (rand > ((attention/100)+0.5)) {
                    
                    Vector3 coinPos = new Vector3(positions[(int)Random.Range(0, 2.9f)], 0.4f, transform.position.z + 20);
                    GameObject thisCoin = Instantiate(coin, coinPos, Quaternion.Euler(0, 180, 0));
                    coins.Add(thisCoin);

                }
                distance = 0.0f;
            }

            transform.position = Vector3.MoveTowards(transform.position, transform.position + d, step);


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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Monster")
        {
            gameOver = true;
            animator.SetTrigger(stopHash);
            transform.position = startPos;
            myCamera.endGame();
            points = 0;
        }

        if (col.gameObject.tag == "Coin")
        {
            points += 1;

        }
    }
}
