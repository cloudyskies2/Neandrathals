using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For using UI elements like Slider

public class PlayerProgress : MonoBehaviour
{
    public Slider progressBar;      // Assign the UI slider in the Inspector
    public GameObject[] lights;     // Array to store all the lights that need to be turned on
    public Image[] bulbImages;  // Array of UI images representing the bulbs
    public Sprite litBulbSprite;  // The lit bulb sprite
    public Sprite unlitBulbSprite; 

 private int activeLightsCount = 0;  // Track how many lights are on

    void Start()
    {
        // Initialize total lights and set the progress bar maximum value
        progressBar.minValue = 0;
        progressBar.maxValue = lights.Length; // Set max to the total number of lights
        progressBar.value = 0; // Start the progress bar at 0

        // Set all bulbs to unlit at the start
        foreach (Image bulb in bulbImages)
        {
            bulb.sprite = unlitBulbSprite;
        }
    }

    // Call this function whenever a light is turned on
    
    void Update()
    {
        // Check for each object in the array if it is active and update the slider accordingly
        int currentActiveObjects = 0;

        foreach (GameObject obj in lights)
        {
            if (obj.activeSelf)
            {
                currentActiveObjects++;
            }
        }

        // Only update the slider if a new object has been activated
        if (currentActiveObjects > activeLightsCount)
        {
            activeLightsCount = currentActiveObjects;  // Update active object count
            UpdateSlider();  // Update the slider's value
             UpdateBulbs();
        }
    }

    // Function to update the slider value based on how many objects are active
    private void UpdateSlider()
    {
        progressBar.value = activeLightsCount;  // Set the slider to the current number of active objects
    }

        private void UpdateBulbs()
    {
        for (int i = 0; i < bulbImages.Length; i++)
        {
            if (i < activeLightsCount)
            {
                bulbImages[i].sprite = litBulbSprite;  // Change to lit bulb if the progress has increased
            }
            else
            {
                bulbImages[i].sprite = unlitBulbSprite;  // Keep the remaining bulbs unlit
            }
        }
    }
}

