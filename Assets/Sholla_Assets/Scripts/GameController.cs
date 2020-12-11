using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

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
    public int coffeeSizeChoice;
    public int currentStep;
    public float coffeeAmountTbs;
    public float waterAmountCups;

    public bool choiceConfirmed;
    public TextMeshProUGUI StepInstructionText;

    private List<Step> recipeList;

    public PanelManager panelManager;

    private void Start()
    {
        recipeList = StepsObject.stepsList;
        choiceConfirmed = false;
        currentStep = 0;
        coffeeSizeChoice = 0;
        ScreenLog.Log( "\n" + currentStep.ToString() );
    }

    private new void Awake()
    {
        ARController.OnARRunning.AddListener(OnARRunningListener);
        PanelManager.ResetInstructions.AddListener(ResetGameLogic);
    }

    private void Update()
    {
   
    }

    private void ResetGameLogic(bool reset)
    {
        if(reset)
        {
            choiceConfirmed = false;
            currentStep = 0;
            coffeeSizeChoice = 0;
        }
    }

    /*
     * 0 = 12 ounces mug size
     * 1 = 14 ounces => 
     * 2 = 16 ounces
     *
     * 2Tbs per 6oz
     * 8oz = 1cup
     * (Add .25 cup of water)
     */
    public void CheckDecision(int choice)
    {
        coffeeSizeChoice = choice;
    }

    public void ConfirmChoice()
    {
        switch (coffeeSizeChoice)
        {
            // Every 2 ounces, we add 2/3s of Coffee in TB, and .25 in cups of water(.50 total for warming);
            // 12 ounces
            case 0:
                coffeeAmountTbs = 4f;
                waterAmountCups = 1.75f;
                break;
            // 14 ounces
            case 1:
                coffeeAmountTbs = 4.66f;
                waterAmountCups = 2.25f;
                break;
            // 16 ounces
            case 2:
                coffeeAmountTbs = 5.33f;
                waterAmountCups = 2.25f;
                break;

            default:
                coffeeAmountTbs = 4f;
                waterAmountCups = 2f;
                break;
        }
        ScreenLog.Log("\n" + coffeeAmountTbs + "\n" + waterAmountCups);
        print(coffeeSizeChoice);
        ReadSteps();
    }

    public void HandleNextStepButton()
    {
        if (!recipeList[currentStep + 1].isLastStep)
        {
            currentStep += 1;
            // function => screenlog
            ReadSteps();
        }
        else
        {
            panelManager.HandleLastStep();
           // PanelManager.HandleLastStep();
        }
       
    }

    public void HandlePreviousStepButton()
    {
        if (currentStep > 0)
        {
            currentStep -= 1;
            ReadSteps();
        }
       
    }

    private void ReadSteps()
    {
       
            StepInstructionText.text = recipeList[currentStep].stepInstruction;

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
