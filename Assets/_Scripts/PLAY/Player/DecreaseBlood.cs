using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecreaseBlood : MonoBehaviour
{
    private float hpDecrease;

    private GameObject player;

    private float percen;

    public Slider sliderBlood; //Thanh máu

    private void Start()
    {
        player = gameObject;
    }

    public void Decrease() //Hàm giảm máu
    {
        sliderBlood.value -= percen * Time.deltaTime; //Giảm máu theo phần trăm
        if (sliderBlood.value == 00) //Nếu máu bằng 0 thì chết
        {
            player.GetComponent<ControllerPlayer>().isLive = false;
            gameObject.GetComponent<ControllerPlayer>().CheckLife();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Xử lý va chạm với đạn hoặc enemy
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            hpDecrease = collision.GetComponent<Bullet>().damage;
            percen = hpDecrease / GetComponent<ControllerPlayer>().health;
            Decrease();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hpDecrease = collision.gameObject.GetComponent<EnemyController>().damage;
            percen = hpDecrease / GetComponent<ControllerPlayer>().health;
            Decrease();
        }
    }
}
