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

    private float percen, space;

    private void Start()
    {
        currentBlood = blood.transform.localScale;
        maxBlood = blood.transform.localScale.x;
        defaultPos = blood.transform.localPosition;
    }

    public void Decrease()
    {
        space = blood.transform.localScale.x * (percen / 100);
        currentBlood.x -= space;
        blood.transform.localScale = currentBlood;
        
        blood.transform.localPosition -= new Vector3(space/4, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            if(blood.transform.localScale.x > 0.1f)
            {
                hpDecrease = collision.GetComponent<Bullet>().damage;
                percen = (hpDecrease / GetComponent<ControllerPlayer>().health) * 100f;
                Decrease();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hpDecrease = collision.gameObject.GetComponent<EnemyController>().damage;
            percen = (hpDecrease / GetComponent<ControllerPlayer>().health) * 100f;
            Decrease();
        }
    }
}
