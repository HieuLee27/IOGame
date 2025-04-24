using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private GameObject target;
    public float speed = 10f;
    public float damage = 1f;
    private Vector2 direction;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag(enemy.tag);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision) //Xóa đạn khi chạm vào cây, gạch, đá, hoặc đạn của địch
    {
        if (collision.gameObject.CompareTag("Tree") || collision.gameObject.CompareTag("Rock") || collision.gameObject.CompareTag("Bounder")
            || collision.gameObject.CompareTag(enemy.tag))
        {
            Destroy(gameObject);
        }
    }
}
