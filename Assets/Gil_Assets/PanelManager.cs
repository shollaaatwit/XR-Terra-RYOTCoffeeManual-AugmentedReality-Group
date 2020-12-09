using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject WelcomePanel;
    public GameObject SizeChoicePanel;
    public GameObject InstructionsPanel;
    public GameObject ResetPanel;
    public GameObject TimerPanel;
    public GameObject FinalPanel;
    public Text ARReady;

    void Awake()
    {
        ARController.OnARRunning.AddListener(CheckForAR);
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void CheckForAR(bool ar)
    {
       // yield return new WaitForSeconds(3);
       // @TODO check condition
       if (ar)
       {
           // @ TODO Possibly add a coroutine for 1sec
           ARReady.text = "AR is Ready";
           WelcomePanel.SetActive(false);
           ARReady.gameObject.SetActive(false);
               SizeChoicePanel.SetActive(true);
           ScreenLog.Log("\n AR is Ready --");
       }
        else
       {
           ARReady.text = "AR Not Supported";
           ScreenLog.Log("\n AR not supported");
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
        WelcomePanel.SetActive(false);
        InstructionsPanel.SetActive(false);
        TimerPanel.SetActive(false);
        ResetPanel.SetActive(false);
        FinalPanel.SetActive(false);
        SizeChoicePanel.SetActive(true);
    }

    public void HandleLastStep()
    {
        InstructionsPanel.SetActive(false);
        TimerPanel.SetActive(false);
        FinalPanel.SetActive(true);
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
