using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamage : MonoBehaviour
{
    public float damage;

    public void DestroyThunder() //Hàm xóa tia sét
    {
        Destroy(gameObject);
    }
}
