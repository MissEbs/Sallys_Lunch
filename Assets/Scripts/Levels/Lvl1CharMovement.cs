using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl1CharMovement : MonoBehaviour
{
    public Level1OutcomeV2 myLevel;
    public bool Move;
    private Animator myAni;

    [Header("Character Movement")]
    public float speed;
    [SerializeField] float Mainspeed = 0;
    public Vector3 HomePos;
    public Vector3 AnimateMoveSpot;

    //public string newWords;

    private void Start()
    {
        myAni = gameObject.GetComponent<Animator>();
    }

    public void ChangePositionX(float x)
    {
        Move = true;
        AnimateMoveSpot.x = x;
    }

    public void ChangePositionAgainY(float y)
    {
        Move = true;
        AnimateMoveSpot.y = y;
    }

    public void ChangeSpeed(float NewSpeed)
    {
        speed = NewSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Move)
        {
            //myAni.enabled = false;
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // calculate distance to move

            if (AnimateMoveSpot != Vector3.zero)
            {
                transform.position = Vector3.MoveTowards(transform.position, AnimateMoveSpot, step);

                if (Vector3.Distance(transform.position, AnimateMoveSpot) < 0.001f)
                {
                    Move = false;
                    AnimateMoveSpot = Vector3.zero;
                    // Swap the position of the cylinder.    
                    //myAni.enabled = true;
                    //myAni.Play("Nothing");
                    //refreshOptions("Empty");
                    speed = Mainspeed;
                    
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, HomePos, step);

                if (Vector3.Distance(transform.position, HomePos) < 0.001f)
                {
                    // Swap the position of the cylinder.    
                    //myAni.enabled = true;
                    myAni.Play("Nothing");
                    refreshOptions("Empty");
                    speed = Mainspeed;
                    Move = false;
                }
            }          
            
        }
    }

    public void refreshOptions(string newWords)
    {
        myLevel.ChangeStory(newWords);
    }
}
