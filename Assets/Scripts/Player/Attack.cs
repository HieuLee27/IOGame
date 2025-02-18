using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Attack : MonoBehaviour
{
    public List<GameObject> dart;
    [SerializeField] private float timeShot;
    [SerializeField] private float visibility;
    [SerializeField] GameObject enemy;
    [SerializeField] private float force;

    internal bool canAttack;

    public AudioSource source;
    public AudioClip shotAudio;

    internal float levelOfDart;

    private Vector3 targetPos;
    private GameObject[] listEnemy;

    private float minDistance;

    private void Awake()
    {
        targetPos = Vector3.zero;
        levelOfDart = 0;
        canAttack = true;
    }

    private void Start()
    {
        InvokeRepeating(nameof(Shoting), 0.5f, timeShot);
    }

    private void Shoting() //Hàm bắn đạn
    {
        listEnemy = GameObject.FindGameObjectsWithTag(enemy.tag);
        if(listEnemy.Length != 0 && canAttack)
        {
            minDistance = Vector2.Distance(listEnemy[0].transform.position, transform.position);
            foreach (var enemy in listEnemy)
            {
                if (Vector2.Distance(enemy.transform.position, transform.position) <= minDistance)
                {
                    minDistance = Vector2.Distance(enemy.transform.position, transform.position);
                    targetPos = enemy.transform.position;
                }
            }
            if (minDistance <= visibility)
            {
                GameObject dartInstance = Instantiate(dart[(int)levelOfDart], transform.position, transform.rotation);
                source.PlayOneShot(shotAudio);
                dartInstance.GetComponent<Rigidbody2D>().velocity = (targetPos - transform.position).normalized * force;
            }
        }
    }
}
