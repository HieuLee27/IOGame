using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class AttracMana : MonoBehaviour
{
    private List<GameObject> listMana;
    private List<GameObject> listCoin;
    [SerializeField] private GameObject manaFood;

    public float attractiveDistance;

    private void Update()
    {
        Attractive();
    }

    private void Attractive() //Hàm thu hút mana
    {
        listMana = GameObject.FindGameObjectsWithTag(manaFood.tag).ToList();
        listCoin = GameObject.FindGameObjectsWithTag("Coin").ToList();
        listMana.AddRange(listCoin);
        if (listMana.Count != 0)
        {
            foreach (var go in listMana)
            {
                float currentDistance = Vector2.Distance(go.transform.position, transform.position);
                if (currentDistance <= attractiveDistance)
                {
                    go.GetComponent<Rigidbody2D>().velocity = CommonMethod.GetDirection(go, gameObject).normalized * 1.5f;
                }
            }
        }
    }
}
