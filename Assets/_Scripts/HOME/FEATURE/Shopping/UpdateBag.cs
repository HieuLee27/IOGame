using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Transactions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBag : MonoBehaviour
{
    public GameObject[] panelInven;
    public GameObject imageItem;

    private ScrollRect[] scrollRects;
    private DataPurchased data;
    private int dataAmount;
    private int currentDataAmount;

    private void Start()
    {
        scrollRects = new ScrollRect[panelInven.Length];
    }

    private void Update()
    {
        UpdateData();
        UpdateDataAmount();
        if(currentDataAmount != dataAmount)
        {
            Debug.Log("Show Data");

            ShowDataUI(data);
            currentDataAmount = dataAmount;
        }
    }

    private void UpdateData() //Liên tục cập nhật vật phẩm trong túi
    {
        string dataV = File.ReadAllText(Shopping.FileName()); //Đọc dữ liệu vật phẩm đã mua trong file Json

        if(dataV == null)
        {
            Debug.Log("Data is null !");
        }
        else
        {
            data = JsonUtility.FromJson<DataPurchased>(dataV); //Cập nhật dữ liệu vật phẩm đã mua vào file Json
        }
        
    }

    public void ShowDataUI(DataPurchased data) //Hiển thị vật phẩm trong túi
    {
        for (int i = 0; i < panelInven.Length; i++) //Panel chứa gameObject scroll view
        {
            scrollRects[i] = panelInven[i].GetComponentInChildren<ScrollRect>();

            for (int j = 0; j < scrollRects[i].content.childCount; j++) //Hiển thị nội dung vào các chỗ trong túi
            {
                GameObject item = scrollRects[i].content.GetChild(j).gameObject;

                if (data.infoItems == null)
                {
                    Debug.Log("Sorry, Data is null !");
                }
                else 
                {
                    if(j < data.infoItems.Count)
                    {
                        GameObject slot = item.transform.Find("Image").gameObject;
                        if (!slot.transform.Find("Image(Clone)"))
                        {
                            GameObject image = Instantiate(imageItem);
                            image.transform.SetParent(slot.transform, false);

                            if (image.GetComponent<Image>() != null)
                            {
                                Image iItem = image.GetComponent<Image>();

                                iItem.sprite = Resources.Load<Sprite>(data.infoItems[j].imagePath);
                                iItem.preserveAspect = true;
                            }
                        }
                    }
                }
            }
        }
    }

    public void UpdateDataAmount() //Cập nhật số lượng sản phẩm trong file Json
    {
        dataAmount = data.infoItems.Count;
    }

    public void UpdateCurrentData(ref int currentData, int amount)
    {
        currentData = amount;
    }
}
