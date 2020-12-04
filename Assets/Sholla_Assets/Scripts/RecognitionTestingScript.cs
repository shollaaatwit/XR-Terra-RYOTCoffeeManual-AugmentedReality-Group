using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class RecognitionTestingScript : MonoBehaviour
{
    private ARTrackedImageManager _arTrackedImage;

    private void Awake()
    {
        _arTrackedImage = FindObjectOfType<ARTrackedImageManager>();
    }
    public void OnEnable()
    {
        _arTrackedImage.trackedImagesChanged += OnImageChanged;
    }
    public void OnDisable()
    {
        _arTrackedImage.trackedImagesChanged -= OnImageChanged;
    }
    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach(var trackedImage in args.added) //runs through all images
        {
            //do something
        }
    }
}
