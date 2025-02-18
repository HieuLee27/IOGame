using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField] private GameObject thunder;
    private GameObject[] countOfThunder;
    public int maxCountOfThunder;
    private float previousTime;
    public float timeToSpawn;

    private void Start()
    {
        previousTime = Time.timeSinceLevelLoad;
    }

    private void Update()
    {
        countOfThunder = GameObject.FindGameObjectsWithTag("Thunder");
        SpawnThunder();
    }

    private void SpawnThunder() //Hàm tạo tia sét
    {
        if (countOfThunder.Length < maxCountOfThunder)
        {
            GameObject newEnemy = GameObject.FindWithTag("Enemy");
            if (newEnemy != null)
            {
                if (Vector2.Distance(transform.position, newEnemy.transform.position) < 3.5f)
                {
                    if (Time.timeSinceLevelLoad - previousTime > timeToSpawn)
                    {
                        Vector3 position = newEnemy.transform.position;
                        Instantiate(thunder, position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                        previousTime = Time.timeSinceLevelLoad;
                    }
                }
            }
        }
    }
}
