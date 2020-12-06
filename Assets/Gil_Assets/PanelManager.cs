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
        InstructionsPanel.SetActive(false);
        ResetPanel.SetActive(!ResetPanel.activeInHierarchy);
    }

    public void toggleTimerPanel()
    {
        TimerPanel.SetActive(!TimerPanel.activeInHierarchy);
    }
}
