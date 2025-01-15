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
    public GameObject scorePanel;
    public GameObject volumePanel;

    // Các UI Elements
    public InputField nameInputField;
    public Text nameWarningText;
    
    
    public Text currentScoresText;
    public Text topScoresText;
    public int currentScore;

    // Các nút
    public Button playButton;
    public Button loadGameButton;
    public Button volumeButton;
    public Button scoreButton;
    public Button quitButton;
    public Button confirmButton;
    public Button resetButton; 

    // Biến lưu tên và điểm người chơi
    private string playerName;
    //private int playerScore = 0;

    private List<int> topScores = new List<int>();

    // UI Elements for Volume Control
    public Slider volumeSlider;   // Slider để điều chỉnh âm lượng
    public Text volumeText;       // Hiển thị giá trị âm lượng
    public Button applyVolumeButton; // Nút xác nhận áp dụng âm lượng
    void Start()
    {
        // Chỉ hiển thị Main Menu khi bắt đầu
        mainMenuPanel.SetActive(true);
        nameInputPanel.SetActive(false);        
        volumePanel.SetActive(false);
        scorePanel.SetActive(false);
        // Load Score
        LoadTopScores();

        //Score
        currentScoresText.text = "Current Score: " + currentScore.ToString();
        
        
        DisplayTopScores();

        
        

        // Gắn sự kiện cho các nút
        playButton.onClick.AddListener(OnPlayButtonClicked);
        loadGameButton.onClick.AddListener(OnLoadGameButtonClicked);
        volumeButton.onClick.AddListener(OnVolumeButtonClicked);
        scoreButton.onClick.AddListener(OnScoreButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
        confirmButton.onClick.AddListener(OnConfirmNameButtonClicked);
        // Gắn sự kiện cho nút Apply Volume
        applyVolumeButton.onClick.AddListener(ApplyVolume);
        // Gắn sự kiện cho nút Reset
        resetButton.onClick.AddListener(ResetScores);
    }

    // Khi nhấn nút Play
    public void OnPlayButtonClicked()
    {
        if (string.IsNullOrEmpty(playerName))
        {
            //mainMenuPanel.SetActive(false);
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

    // Áp dụng volume khi người chơi thay đổi slider
    public void ApplyVolume()
    {
        AudioListener.volume = volumeSlider.value;  // Điều chỉnh âm lượng của game
        volumeText.text = Mathf.RoundToInt(volumeSlider.value * 100).ToString() + "%";  // Cập nhật giá trị âm lượng

        // Lưu giá trị volume vào PlayerPrefs
        PlayerPrefs.SetFloat("Volume", AudioListener.volume);
        PlayerPrefs.Save();
    }

    // Tải top 5 điểm cao nhất
    void LoadTopScores()
    {
        for (int i = 0; i < 5; i++)
        {
            topScores.Add(PlayerPrefs.GetInt("TopScore" + i, 0));  // Lấy điểm từ PlayerPrefs
        }
    }

    // Lưu điểm cao nhất
    public void SaveTopScores()
    {
        topScores.Add(currentScore);
        topScores.Sort((a, b) => b.CompareTo(a));  // Sắp xếp theo thứ tự giảm dần

        // Lưu lại 5 điểm cao nhất
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt("TopScore" + i, topScores[i]);
        }
        PlayerPrefs.Save();
    }

    // Hiển thị top 5 điểm
    void DisplayTopScores()
    {
        topScoresText.text = "Top 5 Scores:\n";
        foreach (var score in topScores)
        {
            topScoresText.text += score.ToString() + "\n";
        }
    }

    // Khi nhấn nút Score
    public void OnScoreButtonClicked()
    {
        mainMenuPanel.SetActive(false);
        scorePanel.SetActive(true);
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
        // Lưu tên người chơi
        //string playerName = nameInputField.text;  // Lấy tên người chơi từ InputField
        PlayerPrefs.SetString("PlayerName", playerName);

        // Reset điểm số và các thông tin khác
        PlayerPrefs.DeleteKey("PlayerScore");
        PlayerPrefs.DeleteKey("PlayerCoins");
        PlayerPrefs.DeleteKey("PlayerHealth");
        PlayerPrefs.DeleteKey("CurrentScene");
        SceneManager.LoadScene("Tutorial");  // Thay đổi tên scene theo game của bạn
    }

    public void OnLoadGameButtonClicked()
    {
        string sceneName = PlayerPrefs.GetString("CurrentScene", "MainMenu");
        if (sceneName != "MainMenu")
        {
            SceneManager.LoadScene(sceneName);  // Load lại màn chơi hiện tại đã lưu
        }
    }
    // Đặt lại điểm khi nhấn nút Reset
    public void ResetScores()
    {
        currentScore = 0; // Reset điểm
        currentScoresText.text = "Current Score: " + currentScore.ToString();  // Cập nhật điểm hiện tại hiển thị trong UI

        // Reset top 5 điểm
        topScores.Clear();  // Xóa danh sách top scores
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt("TopScore" + i, 0);  // Lưu lại top điểm là 0
        }
        PlayerPrefs.Save();

        // Hiển thị top scores sau khi reset
        DisplayTopScores();
    }
}