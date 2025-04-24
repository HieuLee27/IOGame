using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSkin : MonoBehaviour
{
    public Image hatItem;
    public Image skillItem;
    public Image weaponItem;

    public GameObject playerSkin;

    private void Update()
    {
        UpdateSkin();
    }

    public void UpdateSkin()
    {
        if(hatItem.transform.Find("Image(Clone)") != null)
        {
            transform.Find("Hat").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            transform.Find("Hat").GetComponent<Image>().sprite = hatItem.transform.Find("Image(Clone)").GetComponent<Image>().sprite;
        }
        else
        {
            transform.Find("Hat").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }


}
