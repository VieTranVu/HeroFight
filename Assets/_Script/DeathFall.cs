using UnityEngine;
using UnityEngine.SceneManagement;  // Để quản lý cảnh
using UnityEngine.UI;  // Để xử lý UI

public class DeathFall : MonoBehaviour
{
    public GameObject gameOverUI;  // UI Game Over (Panel hoặc Text)
    public Button playAgainButton; // Nút Play Again
    public Button mainMenuButton;  // Nút Main Menu

    private bool isGameOver = false;  // Biến kiểm tra trạng thái Game Over

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu là nhân vật người chơi (dựa trên tag "Player")
        if (other.CompareTag("Player"))
        {
            PlayerDied();
        }
    }

    // Hàm xử lý khi nhân vật chết
    public void PlayerDied()
    {
        if (!isGameOver)  // Kiểm tra để tránh gọi nhiều lần
        {
            isGameOver = true;  // Đánh dấu là đã Game Over
            // Hiển thị UI Game Over
            gameOverUI.SetActive(true);

            // Dừng thời gian trò chơi
            Time.timeScale = 0f;  // Dừng toàn bộ game

            // Đăng ký sự kiện cho các nút
            playAgainButton.onClick.AddListener(RestartLevel);
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        }
    }

    // Hàm khởi động lại màn chơi
    public void RestartLevel()
    {
        // Tiếp tục thời gian trò chơi và tải lại màn chơi
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Tải lại cảnh hiện tại
    }

    // Hàm quay lại Menu chính
    public void GoToMainMenu()
    {
        // Tiếp tục thời gian trò chơi và chuyển đến màn hình chính
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");  // Tải cảnh MainMenu (tạo trước đó)
    }
}
