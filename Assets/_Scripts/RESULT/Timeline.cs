using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class Timeline : MonoBehaviour
{
    void Start()
    {
        // Ví dụ chuyển cảnh bằng nút bấm hoặc trigger
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("SceneB");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Tìm PlayableDirector trong Scene B và chạy
        var director = GameObject.FindObjectOfType<PlayableDirector>();
        if (director != null)
        {
            director.Play();
        }

        // Đừng quên bỏ đăng ký để tránh gọi lại lần sau
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
