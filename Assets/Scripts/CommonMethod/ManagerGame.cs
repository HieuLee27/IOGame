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
    internal bool isPaused = false;
    internal bool modeBoss = false;
    public Slider levelBar;
    public GameObject panelSkills;
    public GameObject skillPrefab;
    public int timeToSpawn;
    public Slider bloodOfBoss;
    public GameObject panelResult;

    [SerializeField] private GameObject[] enemy;
    private HashSet<Vector2> blankPos;
    [SerializeField] private int enemyPerSecond;

    private GameObject player;
    List<Vector2> enabledPos = new();
    [SerializeField] private int maxEnemyInScene;

    private List<GameObject> enemyInScene = new();
    public float timeToSpawnBoss;
    public GameObject warningPanel;
    public GameObject boss;

    private GameObject bossInstance;
    internal Results result;

    private int countBoss;
    public static ManagerGame instance;

    [SerializeField] private TMP_Text textResult;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip winAudio;
    public AudioClip loseAudio;
    public AudioClip warningAudio;
    public AudioClip backgroundAudio;

    private void Start()
    {
        blankPos = new HashSet<Vector2>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(WaitScript());
        InvokeRepeating(nameof(SpawnEnemy), 1, timeToSpawn);
        result = Results.None;
        source.PlayOneShot(backgroundAudio);
    }

    public enum Results //Trạng thái kết quả
    {
        Win,
        Lose,
        None
    }

    private void Update()
    {
        blankPos = Map.mapInstance.blankPos;
        Selection();
        enemyInScene = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        StartCoroutine(Warning());
        EnableBoss();
        instance = this;

        Time.timeScale = isPaused ? 0 : 1;

        StartCoroutine(EndGame());
    }

    private void Selection() //Hiển thị panel skill khi đạt level
    {
        if (levelBar.value == 1 && !isPaused)
        {
            isPaused = true;
            panelSkills.SetActive(true);
            levelBar.value = 0;
        }
    }

    public void TogglePause() //Tạm dừng game
    {
        isPaused = false;
    }

    private void CreateEnablePos() //Tạo vị trí cho enemy
    {
        foreach (Vector2 pos in blankPos)
        {
            if (Vector2.Distance(player.transform.position, pos) > 5)
            {
                enabledPos.Add(pos);
            }
        }
    }

    private void SpawnEnemy() //Tạo enemy
    {
        CreateEnablePos();
        if (enemyInScene.Count < maxEnemyInScene && !modeBoss)
        {
            for (int i = 0; i < enemyPerSecond; i++)
            {
                int index = Random.Range(0, enabledPos.Count - 1);
                Instantiate(enemy[Random.Range(0, enemy.Length)], enabledPos[index],
                    Quaternion.identity);
            }
        }
        enabledPos.Clear();
    }

    IEnumerator WaitScript() //Chờ script chạy xong
    {
        yield return new WaitForSeconds(1);
    }

    IEnumerator DisplayAttention() //Hiển thị cảnh báo
    {
        warningPanel.SetActive(true);
        source.PlayOneShot(warningAudio);
        yield return new WaitForSeconds(3);
        warningPanel.SetActive(false);
    }

    private void ClearEnemy() //Xóa enemy
    {
        foreach (var ene in enemyInScene)
        {
            if (ene.name != "Ghost(Clone)")
            {
                Destroy(ene);
            }
        }
    }

    IEnumerator Warning() //Hiển thị cảnh báo boss
    {
        if ((int)Time.timeSinceLevelLoad == timeToSpawnBoss)
        {

            ClearEnemy();
            modeBoss = true;
            countBoss = 1;
            StartCoroutine(DisplayAttention());
            yield return new WaitForSeconds(3);
            if (countBoss == 1)
            {
                SpawnBoss();
            }
            countBoss = 0;
        }
    }

    private void SpawnBoss() //Tạo boss
    {
        bossInstance = Instantiate(boss, player.transform.position + new Vector3(0, 3.5f, 0), Quaternion.identity);
        bloodOfBoss.gameObject.SetActive(true);
    }


    public void EnableBoss() //Hiển thị boss
    {
        if (bossInstance != null)
        {
            GhostController instance = bossInstance.GetComponent<GhostController>();
            if (Time.timeSinceLevelLoad > instance.timeDisappear + timeToSpawnBoss && !instance.isAppear)
            {
                ClearEnemy();
                StartCoroutine(DisplayAttention());
                transform.position = player.transform.position + new Vector3(0, 3.5f, 0);
                bossInstance.SetActive(true);
                instance.anim.SetTrigger("Appear");
                instance.timeAppear = Time.timeSinceLevelLoad;
                bloodOfBoss.gameObject.SetActive(true);
                instance.isAppear = true;
            }
        }
    }

    IEnumerator EndGame() //Kết thúc game
    {
        if (result != Results.None)
        {
            yield return new WaitForSeconds(2);
            panelResult.SetActive(true);
            textResult.text = result == Results.Win ? "WIN" : "LOSE"; //Hiển thị kết quả
            if (result == Results.Win)
            {
                source.PlayOneShot(winAudio);
            }
            else
            {
                source.PlayOneShot(loseAudio);
            }
            isPaused = true;
        }
    }

    public void Home() //Quay về menu
    {
        SceneManager.LoadScene(0);
    }
}