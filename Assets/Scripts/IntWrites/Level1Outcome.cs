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
    private bool[] buttonsPressedScene3 = null;
    private bool[] buttonsPressedScene4 = null;
    private bool[] buttonsPressedScene5 = null;
    private bool[] buttonsPressedScene6 = null;
    private bool[] buttonsPressedScene7 = null;
    private bool[] buttonsPressedScene8 = null;
    private bool[] buttonsPressedScene9 = null;
    private bool[] buttonsPressedScene10 = null;
    private bool[] buttonsPressedScene11 = null;
    private bool[] buttonsPressedScene12 = null;

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
        if (numberToSaveForBools == 1)          //scene 1
        {buttonsPressedScene1 = buttonsPressed;}
        if (numberToSaveForBools == 2)          //scene 2
        { buttonsPressedScene2 = buttonsPressed;}
        if (numberToSaveForBools == 3)          //scene 3
        { buttonsPressedScene3 = buttonsPressed; }
        if (numberToSaveForBools == 4)          //scene 4
        { buttonsPressedScene4 = buttonsPressed; }
        if (numberToSaveForBools == 5)          //scene 5
        { buttonsPressedScene5 = buttonsPressed; }
        if (numberToSaveForBools == 6)          //scene 6
        { buttonsPressedScene6 = buttonsPressed; }
        if (numberToSaveForBools == 7)          //scene 7
        { buttonsPressedScene7 = buttonsPressed; }
        if (numberToSaveForBools == 8)          //scene 8
        { buttonsPressedScene8 = buttonsPressed; }
        if (numberToSaveForBools == 9)          //scene 9
        { buttonsPressedScene9 = buttonsPressed; }
        if (numberToSaveForBools == 10)          //scene 10
        { buttonsPressedScene10 = buttonsPressed; }
        if (numberToSaveForBools == 11)          //scene 11
        { buttonsPressedScene11 = buttonsPressed; }
        if (numberToSaveForBools == 12)          //scene 12
        { buttonsPressedScene12 = buttonsPressed; }

        buttonsPressed = null; //clear buttons bool

        //loading new bools
        if (newScene == 1)//scene 1
        {   if (buttonsPressedScene1 == null)
            {buttonsPressedScene1 = new bool[ButtonSpots.Length];}
            buttonsPressed = buttonsPressedScene1;
        }
        if (newScene == 2)//scene 2
        {
            if (buttonsPressedScene2 == null)
            { buttonsPressedScene2 = new bool[ButtonSpots.Length]; }
            buttonsPressed = buttonsPressedScene2;
        }
        if (newScene == 3)//scene 3
        {
            if (buttonsPressedScene3 == null)
            { buttonsPressedScene3 = new bool[ButtonSpots.Length]; }
            buttonsPressed = buttonsPressedScene3;
        }
        if (newScene == 4)//scene 4
        {
            if (buttonsPressedScene4 == null)
            { buttonsPressedScene4 = new bool[ButtonSpots.Length]; }
            buttonsPressed = buttonsPressedScene4;
        }
        if (newScene == 5)//scene 1
        {
            if (buttonsPressedScene5 == null)
            { buttonsPressedScene5 = new bool[ButtonSpots.Length]; }
            buttonsPressed = buttonsPressedScene5;
        }
        if (newScene == 6)//scene 1
        {
            if (buttonsPressedScene6 == null)
            { buttonsPressedScene6 = new bool[ButtonSpots.Length]; }
            buttonsPressed = buttonsPressedScene6;
        }
        if (newScene == 7)//scene 7
        {
            if (buttonsPressedScene7 == null)
            { buttonsPressedScene7 = new bool[ButtonSpots.Length]; }
            buttonsPressed = buttonsPressedScene7;
        }
        if (newScene == 8)//scene 8
        {
            if (buttonsPressedScene8 == null)
            { buttonsPressedScene8 = new bool[ButtonSpots.Length]; }
            buttonsPressed = buttonsPressedScene8;
        }
        if (newScene == 9)//scene 9
        {
            if (buttonsPressedScene9 == null)
            { buttonsPressedScene9 = new bool[ButtonSpots.Length]; }
            buttonsPressed = buttonsPressedScene9;
        }
        if (newScene == 10)//scene 10
        {
            if (buttonsPressedScene10 == null)
            { buttonsPressedScene10 = new bool[ButtonSpots.Length]; }
            buttonsPressed = buttonsPressedScene10;
        }
        if (newScene == 11)//scene 11
        {
            if (buttonsPressedScene11 == null)
            { buttonsPressedScene11 = new bool[ButtonSpots.Length]; }
            buttonsPressed = buttonsPressedScene11;
        }
        if (newScene == 12)//scene 12
        {
            if (buttonsPressedScene12 == null)
            { buttonsPressedScene12 = new bool[ButtonSpots.Length]; }
            buttonsPressed = buttonsPressedScene12;
        }
        //refreash story?
        //RefreshView();
        RemoveChildren();
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
