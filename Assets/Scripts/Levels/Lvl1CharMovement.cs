using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl1CharMovement : MonoBehaviour
{
    public Level1Outcome myLevel;
    public bool Move;
    private Animator myAni;

    [Header("Character Movement")]
    public float speed;
    [SerializeField] float Mainspeed = 0;
    public Vector3 HomePos;

    private void Start()
    {
        myAni = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Move)
        {
            myAni.enabled = false;
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, HomePos, step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, HomePos) < 0.001f)
            {
                // Swap the position of the cylinder.                
                refreshOptions();
                speed = Mainspeed;
                Move = false;
            }
        }
    }

    public void refreshOptions()
    {
        myLevel.RefreshView();
    }
}
