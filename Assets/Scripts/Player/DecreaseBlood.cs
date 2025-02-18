using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseBlood : MonoBehaviour
{
    [SerializeField] private GameObject blood;
    private float hpDecrease;
    private Vector3 currentBlood;
    internal float maxBlood;
    internal Vector3 defaultPos;

    private GameObject player;

    private float percen, space;

    private void Start()
    {
        currentBlood = blood.transform.localScale;
        maxBlood = blood.transform.localScale.x;
        defaultPos = blood.transform.localPosition;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Decrease()
    {
        space = blood.transform.localScale.x * (percen / 100);
        currentBlood.x -= space;
        blood.transform.localScale = currentBlood;
        
        blood.transform.localPosition -= new Vector3(space/4, 0, 0);

        player.GetComponent<ControllerPlayer>().health -= hpDecrease;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            hpDecrease = collision.GetComponent<Bullet>().damage;
            percen = (hpDecrease / GetComponent<ControllerPlayer>().health) * 100f;
            if (blood.transform.localScale.x > 0.01f)
            {
                Decrease();
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hpDecrease = collision.GetComponent<EnemyController>().damage;
            percen = (hpDecrease / GetComponent<ControllerPlayer>().health) * 100f;
            if (blood.transform.localScale.x > 0.01f)
            {
                Decrease();
            }
        }
    }
}
