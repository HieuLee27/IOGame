using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disable : MonoBehaviour
{
    public Slider[] sliders;

    void Start()
    {
        DisableSliders();
    }

    public void DisableSliders()
    {
        for(int i = 0; i < sliders.Length; i++)
        {
            sliders[i].interactable = false;
        }
    }
}
