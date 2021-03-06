﻿using UnityEngine;
using UnityEngine.UI;
using System;
using Ink.Runtime;

public class InkRewrite : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;

    public GameObject maincanvas;
    public GameObject Gameplay;
    public GameManager gManager;
    public PlayerMovement ourplayer;

    [Header ("Mad Talking")]
    public bool Madtalk;
    public string Pathfind;

    public void Start()
    {
        RemoveChildren();
        if (Madtalk)
        {
            story = new Story(inkJSONAsset.text);
            if (OnCreateStory != null) OnCreateStory(story);
            //stop everything from moving
            story.ChoosePathString(Pathfind);
            RefreshView();
        }
        // Remove the default message
        else
        {
            StartStory();
        }
        
    }

    // Creates a new Story object with the compiled story which we can then play!
    void StartStory()
    {
        story = new Story(inkJSONAsset.text);
        if (OnCreateStory != null) OnCreateStory(story);
        RefreshView();
    }

    // This is the main function called every time the story changes. It does a few things:
    // Destroys all the old content and choices.
    // Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
    void RefreshView()
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
                Button button = CreateChoiceView(choice.text.Trim());
                // Tell the button what to do when we press it
                button.onClick.AddListener(delegate {
                    OnClickChoiceButton(choice);
                });
            }
        }
        // If we've read all the content and there's no choices, the story is finished!
        else
        {
            Button choice = CreateChoiceView("End of story.\nRestart?");
            choice.onClick.AddListener(delegate {
                StartStory();
            });
        }
    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    // Creates a textbox showing the the line of text
    void CreateContentView(string text)
    {
        if (text == "Boo") //When telling sally she is capabile
        {
            ourplayer.WalkSelf = true;
            Gameplay.SetActive(true);
            maincanvas.SetActive(false);            
        }

        if (text == "Lead the way!") //Start Gameplay
        {
            Gameplay.SetActive(true);
            maincanvas.SetActive(false);
            if (Madtalk)
            {
                ourplayer.WalkSelf = false;
                ourplayer.TalkToSallyButton.SetActive(false);
                Time.timeScale = 1f;              
            }
        }

        if (text == "Name") //setting up your name
        {
            text = "You must be " + gManager.playersName + "!";
        }

        if (text == "Backwards") //when displeaseing her about the name
        {
            Application.LoadLevel("MainMenu DISPLEASED");
        }

        if (text == "LeaveNow") //when displeaseing her about the name
        {
            if(Madtalk)
            {
                maincanvas.SetActive(false);
                ourplayer.Resetmadness();
            }
        }

        Text storyText = Instantiate(textPrefab) as Text;
        storyText.text = text;
        storyText.transform.SetParent(mainTextSpawn, false);

    }

    // Creates a button showing the choice text
    Button CreateChoiceView(string text)
    {
        // Creates the button from a prefab
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform, false);

        // Gets the text from the button prefab
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

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
    }

    [Header("Main Stats")]
    [SerializeField]
    private TextAsset inkJSONAsset = null;
    public Story story;

    [SerializeField] private GameObject canvas = null;
    [SerializeField] private Transform mainTextSpawn = null;

    // UI Prefabs
    [SerializeField]
    private Text textPrefab = null;
    [SerializeField]
    private Button buttonPrefab = null;
}
