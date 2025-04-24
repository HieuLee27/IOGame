using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Description : MonoBehaviour
{
    public Image image;
    public TMP_Text nameItem;
    public TMP_Text description;
    private ShowInfoItem.DataItem data; //Dữ liệu trong file 

    public void LoadInfo()
    {

        string dataTmp = File.ReadAllText(ShowInfoItem.FilePath());

        data = JsonUtility.FromJson<ShowInfoItem.DataItem>(dataTmp);

        if(gameObject.transform.Find("Image").transform.Find("Image(Clone)") != null)
        {
            image.sprite = gameObject.transform.Find("Image").transform.Find("Image(Clone)").GetComponent<Image>().sprite;
        }
        else
        {
            image.sprite = gameObject.transform.Find("Image").GetComponent<Image>().sprite;
        }

        nameItem.text = gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text;

        for (int i = 0; i < data.infoItems.Length; i++)
        {
            if (data.infoItems[i].name == gameObject.transform.GetChild(1).GetComponentInChildren<TMP_Text>().text)
            {
                description.text = data.infoItems[i].description + "\nAttack : " + data.infoItems[i].atk + "\nDefend : " + data.infoItems[i].def + "\nSpeed : " + data.infoItems[i].speed;
                break;
            }
        }
    }

    
}
