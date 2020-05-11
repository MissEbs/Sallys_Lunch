using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopChecks : MonoBehaviour
{
    public bool[] Zones;
    public bool[] EnterZones;
    public bool KeepGoing;

    public void ChangeZone(int zoneNumber, bool setState)
    {
        Zones[zoneNumber] = setState;
    }

    public void ChangeZoneEnter(int zoneNumber, bool setState)
    {
        ResetEnterZone();
        EnterZones[zoneNumber] = setState;    
    }

    public void ResetEnterZone()
    {
        for (int i = 0; i < EnterZones.Length; i++)
        {
            EnterZones[i] = false;
        }
    }
}

