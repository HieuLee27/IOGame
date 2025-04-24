using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Unity.VisualScripting;

public class CreateAvatar : MonoBehaviour
{
    public void ChangedAvatar()
    {
        string nameAvatar = gameObject.transform.parent.GetComponent<Image>().sprite.name;
        PlayerPrefs.SetString("Avatar", nameAvatar);
        PlayerPrefs.Save();
        Debug.Log("Avatar name: " + PlayerPrefs.GetString("Avatar"));
    }
}