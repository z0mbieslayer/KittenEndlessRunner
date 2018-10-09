using UnityEngine;
using System.Collections;

public class CanUseTouch : MonoBehaviour {

    //Can use touch
    static bool canUseTouchBool;

    public static bool canUseTouchM()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            canUseTouchBool = true;
        }
        else
        {
            canUseTouchBool = false;
        }

        return canUseTouchBool;//true for test

    }
}
