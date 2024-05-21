using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Objects", menuName = "Objects")]
public class Object : ScriptableObject
{
    public string attachedObjective;
    public Sprite attachedSprite;
    public string identifier;

    public string feedbackText;

    [Header("Microgames")]
    public bool hasMicrogame;
    public bool flowGame;
    public bool ballGame;

    [Header(" Overall")]
    public bool isLast;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
