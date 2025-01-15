using UnityEngine;

public class HidePanelAfterTime : MonoBehaviour
{
    public GameObject panel;  // Panel cần ẩn
    public float timeToHide = 10f;  // Thời gian ẩn panel (tính bằng giây)

    void Start()
    {
        // Đảm bảo rằng panel sẽ được hiển thị khi bắt đầu
        panel.SetActive(true);

        // Gọi hàm HidePanel sau timeToHide giây
        Invoke("HidePanel", timeToHide);
    }

    void HidePanel()
    {
        // Ẩn panel
        panel.SetActive(false);
    }
}
