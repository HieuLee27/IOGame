using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private GameObject target;
    [SerializeField] private float speed = 10f;
    public float damage = 1f;
    private Vector2 direction;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag(enemy.tag);
    }

    private void Start()
    {
        Shot();
    }

    private void Shot()
    {
        direction = (target.transform.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tree") || collision.gameObject.CompareTag("Wood") || collision.gameObject.CompareTag("Bounder")
            || collision.gameObject.CompareTag(enemy.tag))
        {
            Destroy(gameObject);
        }
    }
}
