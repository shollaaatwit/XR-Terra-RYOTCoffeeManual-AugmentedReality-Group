using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : Singleton<PanelManager>
{
    public  GameObject WelcomePanel;
    public  GameObject SizeChoicePanel;
    public  GameObject InstructionsPanel;
    public  GameObject ResetPanel;
    public  GameObject TimerPanel;
    public  GameObject FinalPanel;
    public  Text ARReady;

    public static BooleanEvent ResetInstructions = new BooleanEvent();

    public Image imageBackground;
    public Color instructionsColor;


    private new void Awake()
    {
        ARController.OnARRunning.AddListener(CheckForAR);
        ARController.OnPlaneScannedEvent.AddListener(IsPlaneScanned);
    }

    private void Start()
    {
        if (WelcomePanel && !WelcomePanel.activeInHierarchy)
        {
            WelcomePanel.SetActive(true);
        }
        imageBackground = InstructionsPanel.GetComponent<Image>();
        instructionsColor = imageBackground.color;
    }


    private   void CheckForAR(bool ar)

    {
        if (ar) 
        {
           // @ TODO Possibly add a coroutine for 1sec
           ARReady.text = "AR is Ready";
           WelcomePanel.SetActive(false);
           //ARReady.gameObject.SetActive(false);
        }
        else
        {
            if (WelcomePanel && ARReady)
            {
                ARReady.text = "AR Not Supported";
                ScreenLog.Log("\n AR not supported");
            }
           
        }
    }

    private  void IsPlaneScanned(bool planeReady)
    {
        if (planeReady)
        {
            SizeChoicePanel.SetActive(true);
        }
    }

    public  void ConfirmSize()
    {
        ARReady.gameObject.SetActive(false);
        SizeChoicePanel.SetActive(false);
        InstructionsPanel.SetActive(true);
    }

    public  void handleResetButton()
    {
        InstructionsPanel.SetActive(!InstructionsPanel.activeInHierarchy);
        ResetPanel.SetActive(!ResetPanel.activeInHierarchy);
    }

    public  void toggleTimerPanel()
    {
        TimerPanel.SetActive(!TimerPanel.activeInHierarchy);
    }
    // Takes you to the choice page
    public  void ResetAppPanels()
    {
        // set gameManger's currentStep = 0;
        WelcomePanel.SetActive(false);
        InstructionsPanel.SetActive(false);
        TimerPanel.SetActive(false);
        ResetPanel.SetActive(false);
        FinalPanel.SetActive(false);
        SizeChoicePanel.SetActive(true);
        ResetInstructions.Invoke(true);
    }

    public  void HandleLastStep()
    {
        InstructionsPanel.SetActive(false);
        TimerPanel.SetActive(false);
        FinalPanel.SetActive(true);
    }

    public void ChangePanelAlpha(bool isNext)
    {
        if (isNext)
        {
            imageBackground.color = new Color(imageBackground.color.r, imageBackground.color.g, imageBackground.color.b, 0f);
        }
        else
        {
            imageBackground.color = instructionsColor;
        }
    }

    public void EndAppSession()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
