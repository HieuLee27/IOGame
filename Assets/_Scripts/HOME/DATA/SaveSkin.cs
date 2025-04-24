using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveSkin : MonoBehaviour
{
    private static SpriteSkin instance;

    public GameObject[] itemSkin;

    private void Update()
    {
        SaveSprite();
    }

    public static string FileNameSkin() //Đường dẫn file Json
    {
        string nameFile = Application.persistentDataPath + "/Skin_Character" + ".json";

        return nameFile;
    }

    public void SaveSprite() //Lưu tên hình ảnh trang bị (Source name)
    {
        for(int i = 0; i < itemSkin.Length; i++)
        {
            if (itemSkin[i].transform.Find("Image(Clone)"))
            {
                switch (i)
                {
                    case 0:
                        instance.nameItemHat = itemSkin[i].transform.Find("Image(Clone)").GetComponent<Image>().sprite.name;
                        break;
                    case 1:
                        instance.nameItemWeapon = itemSkin[i].transform.Find("Image(Clone)").GetComponent<Image>().sprite.name;
                        break;
                    case 2:
                        instance.nameItemSkill = itemSkin[i].transform.Find("Image(Clone)").GetComponent<Image>().sprite.name;
                        break;
                }
            }
        }
        File.WriteAllText(FileNameSkin(), JsonUtility.ToJson(instance, true));
    }
}

[System.Serializable] //Cấu trức lưu dữ liệu trong Json
public struct SpriteSkin
{
    public string nameItemHat;
    public string nameItemWeapon;
    public string nameItemSkill;
}
