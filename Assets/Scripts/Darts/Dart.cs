using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Darts : MonoBehaviour
{
    public float speedRotation;
    public float damage;

    private void Update()
    {
        ShotDart();
    }

    private void ShotDart() // Xoay phi tiêu
    {
        transform.Rotate(0, 0, speedRotation);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision) //Xóa phi tiêu khi chạm vào cây, gỗ, tường, enemy
    {
        if(collision.gameObject.CompareTag("Tree") || collision.gameObject.CompareTag("Wood") 
            || collision.gameObject.CompareTag("Bounder") 
            || collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }


}
