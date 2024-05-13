using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Objects", menuName = "Objects")]
public class Object : ScriptableObject
{
    public string attachedObjective;
    public Sprite attachedSprite;
    public bool hasMicrogame;
    public string identifier;

    [Header("Microgames")]
    public FlowHandler flowHandler;
    public QuestionManager questionManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
