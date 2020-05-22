using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickingObjects : MonoBehaviour
{

    [SerializeField] GameObject Buttons;

    public void TurningButtonsOnandOff()
    {
        if (Buttons.activeInHierarchy)
        {
            Buttons.SetActive(false);
        }
        else
        {
            Buttons.SetActive(true);
        }
    }
}
