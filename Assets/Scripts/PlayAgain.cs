using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour {

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Play Again
                if (hit.transform.name == "btnPlayAgain")
                {
                    SceneManager.LoadScene("CatRunGame");
                }

                //Back to Main Screen
                if (hit.transform.name == "btnBack")
                {
                    SceneManager.LoadScene("MainScene");
                }
            }
        }

    }
}
