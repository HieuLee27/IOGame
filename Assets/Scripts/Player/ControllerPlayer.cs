using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPlayer : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] private Joystick joystickScript;
    [SerializeField] private float speed = 5f;
    public Slider sliderMana;
    public TMP_Text amountOfCoin_text;
    private int amountOfCoin;
    internal int level;

    public float health;
    private bool isLive;

    private Rigidbody2D rb;
    private Animator anim;
    private int parameter;
    private SpriteRenderer sp;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        parameter = 2;
        isLive = true;

        level = 1;
        amountOfCoin = 0;
    }

    private void FixedUpdate()
    {
        Move();
        AnimationPlayer();
        FlipSprite();
        Appear();
        ResetValue();
    }

    private void Move()
    {
        if (joystickScript.Direction.x != 0 || joystickScript.Direction.y != 0)
        {
            rb.velocity = new Vector2(joystickScript.Direction.x, joystickScript.Direction.y).normalized * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void AnimationPlayer()
    {

        if (Math.Abs(joystickScript.Direction.x) > Math.Abs(joystickScript.Direction.y))
        {
            parameter = 0;
        }
        else if (joystickScript.Direction.y > 0)
        {
            parameter = 1;
        }
        else if (joystickScript.Direction.y < 0)
        {
            parameter = -1;
        }
        anim.SetInteger("Move", parameter);
    }

    private void FlipSprite()
    {
        if (parameter == 0)
        {
            if(joystickScript.Direction.x < 0)
            {
                sp.flipX = false;
            }
            else if (joystickScript.Direction.x > 0)
            {
                sp.flipX = true;
            }
        }
        if (health < 0.1f)
        {
            anim.SetTrigger("Die");
            rb.velocity = Vector2.zero;
            gameObject.GetComponent<Attack>().canAttack = false;
        }
    }

    private void Appear()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mana"))
        {
            float valueLevel = (float)collision.gameObject.GetComponent<DestroyMana>().powerUp / (10 * level);
            sliderMana.value += valueLevel;
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            amountOfCoin += collision.gameObject.GetComponent<DestroyMana>().powerUp;
            amountOfCoin_text.text = amountOfCoin.ToString();
        }
    }

    private void ResetValue()
    {
        if (sliderMana.value == 1)
        {
            level += 1;
        }
    }

    public void CheckLife()
    {
        ManagerGame.instance.result = ManagerGame.Results.Lose;
    }
}
