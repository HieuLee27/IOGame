using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private int nextScene = 1;

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
                nextScene = 3;
                
                result = Results.None;
            }
            else if(result == Results.Lose)
            {
                textResult.text = "You Lose!";
                source.PlayOneShot(loseAudio);
                nextScene = 1;

                result = Results.None;
            }
        }
    }

    public void ContinuePlaying()
    {
        panelResult.SetActive(false);
        isPaused = false;
    }

    public void NextScene()
    {
        SceneManager.LoadSceneAsync(nextScene);
    }
}