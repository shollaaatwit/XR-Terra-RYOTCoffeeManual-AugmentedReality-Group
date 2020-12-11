using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsObject : Singleton<StepsObject>
{
    [SerializeField] public static int totalSteps = 13; // 0 indexed
    // TODO replace List with Stack
    public static List<Step> stepsList;


    //new void Awake()

    public Stack<_Step> stepStack = new Stack<_Step>(); // instantiates new Stack with datatype _Step

    private int count = 0;

    public _Step steps;
    private _Step hold;


    public bool IsAtEnd()
    {
        if(count == steps.instructions.Length-1)
        {
            return true;
        }
        return false;
    }

    public _Step CurrentStep()
    {
        return stepStack.Peek();
    }
    public void NextStep()
    {
        count++;
    }
    public void PreviousStep()
    {
        stepStack.Pop();
        count--;
    }

    public void PlayStep() //activates current step
    {
        NextStep();
        CurrentStep();
        Debug.Log(CurrentStep().instructions[count]);
        Debug.Log(count);
        ScreenLog.Log("\n" + CurrentStep().instructions[count]);
    }

    void Awake() //fills stack with first step
    {
        stepStack.Push(steps);
    }
    void Start() 
    {
        Debug.Log(CurrentStep().instructions[count]);
        Debug.Log(count);
        ScreenLog.Log("\n" + CurrentStep().instructions[count]);
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
