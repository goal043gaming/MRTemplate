using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowStop : MonoBehaviour
{
    [SerializeField] Flood flood;
    [SerializeField] FlowHandler flowHandler;

    public bool ButtonEnabled = false;
    public ParticleSystem activeEffect;

    [Header("Debug")]
    public bool PressButton = false;
   public void ButtonPress()
    {
        if (ButtonEnabled)
        {
            flood.flooding = false;
            flowHandler.allowMovement = false;
            activeEffect.Stop();
        }
    }

    public void Update()
    {
        if(PressButton)
        {
            ButtonPress();
        }
    }
}
