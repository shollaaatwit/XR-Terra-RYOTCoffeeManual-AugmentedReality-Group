using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenLog : Singleton<ScreenLog>
{
    //public GameController controller;
    public Text logText;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void _Log(string msg)
    {
        logText.text += msg;
        Debug.Log(msg);

    }

    public static void Log(string msg)
    {
        Instance._Log(msg);

    }

    public static void RemoveLog(string msg)
    {
        
        Instance._RemoveLog(msg);
    }

    public void _RemoveLog(string msg)
    {
        logText.text.Replace(msg, "");
        Debug.Log(msg + " Removed");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
