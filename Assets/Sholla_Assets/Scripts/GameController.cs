using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
// using UnityEditor.UIElements;
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
    public string coffeeAmount;
    public string waterAmount;

    public bool choiceConfirmed;
    public TextMeshProUGUI StepInstructionText;

    private List<Step> recipeList;

    public PanelManager panelManager;

    public static BooleanEvent OnChoosenCoffeeAmountEvent = new BooleanEvent();
    public StackController stackController;

    public PlaceObjectsOnPlane placeObjectsOnPlane;
    public GameObject timerPanel;

    private void Start()
    {
        //stackController = GetComponent<StackController>();
        recipeList = StepsObject.stepsList;
        choiceConfirmed = false;
        currentStep = 0;
        coffeeSizeChoice = 0;
        ScreenLog.Log("\n" + currentStep.ToString());
    }

    private new void Awake()
    {
        ARController.OnARRunning.AddListener(OnARRunningListener);
        PanelManager.ResetInstructions.AddListener(ResetGameLogic);
        PlaceObjectsOnPlane.onObjectPlacedEvent.AddListener(FindStackController);
    }

    private void Update()
    {
       HandleResetLocationButton();
    }

    public void FindStackController(bool objectPlaced)
    {
        if (objectPlaced)
        {
            ScreenLog.Log("object placed, maybe found");
            stackController = GameObject.Find("FullObjectStack").GetComponent<StackController>();
        }
    }

    private void ResetGameLogic(bool reset)
    {
        if (reset)
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
                waterAmountCups = 2.75f;
                break;

            default:
                coffeeAmountTbs = 4f;
                waterAmountCups = 1.75f;
                break;
        }
        ScreenLog.Log("\n" + coffeeAmountTbs + "\n" + waterAmountCups);
        
        coffeeAmount = ConvertToVolumes("coffee", coffeeAmountTbs);
        waterAmount = ConvertToVolumes("water", waterAmountCups);
        OnChoosenCoffeeAmountEvent.Invoke(true);
        ReadSteps();
    }

    public void HandleNextStepButton()
    {
        if (currentStep == 0)
        {
            panelManager.ChangePanelAlpha(true);
        } 
        

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
        if (currentStep == 1)
        {
            panelManager.ChangePanelAlpha(false);
        }

        if (currentStep > 0)
        {
            currentStep -= 1;
            ReadSteps();
        }

    }

    public void HandleResetLocationButton()
    {
         
        if (recipeList[currentStep].isSecondStep)
        {
            panelManager.ResetCupLocationButton(true);
        }
        else
        {
            panelManager.ResetCupLocationButton(false);
        }
    }
    public void OnResetLocationButton()
    {
        placeObjectsOnPlane.placedObject = true;
        placeObjectsOnPlane.spawnedObject?.SetActive(false);
        ScreenLog.Log("\n OnResetLocation");
    }

    public string ConvertToVolumes(string type, float amount)
    {
        if (type == "coffee")
        {
            // return int 1 or 2 if theres decimals, else returns 0 
            int teaspoonAmount = !(amount % 1 == 0) ? Int16.Parse(amount.ToString("0.00").Split('.')[1]) / 33 : 0;

            return amount.ToString("0") + " Tablespoons" +
                   ((teaspoonAmount != 0) ? " and " + teaspoonAmount.ToString() + " teaspoons" : "");
        }
        // for the boil this amount of water
        // will that be step 1 | 0? 
        if (type == "water")
        {
            // return 1-3
            int quaterCupsAmount = !(amount % 1 == 0) ? Int16.Parse(amount.ToString("0.00").Split('.')[1]) / 25 : 0;

            //return "cups";
            return amount.ToString("0") + " cups" +
                   ((quaterCupsAmount != 0) ? " and " + quaterCupsAmount.ToString() + "/4 cups" : "");
        }

        return "";
    }

    private void ReadSteps()
    {

        StepInstructionText.text = recipeList[currentStep].stepInstruction;

        if (recipeList[currentStep].animationFunction.Length > 0 && stackController)
        {
            //Invoke(recipeList[currentStep].animationFunction, 0);
            stackController.InvokeAnimation(recipeList[currentStep].animationFunction);
        }

        if (recipeList[currentStep].isTimerInView && !timerPanel.activeInHierarchy)
        {
            panelManager.toggleTimerPanel();
        }
    }

    private void OnARRunningListener(bool ar)
    {
        ScreenLog.Log("OnARRunningListener: " + ar);
        //nonAREnvironment.SetActive(!ar); //places setting for working in non AR

        if (ar)
        {
            //do something if AR is active

        }
        else
        {
            //do something if AR isn't active
        }
    }

}