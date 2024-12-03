using UnityEngine;
using UnityEngine.SceneManagement;  // Để quản lý cảnh (scenes)

public class FinishPoint : MonoBehaviour
{
    // Hàm này sẽ được gọi khi người chơi va chạm với Finish Line
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem va chạm có phải là với nhân vật người chơi không
        if (other.CompareTag("Player"))
        {
            // Gọi hàm hoàn thành màn chơi
            CompleteLevel();
        }
    }

    // Hàm hoàn thành màn chơi
    void CompleteLevel()
    {
        // Thực hiện hành động khi qua màn (ví dụ chuyển sang màn chơi tiếp theo)
        Debug.Log("Level Complete!");

        // Chuyển sang màn chơi tiếp theo (theo chỉ số trong Build Settings)
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Kiểm tra xem có màn chơi tiếp theo không
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Chuyển đến màn chơi tiếp theo
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // Nếu không còn màn chơi tiếp theo, kết thúc trò chơi
            Debug.Log("Congratulations, you've completed all levels!");
            // Tùy chọn: Quay lại menu chính hoặc kết thúc trò chơi
        }
    }
}
