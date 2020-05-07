using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopChecks : MonoBehaviour
{
    public bool[] Zones;

    public void ChangeZone(int zoneNumber, bool setState)
    {
        Zones[zoneNumber] = setState;
    }
}
