using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : Singleton<PanelManager>
{
    public  GameObject LoadingPanel;
    public  GameObject WelcomePanel;
    public  GameObject SizeChoicePanel;
    public  GameObject InstructionsPanel;
    public  GameObject ResetPanel;
    public  GameObject TimerPanel;
    public  GameObject FinalPanel;

    public  GameObject cupLocationButton;

    public  Text ARReady;

    public Image imageBackground;
    public Color instructionsColor;

    public ARController arController;

    public static BooleanEvent ResetInstructions = new BooleanEvent();




    private new void Awake()
    {
        ARController.OnARRunning.AddListener(CheckForAR);
        ARController.OnPlaneScannedEvent.AddListener(IsPlaneScanned);
    }

    private void Start()
    {
        // TODO replace with loading panel
        if (LoadingPanel && !LoadingPanel.activeInHierarchy)
        {
            LoadingPanel.SetActive(true);
        }
        imageBackground = InstructionsPanel.GetComponent<Image>();
        instructionsColor = imageBackground.color;
    }


    private void CheckForAR(bool ar)
    {
        StartCoroutine(DelaySeconds(3, true));
     
        if (ar) 
        {
            ARReady.text = "AR is Ready";
           StartCoroutine(DelaySeconds(4, false));
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

    IEnumerator DelaySeconds(int seconds, bool first)
    {
        yield return new WaitForSeconds(seconds);

        if (first)
        {
            WelcomePanel.SetActive(true);
            LoadingPanel.SetActive(false);
        }
        if (first == false)
        {
            WelcomePanel.SetActive(false);
            StartCoroutine(arController.CheckForPlanes());
        }

    }

   

    private void IsPlaneScanned(bool planeReady)
    {
        if (planeReady)
        {
            SizeChoicePanel.SetActive(true);
        }
    }

    public void ConfirmSize()
    {
        ARReady.gameObject.SetActive(false);
        SizeChoicePanel.SetActive(false);
        InstructionsPanel.SetActive(true);
    }

    public void handleResetButton()
    {
        InstructionsPanel.SetActive(!InstructionsPanel.activeInHierarchy);
        ResetPanel.SetActive(!ResetPanel.activeInHierarchy);
    }

    public void toggleTimerPanel()
    {
        TimerPanel.SetActive(!TimerPanel.activeInHierarchy);
    }
    // Takes you to the choice page
    public void ResetAppPanels()
    {
        // set gameManger's currentStep = 0;
        LoadingPanel.SetActive(false);
        WelcomePanel.SetActive(false);
        InstructionsPanel.SetActive(false);
        TimerPanel.SetActive(false);
        ResetPanel.SetActive(false);
        FinalPanel.SetActive(false);
        SizeChoicePanel.SetActive(true);
        ResetInstructions.Invoke(true);
    }

    public void HandleLastStep()
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

    public void ResetCupLocationButton(bool reset) //turns on or off reset location button
    {
        cupLocationButton.SetActive(reset);
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
