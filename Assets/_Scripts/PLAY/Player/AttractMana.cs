using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class AttracMana : MonoBehaviour
{
    private List<GameObject> listMana; //Danh sách mana
    private List<GameObject> listCoin; //Danh sách coin
    [SerializeField] private GameObject manaFood; //Mana lấy tag

    public float attractiveDistance; //Khoảng cách thu hút vật phẩm

    private void Update()
    {
        Attractive();
    }

    private void Attractive() //Hàm thu hút mana
    {
        listMana = GameObject.FindGameObjectsWithTag(manaFood.tag).ToList(); //Lấy danh sách mana
        listCoin = GameObject.FindGameObjectsWithTag("Coin").ToList(); //Lấy danh sách coin
        listMana.AddRange(listCoin);
        if (listMana.Count != 0) //Nếu Scene có vật phẩm mana
        {
            foreach (var go in listMana)
            {
                float currentDistance = Vector2.Distance(go.transform.position, transform.position); //Tính khoảng cách giữa player và vật phẩm
                if (currentDistance <= attractiveDistance)
                {
                    go.GetComponent<Rigidbody2D>().velocity = CommonMethod.GetDirection(go, gameObject).normalized * 1.5f; //Tính hướng và tốc độ vật phẩm di chuyển
                }
            }
        }
    }
}
