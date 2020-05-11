using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool ClearOnStart;
    public bool pissed;
    public GameStats MainMenu;
    public GameStats ClearMenu;
    public Button ContinueButton;
    public Text AngryTextName;

    public string playersName;

    public void SaveName()
    {
        SaveSystem.SaveName(MainMenu);
    }

    public void ClearEverything()
    {
        SaveSystem.SaveName(ClearMenu);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        playersName = data.Name;
    }

    private void OnEnable()
    {
        if (ClearOnStart)
        {
            ClearEverything();
        }

        LoadPlayer();
        if (pissed)
        {
            AngryTextName.text = "Maybe it's " + playersName;
        }

        if (ContinueButton != null && playersName == "")
        {
            ContinueButton.enabled = false;
            ContinueButton.image.color = new Color32(166, 166, 166, 255);
        }
    }
}
