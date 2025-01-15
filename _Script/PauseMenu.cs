using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Để sử dụng UI
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;  // Biến kiểm tra trạng thái game
    public GameObject pauseMenuUI;  // Menu tạm dừng
    public GameObject volumePanel;
    public GameObject player;       // Tham chiếu đến đối tượng Player

    private Player playerScript;     // Tham chiếu đến script Player để lưu trạng thái

    public Slider volumeSlider;  // Slider để điều chỉnh âm lượng
    public Text volumeText;      // Hiển thị giá trị âm lượng trên UI

    public Button resumeButton;   // Nút resume
    public Button saveGameButton; // Nút save game
    public Button volumeButton;   // Nút để mở panel volume
    public Button backToMainMenuButton;     // Nút để thoát game

    private void Start()
    {
        // Menu pause sẽ không hiển thị khi bắt đầu game
        pauseMenuUI.SetActive(false);

        // Lấy script Player từ đối tượng Player
        playerScript = player.GetComponent<Player>();

        // Lấy giá trị âm lượng đã lưu từ PlayerPrefs
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);  // Lấy volume từ PlayerPrefs
        volumeText.text = Mathf.RoundToInt(volumeSlider.value * 100).ToString() + "%";  // Hiển thị âm lượng
        AudioListener.volume = volumeSlider.value;  // Áp dụng volume cho game

        resumeButton.onClick.AddListener(Resume);
        saveGameButton.onClick.AddListener(SaveGame);
        volumeButton.onClick.AddListener(OnVolumeButtonClicked);
        backToMainMenuButton.onClick.AddListener(BackToMainMenu);

    }

    void Update()
    {
        // Kiểm tra phím Escape để tạm dừng hoặc tiếp tục game
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();  // Tiếp tục game nếu đang pause
            }
            else
            {
                Pause();  // Tạm dừng game nếu game đang chơi
            }
        }
    }

    public void Resume()
    {
        // Tiếp tục game, ẩn menu pause và phục hồi thời gian game
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;  // Khôi phục thời gian game
        GameIsPause = false;  // Đặt trạng thái pause thành false
    }

    void Pause()
    {
        // Tạm dừng game, hiển thị menu pause và dừng thời gian game
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;  // Dừng thời gian game
        GameIsPause = true;  // Đặt trạng thái pause thành true

    }

    // Hàm để điều chỉnh âm lượng khi thay đổi slider
    public void OnVolumeButtonClicked()
    {
        volumePanel.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void ApplyVolume()
    {
        // Lưu giá trị volume vào PlayerPrefs và áp dụng vào game
        AudioListener.volume = volumeSlider.value;
        volumeText.text = Mathf.RoundToInt(volumeSlider.value * 100).ToString() + "%";  // Cập nhật hiển thị volume

        // Lưu lại volume trong PlayerPrefs
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();

        // Quay lại PauseMenu
        volumePanel.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    // Lưu điểm và trạng thái của player vào PlayerPrefs
    public void SaveGame()
    {
        // Lưu trạng thái game: điểm số, sức khỏe, và màn chơi hiện tại
        string currentScene = SceneManager.GetActiveScene().name;
        int currentScore = PlayerPrefs.GetInt("PlayerScore", 0);  // Lấy điểm hiện tại từ PlayerPrefs
        int currentHealth = PlayerPrefs.GetInt("PlayerHealth", 3);  // Lấy số máu hiện tại từ PlayerPrefs

        PlayerPrefs.SetString("CurrentScene", currentScene);
        PlayerPrefs.SetInt("PlayerScore", currentScore);
        PlayerPrefs.SetInt("PlayerHealth", currentHealth);

        PlayerPrefs.Save();  // Lưu tất cả thay đổi vào PlayerPrefs
    }

    public void BackToMainMenu()
    {
        // Tạm dừng game và quay về menu chính
        //Time.timeScale = 1f;  // Đảm bảo thời gian game được khôi phục
        SceneManager.LoadScene("Start");  // Đổi "MainMenu" thành tên của scene chính của bạn
    }
}