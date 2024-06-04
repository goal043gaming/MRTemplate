using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Created so that the objects can be created in the assets folder
[CreateAssetMenu(fileName = "Objects", menuName = "ObjectProcess")]
public class ObjectList : ScriptableObject
{
    //List in order to store all of the scriptable objects to display a process
    public List<Object> objects;
}
