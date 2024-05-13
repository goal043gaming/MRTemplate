using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Process", menuName = "Process")]
public class StepProcess : ScriptableObject
{
    public Step[] steps;
}
