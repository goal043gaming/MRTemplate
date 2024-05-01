using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveHandler : MonoBehaviour
{
    [SerializeField] TMP_Text textUpdate;
    [SerializeField] AudioSource nextTargetAudio;

    public void UpdateText(string text)
    {
        textUpdate.text = text;
        nextTargetAudio.Play();
    }
}
