﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string LevelName = null;

    public void LoadLevel()
    {
        Application.LoadLevel(LevelName);
    }
}
