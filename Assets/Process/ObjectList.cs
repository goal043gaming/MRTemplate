using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objects", menuName = "ObjectProcess")]
public class ObjectList : ScriptableObject
{
    public List<Object> objects;
}
