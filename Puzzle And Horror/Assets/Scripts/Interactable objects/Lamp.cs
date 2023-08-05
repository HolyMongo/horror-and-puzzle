using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, I_Interactable
{
    private Light lightsource;
    private void Start()
    {
        lightsource = gameObject.GetComponent<Light>();
    }
    public void Interact()
    {

        //lightsource.color = new Color32(Random.Range(0, 256), 0, 0, 0);
        //lightsource.color = new Color32(Random.Range(0, 13), 0, 0, 0);
        lightsource.color = Random.ColorHSV();
    }
}
