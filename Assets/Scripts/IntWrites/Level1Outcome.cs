using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Ink.Runtime;
using UnityEngine.UI;

public class Level1Outcome : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;   
    [SerializeField] private TextAsset inkJSONAsset = null;
    public Story story;

    [Header("Spawn Locations")]
    [SerializeField] private GameObject canvas = null;
    [SerializeField] private Transform mainTextSpawn = null;
    [SerializeField] private Transform ContinueSpot = null;

    [Header("UI Prefabs")]
    [SerializeField] private GameObject textPrefab = null;
    [SerializeField] private Button buttonPrefab = null;

    [Header("Levelstuff")]
    public Lvl1CharMovement myplayer;
    public Animator ourplayerAni;
    public Transform[] ButtonSpots;
    public bool[] buttonsPressed;
    private bool[] buttonsPressedScene1 = null;
    private bool[] buttonsPressedScene2 = null;

    public void Start()
    {
        RemoveChildren();

            StartStory();
    }

    // Creates a new Story object with the compiled story which we can then play!
    void StartStory()
    {
        story = new Story(inkJSONAsset.text);
        if (OnCreateStory != null) OnCreateStory(story);
        RefreshView();
    }

    public void SetStoryPos(string newStoryStart, Transform[] newButtonPos, int numberToSaveForBools, int newScene)
    {
        story.ChoosePathString(newStoryStart); //new story start
        ButtonSpots = newButtonPos; //new button spawn positions

        //need to save last buttons prefab
        if (numberToSaveForBools == 1)//scene 1
        {
            buttonsPressedScene1 = buttonsPressed;
        }
        if (numberToSaveForBools == 2)
        {
            buttonsPressedScene2 = buttonsPressed;
        }
        buttonsPressed = null; //clear buttons bool

        //loading new bools
        if (newScene == 1)//scene 1
        {
            if (buttonsPressedScene1 == null)
            {
                buttonsPressedScene1 = new bool[ButtonSpots.Length];
            }
            buttonsPressed = buttonsPressedScene1;
        }
        if (newScene == 2)//scene 2
        {
            if (buttonsPressedScene2 == null)
            {
                buttonsPressedScene2 = new bool[ButtonSpots.Length];
            }
            buttonsPressed = buttonsPressedScene2;
        }

        //refreash story?
        RefreshView();
    }

    // This is the main function called every time the story changes. It does a few things:
    // Destroys all the old content and choices.
    // Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
    public void RefreshView()
    {
        // Remove all the UI on screen
        RemoveChildren();

        // Read all the content until we can't continue any more
        while (story.canContinue)
        {
            // Continue gets the next line of the story
            string text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen!
            CreateContentView(text);
        }

        // Display all the choices, if there are any!
        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                
                Choice choice = story.currentChoices[i];
                if (choice.text.StartsWith(">") && buttonsPressed[i] == true)
                {
                }
                else if (choice.text == "...")
                {
                    Button button = CreateChoiceView(choice.text.Trim(), ContinueSpot);
                    // Tell the button what to do when we press it
                    button.onClick.AddListener(delegate {
                        OnClickChoiceButton(choice);
                    });
                }
                else
                {
                    Button button = CreateChoiceView(choice.text.Trim(), ButtonSpots[i]);
                    // Tell the button what to do when we press it
                    button.onClick.AddListener(delegate {
                        OnClickChoiceButton(choice);
                    });
                }
            }
        }
        // If we've read all the content and there's no choices, the story is finished!
        else
        {
            Button choice = CreateChoiceView("End of story.\nRestart?", canvas.transform);
            choice.onClick.AddListener(delegate {
                StartStory();
            });
        }
    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        if (choice.text.StartsWith(">"))
        {
            buttonsPressed[choice.index] = true;
        }

        if (choice.text.Substring(1,1) == "A")
        {
            string checkingstring = choice.text.Substring(2,2);
            if (checkingstring == "01") //open fridge
            {
                Debug.Log("Opened fridge");
                RemoveChildren();
                ourplayerAni.enabled = true;
                ourplayerAni.Play("ToFridge");

            }
            else
            {
                RefreshView();
            }
        }
        else if (choice.text.StartsWith("...")) //when pressing on ...
        {
            RemoveChildren();
            myplayer.Move = true;
        }

        else
        {
            RefreshView();
        }        
    }

    // Creates a textbox showing the the line of text
    void CreateContentView(string text)
    {
        if (text == "E")
        {}
        else
        {
            GameObject gam = Instantiate(textPrefab);
            Text storyText = gam.GetComponentInChildren<Text>(); ;
            storyText.text = text;
            gam.transform.SetParent(mainTextSpawn, false);
        }
    }

    // Creates a button showing the choice text
    Button CreateChoiceView(string text, Transform buttonSpotTrans)
    {
        // Creates the button from a prefab
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform, false);
        choice.transform.position = buttonSpotTrans.position;

        // Gets the text from the button prefab
        Text choiceText = choice.GetComponentInChildren<Text>();
        if (text.StartsWith(">"))
        {
            text = text.Substring(5);
            choiceText.text = text;
        }
        else
        {
            choiceText.text = text;
        }


        // Make the button expand to fit the text
        //HorizontalLayoutGroup layoutGroup = canvas.GetComponent<HorizontalLayoutGroup>();
        //layoutGroup.childForceExpandHeight = false;

        return choice;
    }

    // Destroys all the children of this gameobject (all the UI)
    void RemoveChildren()
    {
        int childCount = canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(canvas.transform.GetChild(i).gameObject);
        }

        int childCount2 = mainTextSpawn.childCount;
        for (int i = childCount2 - 1; i >= 0; --i)
        {
            GameObject.Destroy(mainTextSpawn.GetChild(i).gameObject);
        }

        int childCount3 = ContinueSpot.childCount;
        for (int i = childCount3 - 1; i >= 0; --i)
        {
            GameObject.Destroy(ContinueSpot.GetChild(i).gameObject);
        }
    }

}
