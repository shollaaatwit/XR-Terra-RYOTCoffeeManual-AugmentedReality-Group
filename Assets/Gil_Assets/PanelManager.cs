using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject WelcomePanel;
    public GameObject SizeChoicePanel;
    public GameObject InstructionsPanel;
    public GameObject ResetPanel;
    public GameObject TimerPanel;
    public GameObject FinalPanel;

    void Start()
    {
        if (!WelcomePanel.activeInHierarchy)
        {
            WelcomePanel.SetActive(true);
        }

        // Can be replaced for unity event
        StartCoroutine(CheckForAR());
    }

    void Update()
    {
        
    }

    IEnumerator CheckForAR()
    {
        yield return new WaitForSeconds(3);
        WelcomePanel.SetActive(false);
        SizeChoicePanel.SetActive(true);
    }

    public void ConfirmSize()
    {
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
