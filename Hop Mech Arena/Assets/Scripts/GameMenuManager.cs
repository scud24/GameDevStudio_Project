﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject resultsMenuPanel;
    public ResultsMenuController rmc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuPanel.SetActive(true);
        }
    }

    public void ExitToMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeButtonClicked()
    {
        pauseMenuPanel.SetActive(false);
    }

    public void DisplayResultsMenu()
    {
        rmc.SetupResultsDisplay();
        resultsMenuPanel.SetActive(true);
    }

    public void RematchButtonClicked()
    {
        SceneManager.LoadScene("SplitscreenTestScene");
    }

    public void QuitToMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}