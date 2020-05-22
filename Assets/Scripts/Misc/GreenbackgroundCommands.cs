using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenbackgroundCommands : MonoBehaviour
{
    public CameraMovement myCam;

    public void WhenFinishingGreen()
    {
        myCam.ToBeStarted();
    }
}
