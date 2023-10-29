using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lamp : MonoBehaviour, I_Interactable
{
    private Light lightsource;
    [SerializeField] private string popUpText;

    [Header("temporary vairables that may move to another script")]
    [SerializeField] TextMeshProUGUI crossairTextBar;
    private void Start()
    {
        lightsource = gameObject.GetComponent<Light>();
    }
    public void Interact()
    {
        lightsource.color = Random.ColorHSV();
    }
    
    public void LookAt()
    {
        if (crossairTextBar != null && popUpText != null)
        {
            crossairTextBar.text = popUpText;
        }
    }
}
