using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkController : EnemyController
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private float distanceKeeping;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void CatchPlayer() // Đuổi theo player
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= distanceKeeping)
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("ATK", true);
            anim.SetBool("Run", false);
        }
        else
        {
            base.CatchPlayer();
            anim.SetBool("ATK", false);
            anim.SetBool("Run", true);
        }
    }

    public void SpawnBullet() // Bắn đạn
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= distanceKeeping)
        {
            float cornerRotate = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, cornerRotate));
            newBullet.GetComponent<Rigidbody2D>().velocity = CommonMethod.GetDirection(gameObject, player).normalized * newBullet.GetComponent<Bullet>().speed;
        }
    }
}
