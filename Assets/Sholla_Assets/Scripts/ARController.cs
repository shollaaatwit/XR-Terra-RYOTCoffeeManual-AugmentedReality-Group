using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class BooleanEvent : UnityEvent<bool> { }


public class ARController : MonoBehaviour
{
    public bool allowARMode = true;

    public ARSession arSession;
    public ARSessionOrigin arOrigin;
    public ARPlaneManager planeManager;


    public GameObject scanRoomUIPanel;

    public Camera defaultCamera;

    public bool IsARAvailable => (allowARMode && 
                                ARSession.state != ARSessionState.Unsupported && 
                                ARSession.state != ARSessionState.NeedsInstall);
    public bool IsARRunning => (allowARMode && 
                                ARSession.state >= ARSessionState.Ready && 
                                planeManager.trackables.count > 0);

    public static BooleanEvent OnARRunning = new BooleanEvent();
    public static BooleanEvent OnPlaneScannedEvent= new BooleanEvent();

    public AudioManager audioManager;

    private void Start()
    {
        // TODO replace sound playing in current panel("Loading")
        audioManager = GetComponent<AudioManager>();
        audioManager.PlaySound(3);

#if UNITY_EDITOR
        allowARMode = false;
#endif
        if (allowARMode)
        {
            EnableAR(true);

            StartCoroutine(_WaitForARReady());

        }
        else
        {

            EnableAR(false);
            OnARRunning.Invoke(false);
        }
    }

    public void EnableAR(bool enable)
    {
        if(enable)
        {
            ScreenLog.Log("\nAR mode");
            ScreenLog.RemoveLog("Non-AR mode");
        }
        else
        {
            ScreenLog.Log("\nNon-AR mode");
            ScreenLog.RemoveLog("AR mode");
        }
        arSession.gameObject.SetActive(enable);
        arOrigin.gameObject.SetActive(enable);
        arSession.enabled = enable;

        defaultCamera?.gameObject.SetActive(!enable);
    }

    IEnumerator _WaitForARReady()
    {
        bool checking = true;
        while(checking)
        {
            if(ARSession.state == ARSessionState.Unsupported)
            {
                ScreenLog.Log("\nAR not supported on this device");
                OnARRunning.Invoke(false);
                yield break;
            }        
            if(ARSession.state >= ARSessionState.Ready)
            {   
                checking = false;
                OnARRunning.Invoke(true);
            }
            yield return null;
        }
        ScreenLog.Log("\nAR supported");

      
        ScreenLog.Log("\nTracking planes...");

        /*
         *TODO remove checkForPlanes
         */
       
    }

     public IEnumerator CheckForPlanes()
    {
        _PromptToScan(true);
        while (planeManager.trackables.count == 0)
        {
            yield return null;
        }
        _PromptToScan(false);

        OnPlaneScannedEvent.Invoke(true);
        ScreenLog.Log("\nTracked planes!");
    }

    private void _PromptToScan(bool show)
    {
        scanRoomUIPanel?.SetActive(show);
    }

}
