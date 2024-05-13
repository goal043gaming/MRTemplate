using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Step", menuName = "Step")]
public class Step : ScriptableObject
{
    public string stepDescription;
    public Image stepImage;
    public bool stepIsActive;
}
