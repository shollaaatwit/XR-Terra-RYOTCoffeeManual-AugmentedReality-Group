using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step 
{
    public string stepInstruction { get; set; }
    public bool isLastStep { get; set; }
    public bool isTimerInView { get; set; }
    // @Todo
    // add anim Type

    /* bools for prefab-stack */
    public bool isHarioEnabled { get; set; }
    public bool isFilterEnabled { get; set; }
    public bool isCoffeeAdded { get; set; }

}
