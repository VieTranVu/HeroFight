using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Các panel
    public GameObject mainMenuPanel;
    public GameObject nameInputPanel;
    public GameObject scoreDisplayPanel;
    public GameObject volumePanel;

    // Các UI Elements
    public InputField nameInputField;
    public Text nameWarningText;
    public Text scoreDisplayText;

    // Các nút
    public Button playButton;
    public Button volumeButton;
    public Button scoreButton;
    public Button quitButton;
    public Button confirmButton;
    public Button resetButton; // Nút Reset di chuyển vào panel score

    // Biến lưu tên và điểm người chơi
    private string playerName;
    private int playerScore = 0;

    private void Start()
    {
        // Chỉ hiển thị Main Menu khi bắt đầu
        mainMenuPanel.SetActive(true);
        nameInputPanel.SetActive(false);
        scoreDisplayPanel.SetActive(false);
        volumePanel.SetActive(false);

        // Gắn sự kiện cho các nút
        playButton.onClick.AddListener(OnPlayButtonClicked);
        volumeButton.onClick.AddListener(OnVolumeButtonClicked);
        scoreButton.onClick.AddListener(OnScoreButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
        confirmButton.onClick.AddListener(OnConfirmNameButtonClicked);

        // Gắn sự kiện cho nút Reset
        resetButton.onClick.AddListener(ResetScores);
    }

    // Khi nhấn nút Play
    public void OnPlayButtonClicked()
    {
        if (string.IsNullOrEmpty(playerName))
        {
            mainMenuPanel.SetActive(false);
            nameInputPanel.SetActive(true);
        }
        else
        {
            StartGame(); // Bắt đầu trò chơi nếu đã nhập tên
        }
    }

    // Khi nhấn nút Volume (nếu cần)
    public void OnVolumeButtonClicked()
    {
        mainMenuPanel.SetActive(false);
        volumePanel.SetActive(true);
    }

    // Khi nhấn nút Score
    public void OnScoreButtonClicked()
    {
        scoreDisplayText.text = "Name: " + playerName + ", Score: " + playerScore.ToString();
        mainMenuPanel.SetActive(false);
        scoreDisplayPanel.SetActive(true);
    }

    // Khi nhấn nút Quit
    public void OnQuitButtonClicked()
    {
        Application.Quit();  // Thoát game
    }

    // Khi người chơi xác nhận tên
    public void OnConfirmNameButtonClicked()
    {
        playerName = nameInputField.text;
        if (string.IsNullOrEmpty(playerName))
        {
            nameWarningText.text = "Please enter a name!";
        }
        else
        {
            nameInputPanel.SetActive(false);
        }
    }

    // Bắt đầu trò chơi
    public void StartGame()
    {
        playerScore = 0; // Reset điểm khi bắt đầu trò chơi
        SceneManager.LoadScene("Tutorial"); // Thay "GameScene" bằng tên Scene của bạn
    }

    // Đặt lại điểm khi nhấn nút Reset
    public void ResetScores()
    {
        playerScore = 0; // Reset điểm
        scoreDisplayText.text = "Name: " + playerName + ", Score: 0"; // Cập nhật UI
    }
}