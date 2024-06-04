using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveHandler : MonoBehaviour
{
    //Text field used to change the text based on input from other scripts
    [SerializeField] TMP_Text textUpdate;

    //Audio that plays once the objective has changed
    [SerializeField] AudioSource nextTargetAudio;

    //Function that gets called by a wide array of different scripts like the placementhandler and the flowhandler
    //Updates the text variable to input from the function call and plays audio effect
    public void UpdateText(string text)
    {
        textUpdate.text = text;
        nextTargetAudio.Play();
    }
}
