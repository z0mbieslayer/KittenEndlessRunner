using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour {

    //Drag Prefabs
    public GameObject bridgeObstacle;
    public GameObject bridgeObstacleAR;
    private GameObject bridgeObstacleChoosen;
    public GameObject hurdleObstacle;
    public GameObject coinPoints;

    GameObject runnerObj;
    Animator animatorCat;

    //Arrays
    ArrayList arrBridges;
    ArrayList arrHurdles;

    float bridgeMoveSpeed = 2.5f;

    //Game Over
    int gameOverCounter = 0;
    bool isGameOver = false;

    //Audio
    AudioSource[] audioSources;

    int totalScore;

    //Get text
    GameObject endResult;

    // Use this for initialization
    void Start () {

        //Initialize arrays
        arrBridges = new ArrayList();
        arrHurdles = new ArrayList();

        //Get Cat Animator
        animatorCat = GetComponent<Animator>();

        //Get Runner Obj
        runnerObj = GameObject.Find("KittenRunner");

        //Get Sounds
        audioSources = GameObject.Find("Sounds").GetComponents<AudioSource>();

        //All Text
        endResult = GameObject.Find("EndResult");
        endResult.SetActive(false);

    }

    // Update is called once per frame
    void Update () {

        if (isGameOver == false)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                animatorCat.SetTrigger("jump");
                audioSources[1].Play();
            }

            //Add and Move Bridges
            addBridges();
            moveBridges();

            //Add and Move Hurdles
            addHurdles();
            moveHurdles();
        }

    }

    //Add bridges
    void addBridges()
    {
        //Add Bridges in start
        if (arrBridges.Count == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                if (PlayerPrefs.GetString("GameMode") == "AR")
                {
                    bridgeObstacleChoosen = bridgeObstacleAR;
                }
                else
                {
                    bridgeObstacleChoosen = bridgeObstacle;
                }

                Vector3 bridgePosition = bridgeObstacleChoosen.transform.position;
                bridgePosition.z = bridgeObstacleChoosen.transform.position.z + bridgeObstacleChoosen.GetComponent<BoxCollider>().size.z * i;
                GameObject bridgeObj = Instantiate(bridgeObstacleChoosen, bridgePosition, bridgeObstacleChoosen.transform.rotation) as GameObject;
                arrBridges.Add(bridgeObj);
            }
        }

        //Add One Bridge
        if (arrBridges.Count < 8)
        {
            if (PlayerPrefs.GetString("GameMode") == "AR")
            {
                bridgeObstacleChoosen = bridgeObstacleAR;
            }
            else
            {
                bridgeObstacleChoosen = bridgeObstacle;
            }

            GameObject lastBridgePositonObj = arrBridges[arrBridges.Count - 1] as GameObject;
            Vector3 lastBridgePosition = lastBridgePositonObj.transform.position;
            lastBridgePosition.z = lastBridgePositonObj.transform.position.z + bridgeObstacleChoosen.GetComponent<BoxCollider>().size.z;
            GameObject singleBridgeObj = Instantiate(bridgeObstacleChoosen, lastBridgePosition, bridgeObstacleChoosen.transform.rotation) as GameObject;
            arrBridges.Add(singleBridgeObj);
        }

    }

    //Move Bridges
    void moveBridges()
    {
        ArrayList arrTempBridges = new ArrayList(arrBridges);

        foreach (GameObject item in arrBridges)
        {
            if (item.transform.position.z >= 190)
            {
                //Move
                item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z - (bridgeMoveSpeed * Time.deltaTime));
            }
            else
            {
                arrTempBridges.Remove(item);
                Destroy(item);
            }
        }

        arrBridges = arrTempBridges;
    }

    //Add Hurdles
    void addHurdles()
    {
        //Add Hurdles in start
        if (arrHurdles.Count == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                //Main container
                GameObject mainContainerObj = new GameObject();

                //Add Hurdle
                Vector3 hurdlePosition = hurdleObstacle.transform.position;
                hurdlePosition.z = hurdleObstacle.transform.position.z + i * 5;
                GameObject hurdleObj = Instantiate(hurdleObstacle, hurdlePosition, hurdleObstacle.transform.rotation) as GameObject;

                //Main container position
                mainContainerObj.transform.position = hurdleObj.transform.position;

                //Add hurdle in main container
                hurdleObj.transform.SetParent(mainContainerObj.transform);

                if (Random.Range(1, 100) > 50)
                {
                    //Add coins
                    Vector3 coinsPosition = hurdleObj.transform.position;
                    coinsPosition.y = hurdleObj.GetComponent<BoxCollider>().size.y - hurdleObj.GetComponent<BoxCollider>().size.y / 2;
                    GameObject coinObj = Instantiate(coinPoints, coinsPosition, coinPoints.transform.rotation) as GameObject;
                    coinObj.transform.SetParent(mainContainerObj.transform);
                }

                //Add in array
                arrHurdles.Add(mainContainerObj);
            }
        }

        //Add One Hurdle
        if (arrHurdles.Count < 10)
        {
            //Main container
            GameObject mainContainerObj = new GameObject();

            GameObject lastHurdlePositonObj = arrHurdles[arrHurdles.Count - 1] as GameObject;
            Vector3 lastHurdlePosition = lastHurdlePositonObj.transform.position;
            lastHurdlePosition.z = lastHurdlePositonObj.transform.position.z + gamePlayValue();
            GameObject singleHurdleObj = Instantiate(hurdleObstacle, lastHurdlePosition, hurdleObstacle.transform.rotation) as GameObject;

            //Main container position
            mainContainerObj.transform.position = singleHurdleObj.transform.position;
            //Add hurdle in main container
            singleHurdleObj.transform.SetParent(mainContainerObj.transform);

            //Coins
            if (Random.Range(1, 100) > 50)
            {
                //Add coins
                Vector3 coinsPosition = singleHurdleObj.transform.position;
                coinsPosition.y = singleHurdleObj.GetComponent<BoxCollider>().size.y - singleHurdleObj.GetComponent<BoxCollider>().size.y / 2;
                GameObject coinObj = Instantiate(coinPoints, coinsPosition, coinPoints.transform.rotation) as GameObject;
                coinObj.transform.SetParent(mainContainerObj.transform);
            }
            

            //Add in array
            arrHurdles.Add(mainContainerObj);
        }
    }

    //Move Hurdles
    void moveHurdles()
    {
        ArrayList arrTempHurdles = new ArrayList(arrHurdles);

        foreach (GameObject item in arrHurdles)
        {
            if (item.transform.position.z >= 190)
            {
                //Move
                item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z - (bridgeMoveSpeed * Time.deltaTime));
            }
            else
            {
                arrTempHurdles.Remove(item);
                Destroy(item);
            }
        }

        arrHurdles = arrTempHurdles;
    }

    float gamePlayValue()
    {
        float gameValue = 2.3f;

        if (totalScore < 5)
        {
            gameValue = 3.0f;
        }
        else if (gameValue < 10)
        {
            gameValue = 2.7f;
        }
        else if (gameValue < 20)
        {
            gameValue = 2.5f;
        }
        else if (gameValue < 30)
        {
            gameValue = 2.4f;
        }
        else
        {
            gameValue = 2.3f;
        }

        return gameValue;
    }

    bool isHitWait = false;
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "coin")
        {
            totalScore++;
            audioSources[3].Play();
            Destroy(other.gameObject);
        }

        if (other.tag == "hurdle")
        {
            if (!isHitWait)
            {
                Debug.Log("Hit");

                //Hit Wait Logic
                isHitWait = true;
                Invoke("isHitWaitM", 1);

                //Colide sound
                audioSources[2].Play();

                //Colide Animation
                animatorCat.SetTrigger("hit");

                gameOverCounter++;
                if (gameOverCounter == 3)//Three Chance
                {
                    isGameOver = true;

                    //GameOver Animation
                    animatorCat.SetTrigger("end");

                    gameOver();

                }

            }
        }
    }

    void isHitWaitM()
    {
        if (isGameOver == false)
        {
            isHitWait = false;
        }
    }

    void gameOver()
    {
        Debug.Log("Game Over");

        //End Result UI
        endResultUI();

    }

    void endResultUI()
    {
        //Set values
        endResult.SetActive(true);
        GameObject.Find("lblERScore").GetComponent<TextMesh>().text = "Score : " + totalScore;

    }

}
