using UnityEngine;
using UnityEngine.UI;
using System;
using Ink.Runtime;

// This is a super bare bones example of how to play and display a ink story in Unity.
public class BasicInkExample : MonoBehaviour {
    public static event Action<Story> OnCreateStory;
	
    void Awake () {
		// Remove the default message
		RemoveChildren();
		StartStory();
	}

	// Creates a new Story object with the compiled story which we can then play!
	void StartStory () {
		story = new Story (inkJSONAsset.text);
        if(OnCreateStory != null) OnCreateStory(story);
		RefreshView();
	}
	
	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	void RefreshView () {
		// Remove all the UI on screen
		RemoveChildren ();
		
		// Read all the content until we can't continue any more
		while (story.canContinue) {
			// Continue gets the next line of the story
			string text = story.Continue ();
			// This removes any white space from the text.
			text = text.Trim();
			// Display the text on screen!
			CreateContentView(text);
		}

		// Display all the choices, if there are any!
		if(story.currentChoices.Count > 0) {
			for (int i = 0; i < story.currentChoices.Count; i++) {
                if (i == 0)
                {
                    Choice choice = story.currentChoices[i];
                    if (choice.text != "e")
                    {

                        upButton.gameObject.SetActive(true);
                        //Button button = upButton;//CreateChoiceView(choice.text.Trim());
                        upButton.GetComponentInChildren<Text>().text = choice.text;

                        //Tell the button what to do when we press it
                        upButton.onClick.RemoveListener(delegate
                        {
                            OnClickChoiceButton(choice);
                        });
                        upButton.onClick.AddListener(delegate {
                            OnClickChoiceButton(choice);
                        });
                    }
                }

                else if (i == 1)
                {
                    Choice choice = story.currentChoices[i];
                    if (choice.text != "e")
                    {

                        rightButton.gameObject.SetActive(true);
                        //Button button = upButton;//CreateChoiceView(choice.text.Trim());
                        rightButton.GetComponentInChildren<Text>().text = choice.text;

                        // Tell the button what to do when we press it
                        rightButton.onClick.AddListener(delegate {
                            OnClickChoiceButton(choice);
                        });
                    }
                }

                else if (i == 2)
                {
                    Choice choice = story.currentChoices[i];
                    if (choice.text != "e")
                    {

                        downButton.gameObject.SetActive(true);
                        //Button button = upButton;//CreateChoiceView(choice.text.Trim());
                        downButton.GetComponentInChildren<Text>().text = choice.text;

                        // Tell the button what to do when we press it
                        downButton.onClick.AddListener(delegate {
                            OnClickChoiceButton(choice);
                        });
                    }
                }

                else if (i == 3)
                {
                    Choice choice = story.currentChoices[i];
                    if (choice.text != "e")
                    {

                        leftButton.gameObject.SetActive(true);
                        //Button button = upButton;//CreateChoiceView(choice.text.Trim());
                        leftButton.GetComponentInChildren<Text>().text = choice.text;

                        // Tell the button what to do when we press it
                        leftButton.onClick.AddListener(delegate {
                            OnClickChoiceButton(choice);
                        });
                    }
                }

                else
                {
                    Choice choice = story.currentChoices[i];
                    Button button = CreateChoiceView(choice.text.Trim());
                    // Tell the button what to do when we press it
                    button.onClick.AddListener(delegate {
                        OnClickChoiceButton(choice);
                    });
                }
				
			}
		}
		// If we've read all the content and there's no choices, the story is finished!
		else {
            Debug.Log("Ending");
            //Button choice = CreateChoiceView("End of story.\nRestart?");
            endStory.gameObject.SetActive(true);
            endStory.onClick.AddListener(delegate{
				StartStory();
			});
		}
	}

	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton (Choice choice) {
		story.ChooseChoiceIndex (choice.index);
		RefreshView();
	}

	// Creates a textbox showing the the line of text
	void CreateContentView (string text) {
        Text storyText = Instantiate(textPrefab) as Text;//MainBox;
        storyText.text = text;
		storyText.transform.SetParent (MainBox.transform, false);
	}

	// Creates a button showing the choice text
	Button CreateChoiceView (string text) {
		// Creates the button from a prefab
		Button choice = Instantiate (buttonPrefab) as Button;
		choice.transform.SetParent (ButtonSpawn.transform, false);
		
		// Gets the text from the button prefab
		Text choiceText = choice.GetComponentInChildren<Text> ();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent <HorizontalLayoutGroup> ();
		layoutGroup.childForceExpandHeight = false;

		return choice;
	}

	// Destroys all the children of this gameobject (all the UI)
	void RemoveChildren () {
        upButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        leftButton.gameObject.SetActive(false);
        downButton.gameObject.SetActive(false);
        endStory.gameObject.SetActive(false);
        int childCount = ButtonSpawn.transform.childCount;
        int childCount1 = MainBox.transform.childCount;
        //MainBox.text = "";
        for (int i = childCount - 1; i >= 0; --i) {
        	GameObject.Destroy (ButtonSpawn.transform.GetChild (i).gameObject);
        }

        for (int i = childCount1 - 1; i >= 0; --i)
        {
            GameObject.Destroy(MainBox.transform.GetChild(i).gameObject);
        }
    }

	[SerializeField]
	private TextAsset inkJSONAsset = null;
	public Story story;

	[SerializeField]
	private GameObject ButtonSpawn = null;

	// UI Prefabs
	[SerializeField]
	private Text textPrefab = null;
	[SerializeField]
	private Button buttonPrefab = null;
    [SerializeField] private GameObject MainBox = null;
    [SerializeField] private Button upButton = null;
    [SerializeField] private Button rightButton = null;
    [SerializeField] private Button downButton = null;
    [SerializeField] private Button leftButton = null;
    [SerializeField] private Button endStory = null;
}
