using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    
    public Text timerText;
    public TMP_Text buttonText;
    public bool isTimerOn = false;
    private float theTime = 0;
    private string _initialButtonText;
    [SerializeField] private float _speed;
    void Start()
    {
        _speed = 1f;
        _initialButtonText = buttonText.text;
    }

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        if (isTimerOn)
        {
            theTime += Time.deltaTime * _speed;
            string minutes = Math.Floor((theTime % 3600) / 60).ToString("00");
            string seconds = (theTime % 60).ToString("00");

            timerText.text = minutes + ":" + seconds;
        }
    }

    public void HandleTimerButton()
    {
        
        if (!timerText.isActiveAndEnabled)
        {
            timerText.gameObject.SetActive(true); 
        }
        isTimerOn = !isTimerOn;

        buttonText.text = isTimerOn ? "Pause" : "Resume";
    }

    private void ResetTimer()
    {
        theTime = 0f;
        isTimerOn = false;
        buttonText.text = _initialButtonText;
        _speed = 1f;
    }

}
