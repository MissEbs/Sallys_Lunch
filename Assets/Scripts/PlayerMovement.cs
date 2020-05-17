using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Stats")]
    public float speed;
    public bool Move;
    public Vector2 inputVector;
    Vector2 movement;
    bool[] activeAreas;
    float Timer = 0, Timer2 = 0;
    [SerializeField] Animator animator = null;

    [Header("Touch Drag")]
    private GameObject CircleObject;
    [SerializeField] Rigidbody2D rbody = null;
    [SerializeField] GameObject[] Buttons = null; // up, right, down, left
    [SerializeField] Text displaycorner = null;
 
    private Vector2 startPos, endPos, Direction; //touch start pos, touch end pos, swipe direction

    [Header("She angry")]
    [SerializeField] Vector2 waitTime = Vector2.zero;
    public bool WalkSelf;
    [SerializeField] float TimerbeforeYouCantalktoHer = 1;
    public GameObject TalkToSallyButton;

    private void Start()
    {
        if (WalkSelf == true)
        {           
            foreach (GameObject gam in Buttons)
            {
                gam.SetActive(false);
            }
            movementTime();
        }
    }

    private void Update()
    {
        //if you touch the screen
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !WalkSelf)
        {
            //if (CircleObject != null)
            //{
            //    Destroy(CircleObject);
            //}
            startPos = Input.GetTouch(0).position;            
            Timer = 0;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && !WalkSelf)
        {
            //getting current finger position
            endPos = Input.GetTouch(0).position;

            Timer += Time.deltaTime;

            //calculating swipe direction
            Direction = startPos - endPos;
            Direction.Normalize();
            displaycorner.text = Direction.ToString();

            if (Move == false && Timer >= 0.1f) //if not moving
            {
                //if (CircleObject == null)
                //{
                //    CircleObject = Instantiate(CircleSpot, startPos, Quaternion.Euler(0, 0, 0));
                //    CircleObject.transform.SetParent(Canvas);
                //}
                if (Direction.y >= -1 && Direction.y < 0 && Direction.x < 0.5 && Direction.x > -0.5 && !activeAreas[0]) // Up
                {
                    for (int i = 0; i < Buttons.Length; i++)
                    {
                        if (i == 0)
                        {
                            Buttons[i].SetActive(true);
                            MoveAheadUp();
                        }
                        else
                        {
                            Buttons[i].SetActive(false);
                        }
                    }

                }
                else if (Direction.y <= 1 && Direction.y > 0 && Direction.x < 0.5 && Direction.x > -0.5 && !activeAreas[2]) // Down
                {
                    for (int i = 0; i < Buttons.Length; i++)
                    {
                        if (i == 2)
                        {
                            Buttons[i].SetActive(true);
                            MoveAheadDown();
                        }
                        else
                        {
                            Buttons[i].SetActive(false);
                        }
                    }
                }

                if (Direction.x <= 1 && Direction.x > 0 && Direction.y < 0.5 && Direction.y > -0.5 && !activeAreas[3]) // left
                {
                    for (int i = 0; i < Buttons.Length; i++)
                    {
                        if (i == 3)
                        {
                            Buttons[i].SetActive(true);
                            MoveAheadLeft();
                        }
                        else
                        {
                            Buttons[i].SetActive(false);
                        }
                    }
                }

                else if (Direction.x >= -1 && Direction.x < 0 && Direction.y < 0.5 && Direction.y > -0.5 && !activeAreas[1])//right
                {
                    for (int i = 0; i < Buttons.Length; i++)
                    {
                        if (i == 1)
                        {
                            Buttons[i].SetActive(true);
                            MoveAheadRight();
                        }
                        else
                        {
                            Buttons[i].SetActive(false);
                        }
                    }
                }
            }
        }

        if (WalkSelf && !TalkToSallyButton.activeInHierarchy)
        {
            Timer2 += Time.deltaTime;

            if (Timer2 >= TimerbeforeYouCantalktoHer)
            {
                TalkToSallyButton.SetActive(true);
            }
        }

        //if you release your finger
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        //{
        //    if (CircleObject != null)
        //    {
        //        Destroy(CircleObject);
        //    }

            //if (Move == false && Timer >= 0.1f) //if not moving
            //{

            //    if (Direction.y >= -1 && Direction.y < 0 && Direction.x < 0.5 && Direction.x > -0.5 && !activeAreas[0]) // Up
            //    {
            //        MoveAheadUp();

            //    }
            //    else if (Direction.y <= 1 && Direction.y > 0 && Direction.x < 0.5 && Direction.x > -0.5 && !activeAreas[2]) // Down
            //    {
            //        MoveAheadDown();
            //    }

            //    if (Direction.x <= 1 && Direction.x > 0 && Direction.y < 0.5 && Direction.y > -0.5 && !activeAreas[3]) // left
            //    {
            //        MoveAheadLeft();
            //    }

            //    else if (Direction.x >= -1 && Direction.x < 0 && Direction.y < 0.5 && Direction.y > -0.5 && !activeAreas[1])//right
            //    {
            //        MoveAheadRight();
            //    }
                
            //}
        //}
    }
    

    public void MoveAheadUp()
    {
        Move = true;
        rbody.velocity = new Vector2(0, speed);
        // Move = true; //are we moving?

        foreach (GameObject gam in Buttons)
        {
            gam.SetActive(false);
        }
        
        animator.SetFloat("y", 1);
        animator.SetFloat("x", 0);
        animator.SetBool("Move", true);
    }
    public void MoveAheadRight()
    {
        Move = true;
        rbody.velocity = new Vector2(speed, 0);
        //Move = true; //are we moving?

        foreach (GameObject gam in Buttons)
        {
            gam.SetActive(false);
        }
        animator.SetFloat("x", 1);
        animator.SetFloat("y", 0);
        animator.SetBool("Move", true);
    }
    public void MoveAheadDown()
    {
        Move = true;
        rbody.velocity = new Vector2(0, -speed);
        //Move = true; //are we moving?

        foreach (GameObject gam in Buttons)
        {
            gam.SetActive(false);
        }
        animator.SetFloat("y", -1);
        animator.SetFloat("x", 0);
        animator.SetBool("Move", true);
    }
    public void MoveAheadLeft()
    {
        Move = true;
        rbody.velocity = new Vector2(-speed, 0);
        //Move = true; //are we moving?

        foreach (GameObject gam in Buttons)
        {
            gam.SetActive(false);
        }
        animator.SetFloat("x", -1);
        animator.SetFloat("y", 0);
        animator.SetBool("Move", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            StopChecks stop = collision.GetComponent<StopChecks>();
            activeAreas = stop.Zones;

            if (!WalkSelf)
            {
                for (int i = 0; i < Buttons.Length; i++)
                {
                    if (activeAreas[i] == false)
                    {
                        Buttons[i].SetActive(true);
                    }
                }
            }
            
            rbody.velocity = new Vector2(0, 0);
            Move = false;
            animator.SetBool("Move", false);
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
            else if (WalkSelf)
            {
                StartCoroutine(movementTime());
            }

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

    public void Pause()
    {
        Time.timeScale = 0f;
    }
        public void Resetmadness()
    {
        Timer2 = 0;
        TalkToSallyButton.SetActive(false);
        Time.timeScale = 1f;
    }
}
