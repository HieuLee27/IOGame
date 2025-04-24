using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UpdateSkin : MonoBehaviour
{
    private string pathFile; //Đường dẫn file Json
    public GameObject itemHat;
    private SpriteSkin instanceSkin;

    // Start is called before the first frame update
    void Start()
    {
        pathFile = SaveSkin.FileNameSkin(); //Lấy đường dẫn file Json
        ReadItemName(); //Đọc tên hình ảnh trang bị từ file Json
    }

    public void ReadItemName()
    {
        string data = File.ReadAllText(pathFile); //Đọc file Json
        Debug.Log("Data: " + data); //In ra dữ liệu Json để kiểm tra
        instanceSkin = JsonUtility.FromJson<SpriteSkin>(data); //Chuyển đổi Json thành struct
        string nameItem = instanceSkin.nameItemHat; //Lấy tên hình ảnh trang bị
        if (nameItem != null)
        {
            itemHat.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ImageItems/" + nameItem);
        }
    }
}
