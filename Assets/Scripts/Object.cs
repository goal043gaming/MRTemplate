using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Created so that the objects can be created in the assets folder
[CreateAssetMenu(fileName = "Objects", menuName = "Objects")]
public class Object : ScriptableObject
{
    //String that changes the objective based on the current object
    public string attachedObjective;

    //Sprite object that changes the current sprite to the linked object, used for the P&ID drawings
    public Sprite attachedSprite;

    //String used to find the right in game object that is the same as this object
    public string identifier;

    //String that is used to display feedback after selecting the game object
    public string feedbackText;

    //NO LONGER USED
    [Header("Microgames")]
    public bool hasMicrogame;
    public bool flowGame;
    public bool ballGame;

    //NO LONGER USED
    [Header(" Overall")]
    public bool isLast;
}
