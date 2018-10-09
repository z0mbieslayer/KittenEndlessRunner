using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RealWorldCamScript : MonoBehaviour
{

    public static WebCamTexture backCam;
    GameObject arCube;

    //AR 3D Options
    Text txt3D;
    Text txtAR;

    //None Material
    public Material noneMaterial;
    public Material skyMaterial;

    //Envirnment
    GameObject environment;
    GameObject environmentAR;

    // Use this for initialization
    void Start()
    {

        //Envirnment
        environment = GameObject.Find("Environment");
        environmentAR = GameObject.Find("EnvironmentAR");

        //Get AR Cube
        arCube = GameObject.Find("Camera/ARCamCube");

        //AR 3D Options
        txt3D = GameObject.Find("Canvas/btn3D").GetComponentInChildren<Text>();
        txtAR = GameObject.Find("Canvas/btnAR").GetComponentInChildren<Text>();

        //Initial Settings
        if (PlayerPrefs.GetString("GameMode") == "3D" || PlayerPrefs.GetString("GameMode") == "")
        {
            menuButton3D();
        }

        if (PlayerPrefs.GetString("GameMode") == "AR")
        {
            menuButtonAR();
        }
    }

    public void menuButton3D()
    {

        PlayerPrefs.SetString("GameMode", "3D");
        
        arCube.SetActive(false);

        //AR Cam stop if playing
        if (backCam != null)
        {
            if (backCam.isPlaying)
                backCam.Stop();
        }

        //AR 3D Options
        txt3D.color = Color.green;
        txtAR.color = Color.white;

        //Set SkyBox
        RenderSettings.skybox = skyMaterial;

        //Set Environment
        environment.SetActive(true);
        environmentAR.SetActive(false);

        //Set GameMode
        PlayerPrefs.SetString("GameMode", "3D");

    }

    public void menuButtonAR()
    {

        PlayerPrefs.SetString("GameMode", "AR");
        
        arCube.SetActive(true);

        float worldScreenHeight = Camera.main.orthographicSize * 12.0f;//12.0f;//17
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        arCube.transform.localScale = new Vector3(worldScreenWidth, worldScreenHeight, 0.1f);

        //Opacity
        //GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.9f);

        if (backCam == null)
            backCam = new WebCamTexture();

        arCube.GetComponent<Renderer>().material.mainTexture = backCam;

        if (!backCam.isPlaying)
            backCam.Play();

        //AR 3D Options
        txt3D.color = Color.white;
        txtAR.color = Color.green;

        //Set SkyBox
        RenderSettings.skybox = noneMaterial;

        //Set Environment
        environment.SetActive(false);
        environmentAR.SetActive(true);

        //Set GameMode
        PlayerPrefs.SetString("GameMode", "AR");

    }

    //Back Button On the CatRunGame Scene
    public void menuBtnBack()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Update is called once per frame
    void Update()
    {
        //If escape press or back button on android
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainScene");
        }
    }

}
