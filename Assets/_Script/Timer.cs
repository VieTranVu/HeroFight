using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;  // Để quản lý cảnh

public class Timer : MonoBehaviour
{
    public float startTime = 60f;  // Thời gian bắt đầu
    private float currentTime;     // Thời gian hiện tại
    public TMP_Text countdownText; // Text để hiển thị thời gian còn lại
    public DeathFall deathFallScript;  // Tham chiếu đến script DeathFall

    void Start()
    {
        currentTime = startTime;
        UpdateCountdownText();
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;  // Giảm thời gian theo mỗi khung hình
            UpdateCountdownText();
        }
        else
        {
            currentTime = 0;
            TimeIsUp();  // Gọi hàm khi hết giờ
        }
    }

    // Cập nhật UI countdownText
    void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Hàm gọi khi hết giờ
    void TimeIsUp()
    {
        Debug.Log("Time's up!");

        // Gọi hàm PlayerDied từ DeathFall (hiển thị game over UI và xử lý các sự kiện)
        if (deathFallScript != null)
        {
            deathFallScript.PlayerDied();  // Chắc chắn gọi hàm PlayerDied trong DeathFall để xử lý Game Over
        }
    }
}
