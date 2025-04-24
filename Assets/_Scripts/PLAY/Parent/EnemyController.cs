using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    internal Rigidbody2D rb;
    [SerializeField] private float speedMoving;
    [SerializeField] private float timeAttacking;
    private Vector2 direction;
    internal GameObject player;
    private SpriteRenderer sp;

    internal Animator anim;
    public GameObject mana;
    public GameObject coin;

    [Header("Component")]
    public float health;
    public float damage;

    public virtual void Awake()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    {
        direction = CommonMethod.GetDirection(gameObject, player);
        Appear();
        FlipSprite();
        if (Vector2.Distance(transform.position, player.transform.position) < 2)
        {
            CatchPlayer(); 
        }
    }

    public virtual void Appear() //Cách thức xuất hiện của enemy
    {
        transform.position = new Vector3(transform.position.x, 
            transform.position.y, transform.position.y);
    }

    private void FlipSprite() //Lật hình của enemy
    {
        if (direction.x > 0)
        {
            sp.flipX = true;
        }
        else
        {
            sp.flipX = false;
        }
    }

    public virtual void CatchPlayer() //Đuổi theo player
    {
        rb.velocity = direction.normalized * speedMoving;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision) //Xử lý va chạm với đạn
    {
        if (collision.gameObject.CompareTag("Dart"))
        {
            anim.SetTrigger("Hit");
            rb.velocity = direction.normalized * -2f;
            health -= collision.GetComponent<Darts>().damage;
            DamagePopUpGenerator.current.CreatePopUp(transform.position, 
                collision.GetComponent<Darts>().damage.ToString(), 
                Color.yellow);
        }
        if (collision.gameObject.CompareTag("Thunder"))
        {
            anim.SetTrigger("Hit");
            health -= collision.GetComponent<MakeDamage>().damage;
            DamagePopUpGenerator.current.CreatePopUp(transform.position, 
                collision.GetComponent<MakeDamage>().damage.ToString(), 
                Color.red);
            rb.velocity = Vector2.zero;
        }
    }

    public virtual void DestroyEnemyAndSpawnMana() //Xử lý khi enemy bị hết máu
    {
        if(health <= 0.05f)
        {
            Destroy(gameObject);
            int number1 = Random.Range(2, 7);
            for(int i = 0; i < number1; i++)
            {
                Instantiate(mana, transform.position, 
                    Quaternion.identity);
            }

            int number2 = Random.Range(1, 2);
            for (int i = 0; i < number2; i++)
            {
                Instantiate(coin, transform.position, 
                    Quaternion.identity);
            }
        }
    }
}
