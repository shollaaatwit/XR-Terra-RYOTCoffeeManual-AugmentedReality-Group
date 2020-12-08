using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject nonAREnvironment;
    public GameObject gameUIPanel;

    private void Awake()
    {
        ARController.OnARRunning.AddListener(OnARRunningListener);
    }

    private void OnARRunningListener(bool ar)
    {
        ScreenLog.Log("OnARRunningListener: " + ar);
        nonAREnvironment.SetActive(!ar); //places setting for working in non AR

        if(ar)
        {
            //do something if AR is active

        }
        else
        {
            //do something if AR isn't active
        }
    }

}
