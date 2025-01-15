using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // To handle UI buttons

public class WinGameController : MonoBehaviour
{
    // For the buttons on the Win screen
    public Button restartButton;
    public Button mainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        // Add listeners for the buttons
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    // Restart the game from the first level
    public void RestartGame()
    {
        // Reset PlayerPrefs (optional)
        PlayerPrefs.DeleteAll();

        // Load the first level (index 0 or your starting level)
        SceneManager.LoadScene(0);
    }

    // Go to the Main Menu scene (assuming index 0 is the main menu)
    public void GoToMainMenu()
    {
        // Optionally reset PlayerPrefs
        PlayerPrefs.DeleteAll();

        // Load the Main Menu (replace the index with your actual Main Menu scene index)
        SceneManager.LoadScene("MainMenu");
    }
}
