using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsObject : Singleton<StepsObject>
{
    [SerializeField] public static int totalSteps = 13; // 0 indexed
    public GameObject[] stepsAnimations = new GameObject[totalSteps];
    public List<Step> stepsList;


    void Start() 
    {
        stepsList = new List<Step>();
        FillList();
    }

    // Default recipe 
    private Dictionary<int, string> _stepStrings = new Dictionary<int, string>()
    {
        {0, "Tap to place mug"},
        {3, "Don't add coffee yet!"},
        {6, "Time your brew"},
        {7, "Cover grounds w/ water"},
        {8, "Make sure "}, //@Todo
        {10, "Looking good!"},
        {11, "Fill up the Hario"},
        {12, "Use any remaining water"},
    };


    private void FillList()
    {
        for (int i = 0; i <= totalSteps; i++)
        {
            stepsList.Add( new Step
                {
                    stepInstruction = CheckDictionary(i),
                    isLastStep = (i == totalSteps),
                    isTimerInView = (i >= 6) 
                }
            );
            Debug.Log(stepsList[i].stepInstruction);
        }
    }

    private string CheckDictionary(int i)
    {
        if (_stepStrings.ContainsKey(i) )
        {
            return _stepStrings[i];
        }
        else
        {
            return "";
        }
    }

}
