using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlArrow : MonoBehaviour
{

    
    [Header("New Varibles")]
    [SerializeField] Vector3 newHomepos = Vector3.zero; //players new home pos
    [SerializeField] string newPathString = null; //place where the story goes to.\
    [SerializeField] Vector2 CameraMaxPosition = Vector2.zero; //x and y
    [SerializeField] Vector2 CameraMinPosition = Vector2.zero;
    public Transform[] newButtonSpots;
    [SerializeField] int lastSceneInt = 1;
    [SerializeField] int newSceneInt = 1;
    [SerializeField] float newSpeed = 1;

    [Header("Need to touch")]
    [SerializeField] Lvl1CharMovement characterMove = null;
    [SerializeField] Level1Outcome ourTalker = null;
    [SerializeField] CameraMovement camTalk = null;

    public void ChangeingZones()
    {
        //change camera settings
        camTalk.minPosition = CameraMinPosition;
        camTalk.maxPosition = CameraMaxPosition;
        //Sets players new home pos 
        characterMove.HomePos = newHomepos;
        //speed the character up
        characterMove.speed = newSpeed;
        //needs to make character move into the next zone
        characterMove.Move = true;
        //Starts next story Knott thingy, set new button spots, set buttons pressed
        ourTalker.SetStoryPos(newPathString, newButtonSpots, lastSceneInt, newSceneInt);
    }
}
