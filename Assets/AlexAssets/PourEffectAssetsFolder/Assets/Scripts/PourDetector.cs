using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public GameObject kettle;
    public float pourThreshold;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    private bool isPouring = false;
    private Stream currentStream = null;

    public static BooleanEvent onPouringEvent = new BooleanEvent();

    private void Update()
    {
        bool pourCheck = CalculatePourAngle() < pourThreshold;

        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }
    }

    private void StartPour()
    {
        print("Start pour");
        currentStream = CreateStream();
        currentStream.Begin();
        onPouringEvent.Invoke(true);
    }

    private void EndPour()
    {
        print("End pour");
        currentStream.End();
        currentStream = null;
    }

    private float CalculatePourAngle()
    {
        // print("transform.rotation.x " + kettle.transform.rotation.x * Mathf.Rad2Deg);
        // print("transform.rotation.y " + transform.rotation.y);
        // print("transform.rotation.z " + transform.rotation.z);
        return kettle.transform.rotation.x * Mathf.Rad2Deg;
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }
}