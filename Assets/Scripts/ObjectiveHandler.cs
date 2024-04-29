using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveHandler : MonoBehaviour
{
    [SerializeField] TMP_Text textUpdate;

    public void UpdateText(string text)
    {
        textUpdate.text = text;
    }
}
