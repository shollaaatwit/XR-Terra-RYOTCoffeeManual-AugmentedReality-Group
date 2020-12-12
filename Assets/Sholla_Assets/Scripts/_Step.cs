using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class _Step
{
    public string[] stepNames;

    public GameObject[] stepsAnimations;

    [TextArea(2, 10)]
    public string[] instructions;

}
