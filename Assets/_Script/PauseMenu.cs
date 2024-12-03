using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Để sử dụng UI
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;  // Biến kiểm tra trạng thái game
    public GameObject pauseMenuUI;  // Menu tạm dừng

    private void Start()
    {
        // Menu pause sẽ không hiển thị khi bắt đầu game
        pauseMenuUI.SetActive(false);
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

    public void QuitGame()
    {
        //Application.Quit();  // Thoát game khi nhấn nút Quit
        // Dừng tất cả các hoạt động trong game
        Time.timeScale = 0f;  // Dừng thời gian, làm cho game tạm dừng

        // Quay lại Main Menu
        SceneManager.LoadScene("Start");
    }
}