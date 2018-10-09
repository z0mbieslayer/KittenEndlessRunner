using UnityEngine;
using System.Collections;

public class Rater : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //Rater
        showAPIRaterFinalX();

        //Delete Counter Just for testing purpose make sure to comment this
        //PlayerPrefs.DeleteAll();
    }

    //Create Rate URL
    string getRateURL()
    {
        string appId = "com.ZeeMelApps.KittenRunner";//GooglePlay, Amazon, Samsung
        //string appId = "49058ZeeMelApps.EndlessKittenRun_bc0tz99paqz1e";//Windows 10
        //string appId = "1185855724";//Apple

        string rateURL = "market://details?id=" + appId;//Google Play
        //string rateURL = "amzn://apps/android?p=" + appId;//Amazon
        //string rateURL = "samsungapps://ProductDetail/" + appId;//Samsung
        //string rateURL = "ms-windows-store:REVIEW?PFN=" + appId;//Windows 10
        //string rateURL = "https://itunes.apple.com/app/id" + appId;//Apple

        return rateURL;
    }

    int initialLaunchCount = 5;
    int remindLaterLaunchCount = 10;
    void rateAppShow()
    {
        // Show Rater Dialog
        raterObj.SetActive(true);
    }

    public void btnRatingRateIt()
    {
        //Rate It
        PlayerPrefs.SetString("CounterRater", "100000");

        //Open Rate URL
        Application.OpenURL(getRateURL());

        //Hide Rate It
        raterObj.SetActive(false);
    }

    public void btnRatingRemindMeLater()
    {
        //Remind Me Later
        PlayerPrefs.SetString("CounterRater", remindLaterLaunchCount.ToString());

        //Hide Rate It
        raterObj.SetActive(false);
    }

    bool canShowRateIt()
    {

        bool canGo = false;

        string fTIme = "";
        try
        {
            fTIme = PlayerPrefs.GetString("FirstTimeRater");
        }
        catch (System.Exception)
        {
            PlayerPrefs.SetString("FirstTimeRater", "no");
            PlayerPrefs.SetString("CounterRater", initialLaunchCount.ToString());
        }

        if (fTIme == "no")
        {
            string counterS = PlayerPrefs.GetString("CounterRater");
            int counter = int.Parse(counterS);
            counter--;
            if (counter <= 0)
            {
                canGo = true;
            }
            else
            {
                PlayerPrefs.SetString("CounterRater", counter.ToString());
            }

        }
        else
        {
            PlayerPrefs.SetString("FirstTimeRater", "no");
            PlayerPrefs.SetString("CounterRater", initialLaunchCount.ToString());
        }

        return canGo;

    }

    void showAPIRateFinal()
    {
        try
        {

            if (canShowRateIt())
            {
                if (checkNetworkAvailability())
                {
                    rateAppShow();
                }

            }
        }
        catch (System.Exception)
        {
        }
    }

    //Get Rater Box
    GameObject raterObj;
    void showAPIRaterFinalX()
    {
        //API Rater
        raterObj = GameObject.Find("Canvas/Rater");
        raterObj.SetActive(false);
        showAPIRateFinal();

    }

    //Check internet availability
    bool checkNetworkAvailability()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
