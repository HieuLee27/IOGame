using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingVolume : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Dropdown dropdownQuality;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        LoadVolume();
        LoadQuality();
    }

    // Update is called once per frame
    void Update()
    {
        SetVolume();
        SetQuality();
    }

    public void SetVolume()
    {
        float volume = volumeSlider.value;
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void LoadVolume()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            float volume = PlayerPrefs.GetFloat("Volume");
            audioSource.volume = volume;
            volumeSlider.value = volume;
        }
    }

    public void SetQuality()
    {
        dropdownQuality.onValueChanged.AddListener(ChangedQuality);
    }

    public void ChangedQuality(int level)
    {
        QualitySettings.SetQualityLevel(level);
        PlayerPrefs.SetInt("QualityLevel", level);
        PlayerPrefs.Save();
    }

    public void LoadQuality()
    {
        if (PlayerPrefs.HasKey("QualityLevel"))
        {
            int level = PlayerPrefs.GetInt("QualityLevel");
            dropdownQuality.value = level;
            QualitySettings.SetQualityLevel(level);
        }
    }
}
