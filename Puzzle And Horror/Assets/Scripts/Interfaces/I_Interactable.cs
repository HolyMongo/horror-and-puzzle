using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Interactable
{
    void Interact();
    void LookAt(out string popupText);
}
