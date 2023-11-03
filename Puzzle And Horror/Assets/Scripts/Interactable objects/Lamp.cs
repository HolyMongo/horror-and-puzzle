using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lamp : MonoBehaviour, I_Interactable
{
    private Light lightsource;
    [SerializeField] private string popUpText;
    private void Start()
    {
        lightsource = gameObject.GetComponent<Light>();
    }
    public void Interact()
    {
        lightsource.color = Random.ColorHSV();
    }
    
    public void LookAt(out string popupText)
    {
        popupText = popUpText;
       
    }
}
