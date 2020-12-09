using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameController : Singleton<GameController>
{

    /*
     * Keep track our current step
     *
     * Keep track of coffee amount
     *
     * Function => converts the volume of coffee & water
     *
     * Enables/disables the gameObject stack of the mug/hario/
     *  
     * @Stretch feature: Keep track of the recipe
     */

    //public GameObject nonAREnvironment;
    //public GameObject gameUIPanel;
    public int currentStep;

    private void Start()
    {
        currentStep = 0;
        ScreenLog.Log( "\n" + currentStep.ToString() );
    }

    private void Awake()
    {
        ARController.OnARRunning.AddListener(OnARRunningListener);
    }

    public void CheckDecision(int choice)
    {
        print(choice);
    }

    public void HandleNextStepButton()
    {
        currentStep += 1;
        // function => screenlog

    }

    public void HandlePreviousStepButton()
    {
        currentStep -= 1;
    }

    private void OnARRunningListener(bool ar)
    {
        ScreenLog.Log("OnARRunningListener: " + ar);
        //nonAREnvironment.SetActive(!ar); //places setting for working in non AR

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
