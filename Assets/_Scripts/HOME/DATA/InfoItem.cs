using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class InfoItem : MonoBehaviour
{
    public GameObject[] inventory;
    private ScrollRect[] scrollRect;

    private void Awake()
    {
        scrollRect = new ScrollRect[inventory.Length];
        for (int i = 0; i < inventory.Length; i++)
        {
            scrollRect[i] = inventory[i].GetComponentInChildren<ScrollRect>();
        }
    }

    public void LoadData(DataInfoItem[] data)
    {
        for (int i = 0; i < scrollRect.Length; i++)
        {
            GameObject itemsshop = scrollRect[i].content.GetChild(0).GetComponentInChildren<Transform>().gameObject;

            GameObject items = itemsshop.transform.Find("Items").gameObject; //access to "Items"

            for (int j = 0; j < items.transform.childCount; j++)
            {
                Image imageItem = items.transform.GetChild(j).transform.Find("Image").GetComponent<Image>();

                TMP_Text cost = items.transform.GetChild(j).GetComponentInChildren<Button>().GetComponentInChildren<TMP_Text>();

                TMP_Text textItem = items.transform.GetChild(j).GetComponentInChildren<TMP_Text>();

                if (j < data.Length - 1)
                {
                    imageItem.sprite = Resources.Load<Sprite>(data[j].imagePath);
                    textItem.text = data[j].name;
                    cost.text = data[j].price.ToString();
                }
                else
                {
                    break;
                }
            }
        }
    }
}

[System.Serializable]
public struct DataInfoItem
{
    public string name;
    public string imagePath;
    public string description;
    public int price;
    public int atk, def, speed;
}