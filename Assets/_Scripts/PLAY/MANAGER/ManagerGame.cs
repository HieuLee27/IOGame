using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class ManagerGame : MonoBehaviour
{
    public static ManagerGame instance;

    internal bool isPaused = false;
    public Slider levelBar;
    public GameObject panelSkills;
    public GameObject skillPrefab;
    public Slider bloodOfBoss;

    private GameObject player;

    public GameObject panelResult;
    internal Results result;
    [SerializeField] private TMP_Text textResult;

    [Header("Audio")] //SFX and BGM
    public AudioSource source;
    public AudioClip winAudio;
    public AudioClip loseAudio;
    public AudioClip BGM;

    [SerializeField] private TMP_Text textLevel;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        result = Results.None;
        source.PlayOneShot(BGM);
    }

    public enum Results //State of game
    {
        Win,
        Lose,
        None
    }

    private void Update()
    {
        SkillUpping();
        instance = this;

        Time.timeScale = isPaused ? 0 : 1;
        StartCoroutine(GameEnding());
    }

    private void SkillUpping() //Option skill
    {
        if (levelBar.value == 1 && !isPaused)
        {
            isPaused = true;
            panelSkills.SetActive(true);
            levelBar.value = 0;
        }
    }

    public void GamePausing() //Pause game when touch pause button
    {
        isPaused = true;
        panelResult.SetActive(true);
        panelResult.transform.Find("btnHome").gameObject.SetActive(false);
    }

    IEnumerator GameEnding() //End game
    {
        if (result != Results.None)
        {
            yield return new WaitForSeconds(1);

            isPaused = true;
            panelResult.SetActive(true);
            panelResult.transform.Find("btnClose").gameObject.SetActive(false);
            panelResult.transform.Find("btnHome").gameObject.SetActive(true);

            Debug.Log("Result: " + result.ToString());

            if (result == Results.Win)
            {
                textResult.text = "You Win!";
                source.PlayOneShot(winAudio);
                
                result = Results.None;
            }
            else if(result == Results.Lose)
            {
                textResult.text = "You Lose!";
                source.PlayOneShot(loseAudio);

                result = Results.None;
            }
        }
    }

    public void ContinuePlaying()
    {
        panelResult.SetActive(false);
        isPaused = false;
    }

    public void NextChapter()
    {
        string name = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync(nameof(name));
        ResetStatus();
    }

    private void NextScene()
    {

    }

    public void ResetStatus()
    {
        string path = LevelTrasition.FileName();
        GameProgress progressI = JsonUtility.FromJson<GameProgress>(File.ReadAllText(path));
        progressI.coin = "0";
        progressI.chapter = "1";
        progressI.mana = 0;
        File.WriteAllText(path, JsonUtility.ToJson(progressI, true));
    }
}