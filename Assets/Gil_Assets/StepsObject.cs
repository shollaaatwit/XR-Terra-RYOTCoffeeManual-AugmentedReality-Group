using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StepsObject : Singleton<StepsObject>
{
    [SerializeField] public static int totalSteps = 15; // 0 indexed
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
            stepsList[0].stepInstruction = $"Set {gameController.waterAmount} of water to boil";
            // change step 0 for water
        }
    }

    // Default recipe 
    private Dictionary<int, string> _stepStrings = new Dictionary<int, string>()
    {
        {0, "Boil X Water"},
        {1, "Tap to place mug"},
        {2, "Place Hario on Mug"},
        {3, "Place filter In Hario"},
        {4, "Don't add coffee yet but wet the filter!"}, // add something about just enough to wet the filter
        {5, "Discard the primer water"},
        {6, $"Add {_coffeeAmountString} of coffee" },
        {7, "Start your timer, then tap next"}, // should end coffee being poured in animation and show timer
        {8, "Cover grounds w/ 1/4 cup water, wait 30 sec"}, // water animation
        {9, "Make sure to wet all grounds with a gentle swirl"}, // swirling animation, maybe make text more explicit
        {10, "Point camera at your grounds - hit next"}, // needs to stop swirling animation
        {11, "Looking good!"},
        {12, "Fill up the Hario"}, // goes to end screen after this
        {13, "StirWithSpoon"},
        {14, "One last gentle swirl"},
    };


    private Dictionary<int, string> _stepAnimationFunctions = new Dictionary<int, string>()
    {
        {2, "PlaceHarioOnMug"},
        {3, "PlaceFilterInHario"},
        {4, "PourInPrimerWater"}, //PourWaterAnimation
        {5, "DiscardPrimerWater"}, // needs to cutoff sound effect if still playing
        {6, "AddCoffeeGrinds"},
        {7, "StopPouringCoffeeGrinds"},
        {8, "FirstWaterPour"},
        {9, "SwirlEntireStack"}, // this fires too early
        {10, "StopSwirlingStack"},
        {12, "SecondaryWaterPour"},
        {13, "StirWithSpoon"}, // needs to have written instructions
        {14, "FinalStackSwirl"},
        {15, "Finish"}
    };

 


    private void FillList()
    {
        for (int i = 0; i <= totalSteps; i++)
        {
            stepsList.Add( new Step
                {
                    animationFunction = CheckFuncDictionary(i) ,
                    stepInstruction = CheckDictionary(i),
                    isSecondStep = (i == 1),
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
