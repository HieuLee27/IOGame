using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch : MonoBehaviour
{
    public int sceneIndex = 1; // Chỉ số của scene bạn muốn chuyển đến

    public void SwitchScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
