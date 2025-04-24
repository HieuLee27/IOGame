using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : EnemyController
{
    private float defaultDamage;

    private void Start()
    {
        defaultDamage = damage;
    }

    public override void Update()
    {
        base.Update();
        DeleteDamage();
    }

    private void OnCollisionStay2D(Collision2D collision) // Tấn công khi va chạm với player
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("Attack", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) // Ngừng tấn công khi không va chạm với player
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("Attack", false);
        }
    }

    private void DeleteDamage() // Xóa sát thương
    {
        damage = 0;
    }

    public void MakeDamage() // Tạo sát thương
    {
        damage = defaultDamage;
    }
}
