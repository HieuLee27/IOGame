using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shopping : MonoBehaviour
{
    public GameObject inventoryShop;

    private ScrollRect scrollInventory;
    private GameObject items;

    public TMP_Text coin;

    //data
    public DataPurchased dataInfoItem = new();

    private void Awake()
    {
        
    }

    private void Start() //Khởi tạo dữ liệu
    {
        scrollInventory = inventoryShop.GetComponentInChildren<ScrollRect>();

        items = scrollInventory.content.GetChild(0).transform.GetChild(1).gameObject;

        Buying();
    }

    private void Buying() //Tham chiếu và gán sự kiện cho nút mua
    {
        dataInfoItem.infoItems = new List<DataInfoItem>();

        for (int i = 0; i < items.transform.childCount; i++)
        {
            Button btn_Buy = items.transform.GetChild(i).GetComponentInChildren<Button>();

            btn_Buy.onClick.AddListener(() => BuyItem(btn_Buy, dataInfoItem));
        }
    }

    public static string FileName() //Lấy đường dẫn file
    {
        string path = Application.persistentDataPath + "/Purchased_Items.json";
        return path;
    }

    #region ReadPurchasedItemsFile
    private void BuyItem(Button btn, DataPurchased data) //Mua vật phẩm
    {
        string nameItem = btn.transform.parent.GetComponentInChildren<TMP_Text>().text; //Khởi tạo biến tham chiếu tên vật phẩm

        string dataPurchasedItem = File.ReadAllText(FileName()); //Đọc dữ liệu vật phẩm đã mua

        for (int i = 0; i < ShowInfoItem.dataItem.infoItems.Length; i++) //Duyệt lần lượt vật phẩm trong kho
        {
            if (nameItem == ShowInfoItem.dataItem.infoItems[i].name) //Tìm vật phẩm cùng tên
            {
                Debug.Log("Square");
                Debug.Log("Name : " + ShowInfoItem.dataItem.infoItems[i].name);

                if (int.Parse(coin.text) >= ShowInfoItem.dataItem.infoItems[i].price)
                {
                    data.infoItems.Clear();
                    data.infoItems.Add(ShowInfoItem.dataItem.infoItems[i]); //Thêm dữ liệu vật phẩm vào dữ liệu tạm thời

                    //Debug.Log("Data : " + data.infoItems[i].name + " " + data.infoItems[i].price + " "
                    //        + data.infoItems[i].description); //Kiểm tra thông tin dữ liệu

                    coin.text = (int.Parse(coin.text) - ShowInfoItem.dataItem.infoItems[i].price).ToString();

                    if (dataPurchasedItem == null) //Ghi đè dữ liệu khi túi vật phẩm đã mua rỗng
                    {
                        File.WriteAllText(FileName(), JsonUtility.ToJson(data, true)); //Ghi đè dữ liệu tạm thời vào file

                        break; //Thoat vòng lặp
                    }
                    else //Thêm vật vật khi đã có dữ liệu trong túi vật phẩm
                    {
                        DataPurchased datatmp = JsonUtility.FromJson<DataPurchased>(dataPurchasedItem); //Chuyển đổi dữ liệu đa đọc được từ file

                        datatmp.infoItems.AddRange(data.infoItems);

                        File.WriteAllText(FileName(), JsonUtility.ToJson(datatmp, true)); //Ghi thêm dữ liệu vào file

                        break; //Thoát vòng lặp
                    }
                }
                break;
            }
        }
    }
    #endregion
}

[System.Serializable]
public struct DataPurchased
{
    public List<DataInfoItem> infoItems;
}