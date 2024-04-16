using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For loading the level 1 scene

public class Main : MonoBehaviour
{
    // Define variables here for player, friends, UI elements, etc.

    // Start is called before the first frame update
    void Start()
    {
        // Load level/Scene
        SceneManager.LoadScene("Level_1");
        // Initialize character
        // Profile creation with username and password
        // Saving/loading
        // Menu GUI
        // Option settings
    }

    // Update is called once per frame
    void Update()
    {
        // Handle character controls and movements
        // Check for interactions with world (collisions, triggering math problems, etc.)
        // Creating character icon movement (switching images based on conditions)
        // Update energy picked up, energy needed by friends, etc.
        // Transitions or animations for UI components
    }
}
