using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StepsObject : Singleton<StepsObject>
{
    [SerializeField] public static int totalSteps = 13; // 0 indexed
    // TODO replace List with Stack
    public static List<Step> stepsList;

    private GameController gameController;
    private static string _coffeeAmountString;




    void Awake() //fills stack with first step
    {
        stepsList =  new List<Step>();
        FillList();
        GameController.OnChoosenCoffeeAmountEvent.AddListener(GetVolumes);
    }
    void Start() 
    {
        gameController = GetComponent<GameController>();
    }

    public void GetVolumes(bool ready)
    {
        if (ready)
        {
            // hardcoded
            stepsList[6].stepInstruction = $"Add {gameController.coffeeAmount} of coffee";
            // change step 0 for water
        }
    }

    // Default recipe 
    private Dictionary<int, string> _stepStrings = new Dictionary<int, string>()
    {
        {0, "Boil X Water"},
        {1, "Tap to place mug"},
        {4, "Don't add coffee yet!"},
        {6, $"Add {_coffeeAmountString} of coffee" },
        {7, "Time your brew"},
        {8, "Cover grounds w/ water"},
        {9, "Make sure to wet all grounds"}, //@Todo
        {10, "Point camera at your grounds - hit next"},
        {11, "Looking good!"},
        {12, "Fill up the Hario"},
        {13, "Use any remaining water"},
    };


    private Dictionary<int, string> _stepAnimationFunctions = new Dictionary<int, string>()
    {
        {2, "ShowHarioPlacementAnimation"},
        {3, "ShowFilterPlacementAnimation"},
        {4, "FirstWaterPour"}, //PourWaterAnimation
        //@TODO step 5 pour out water replace with sink
        {5, "StopPouringWaterAnimation"},
        {6, "PourCoffeeGroundsAnimation"},
        {7, "FinishAddingCoffeeGroundsStep"},
        {8, "PourWaterAnimation"},
        {9, "ShowHands"},
        {10, "HideHands"},
        {12, "PourWaterAnimation"},
        {13, "PourWaterAnimation"}
    };

 


    private void FillList()
    {
        for (int i = 0; i <= totalSteps; i++)
        {
            stepsList.Add( new Step
                {
                    animationFunction = CheckFuncDictionary(i) ,
                    stepInstruction = CheckDictionary(i),
                    isLastStep = (i == totalSteps),
                    isTimerInView = (i >= 7) 
                }
            );
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

    private string CheckFuncDictionary(int i)
    {
        if (_stepAnimationFunctions.ContainsKey(i))
        {
            return _stepAnimationFunctions[i];
        }
        else
        {
            return "";
        }
    }

}
