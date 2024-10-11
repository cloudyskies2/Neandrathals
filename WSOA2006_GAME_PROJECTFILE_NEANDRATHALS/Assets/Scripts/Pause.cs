using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel; // Assign the panel that contains the resume button
    private bool isPaused = false; // Track if the game is paused

    // Function to pause the game
    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            pausePanel.SetActive(true); // Show the pause panel
            Time.timeScale = 0f; // Freeze time in the game
        }
    }

    // Function to resume the game
    public void ResumeGame()
    {
        if (isPaused)
        {
            isPaused = false;
            pausePanel.SetActive(false); // Hide the pause panel
            Time.timeScale = 1f; // Unfreeze time
        }
    }
}
