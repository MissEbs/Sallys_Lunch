using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Stats")]
    public float speed;
    //public bool Move;
    public Vector2 inputVector;
    Vector2 movement;
    [SerializeField] Rigidbody2D rbody = null;
    [SerializeField] GameObject[] Buttons;

    //void Updatespace()
    //{
    //    if (Move)
    //    {
    //        //movement = inputVector * speed * Time.deltaTime;
    //        //rbody.MovePosition(rbody.position + inputVector * speed * Time.deltaTime);
            
    //    }

    //    else
    //    {
            
    //    }

    //}

    public void MoveAheadUp()
    {
        rbody.velocity = new Vector2(0, speed);
       // Move = true; //are we moving?
        foreach (GameObject gam in Buttons)
        {
            gam.SetActive(false);
        }
    }
    public void MoveAheadRight()
    {
        rbody.velocity = new Vector2(speed, 0);
        //Move = true; //are we moving?
        foreach (GameObject gam in Buttons)
        {
            gam.SetActive(false);
        }
    }
    public void MoveAheadDown()
    {
        rbody.velocity = new Vector2(0, -speed);
        //Move = true; //are we moving?
        foreach (GameObject gam in Buttons)
        {
            gam.SetActive(false);
        }
    }
    public void MoveAheadLeft()
    {
        rbody.velocity = new Vector2(-speed, 0);
        //Move = true; //are we moving?
        foreach (GameObject gam in Buttons)
        {
            gam.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            //Move = false;
            bool[] activeAreas;
            activeAreas = collision.GetComponent<StopChecks>().Zones;

            for (int i = 0; i < Buttons.Length; i++)
            {
                if (activeAreas[i] == false)
                {
                    Buttons[i].SetActive(true);
                }
            }
            rbody.velocity = new Vector2(0, 0);
        }
    }
}
