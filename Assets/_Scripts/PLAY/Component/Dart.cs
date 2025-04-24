using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Darts : MonoBehaviour
{
    public float speedRotation; //Tốc độ quay của phi tiêu
    public float damage; //Sát thương của phi tiêu

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
        if(/*collision.gameObject.CompareTag("Tree") || */collision.gameObject.CompareTag("Rock") 
            || collision.gameObject.CompareTag("Bounder") 
            || collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }


}
