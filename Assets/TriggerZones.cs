﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZones : MonoBehaviour
{

    [SerializeField] StopChecks mystop = null;
    [SerializeField] int myPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            mystop.ChangeZone(myPos, true);
        }
    }
}
