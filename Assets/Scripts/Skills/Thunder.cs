using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField] private GameObject thunder;
    private GameObject[] countOfThunder;
    [SerializeField] private int maxCountOfThunder;
    private float previousTime;
    public float timeToSpawn;

    private void Start()
    {
        previousTime = Time.time;
    }

    private void Update()
    {
        countOfThunder = GameObject.FindGameObjectsWithTag("Thunder");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(countOfThunder != null)
            {
                if (Time.time > previousTime + timeToSpawn)
                {
                    Instantiate(thunder, new Vector3(collision.transform.position.x, collision.transform.position.y, 0) + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    previousTime = Time.time;
                }
            }
        }
    }

}
