using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    // This function will be called when the player collides with the Finish Line
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with the player character
        if (other.CompareTag("Player"))
        {
            // Call the function to complete the level
            CompleteLevel();
        }
    }

    // This function is for completing the level
    void CompleteLevel()
    {
        // Perform actions when the level is completed (for example, move to the next scene)
        Debug.Log("Level Complete!");

        // Save player data (score, health, etc.)
        //SavePlayerData();

        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Save the next scene index into PlayerPrefs
        PlayerPrefs.SetInt("CurrentLevel", nextSceneIndex);
        PlayerPrefs.Save();  // Immediately save the PlayerPrefs

        // Check if there is a next level
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Load the next scene (next level)
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // If no next level, load the Win scene
            Debug.Log("Congratulations, you've completed all levels!");
            SceneManager.LoadScene("WinScene");  // Load the "Win Game" scene
        }
    }

    // Save player data
    /*void SavePlayerData()
    {
        // Example: Saving player's score, health, and other stats
        // Replace these with the actual variables you want to save
        int playerScore = 100;  // Example: Retrieve the player's score
        int playerHealth = 3;   // Example: Retrieve the player's health
        int playerCoins = 50;   // Example: Retrieve the number of coins

        PlayerPrefs.SetInt("PlayerScore", playerScore);
        PlayerPrefs.SetInt("PlayerHealth", playerHealth);
        PlayerPrefs.SetInt("PlayerCoins", playerCoins);
        PlayerPrefs.Save();  // Save the data immediately
    }*/
}
