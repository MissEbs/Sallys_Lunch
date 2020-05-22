using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodAI : MonoBehaviour
{
    [Header("Movement Stats")]
    public float speed;
    public bool Move;
    public Vector2 inputVector;
    Vector2 movement;
    bool[] activeAreas;
    [SerializeField] Rigidbody2D rbody = null;
    [SerializeField] Vector2 waitTime = Vector2.zero;

    private void Start()
    {
        movementTime();
    }

    public void MoveAheadUp()
    {
        rbody.velocity = new Vector2(0, speed);
    }
    public void MoveAheadRight()
    {
        rbody.velocity = new Vector2(speed, 0);
    }
    public void MoveAheadDown()
    {
        rbody.velocity = new Vector2(0, -speed);
    }
    public void MoveAheadLeft()
    {
        rbody.velocity = new Vector2(-speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            StopChecks stop = collision.GetComponent<StopChecks>();
            activeAreas = stop.Zones;

            rbody.velocity = new Vector2(0, 0);

            if (stop.KeepGoing)
            {
                for (int i = 0; i < activeAreas.Length; i++)
                {
                    if (activeAreas[i] == false && stop.EnterZones[i] == false)
                    {
                        if (i == 0)
                        {
                            MoveAheadUp();
                        }
                        if (i == 1)
                        {
                            MoveAheadRight();
                        }
                        if (i == 2)
                        {
                            MoveAheadDown();
                        }
                        if (i == 3)
                        {
                            MoveAheadLeft();
                        }
                    }
                }
                stop.ResetEnterZone();
            }

            else
            {
                StartCoroutine(movementTime());
            }
        }

        if (collision.name == "Player") //if hitting the player
        {

            Application.LoadLevel("Level1_Horz");
        }
    }

    IEnumerator movementTime()
    {
        float waittime = Random.Range(waitTime.x, waitTime.y);
        yield return new WaitForSeconds(waittime);
        pickNumb();

        if (Numbpicked == 0)
        {
            MoveAheadUp();
        }
        if (Numbpicked == 1)
        {
            MoveAheadRight();
        }
        if (Numbpicked == 2)
        {
            MoveAheadDown();
        }
        if (Numbpicked == 3)
        {
            MoveAheadLeft();
        }
    }

    private int Numbpicked;

    private void pickNumb()
    {
        Numbpicked = Random.Range(0, 4);
        if (activeAreas[Numbpicked] == true)
        {
            pickNumb();
        }
    }
}