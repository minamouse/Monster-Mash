using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public CameraController myCamera;
    public MindTracking myTracker;
    float distance;

    public AudioClip coinDing;
    public AudioSource myAudio;

    public bool won = false;

    public Text displayScore;

    float monsterLane = 0.0f;
    float coinLane = 0.0f;


    void Start () {
        monsterTypes.Add(goblinPrefab);
        monsterTypes.Add(insectPrefab);
        monsterTypes.Add(trollPrefab);

        positions.Add(-0.6f);
        positions.Add(0.0f);
        positions.Add(0.6f);

        animator = GetComponent < Animator >();
        distance = 0.0f;
        myAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (won) {
            displayScore.text = "Points: " + points;
            gameOver = true;
            myCamera.gameOver = true;
            animator.SetTrigger(stopHash);
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
        } else if (gameOver) {
            displayScore.text = " ";

            foreach (GameObject monsterClone in monsters) {
                Destroy(monsterClone);
            }
            foreach (GameObject coinClone in coins) {
                Destroy(coinClone);
            }
            monsters = new HashSet<GameObject>();
            coins = new List<GameObject>();

            if (Input.GetKeyDown(KeyCode.Space)){
                gameOver = false;
                animator.SetTrigger(startHash);
            }
        } else {
            transform.Translate(Vector3.forward * Time.deltaTime * myTracker.speed);

            Vector3 d = new Vector3(goal - transform.position.x, myTracker.floating-transform.position.y, 0.0f);
            float step = Time.deltaTime * 3;
            distance += step;

            if (distance >= 2.0f) {

                if (transform.position.z <= 290)
                {

                    if (myTracker.spawnMonster)
                    {
                        monsterLane = positions[(int)Random.Range(0, 2.9f)];
                        GameObject monster = monsterTypes[(int)Random.Range(0, 2.9f)];
                        Vector3 monsterPos = new Vector3(monsterLane, 0, transform.position.z + 20);
                        GameObject monsterClone = Instantiate(monster, monsterPos, Quaternion.Euler(0, 180, 0));
                        monsters.Add(monsterClone);

                    }
                    if (myTracker.spawnCoin)
                    {
                        coinLane = positions[(int)Random.Range(0, 2.9f)];
                        if (coinLane != monsterLane) {
                            Vector3 coinPos = new Vector3(coinLane, 0.4f, transform.position.z + 20);
                            GameObject thisCoin = Instantiate(coin, coinPos, Quaternion.Euler(0, 180, 0));
                            coins.Add(thisCoin);

                        }

                    }
                }
                distance = 0.0f;
            }

            transform.position = Vector3.MoveTowards(transform.position, transform.position + d, step);

            if (transform.position.z >= 310) {
                won = true;
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


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Monster")
        {

            RestartGame();
        }

        if (col.gameObject.tag == "Coin")
        {
            myAudio.PlayOneShot(coinDing);
            points += myTracker.coinPoints;

        }

    }

    void RestartGame()
    {
        points = 0;
        gameOver = true;
        won = false;
        animator.SetTrigger(stopHash);
        transform.position = startPos;
        myCamera.endGame();
    }

}
