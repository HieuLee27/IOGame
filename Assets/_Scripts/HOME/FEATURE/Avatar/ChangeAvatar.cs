using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAvatar : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("default avatar: " + gameObject.GetComponent<Image>().sprite.name);
    }

    private void Update()
    {
        if (gameObject.GetComponent<Image>().sprite.name != PlayerPrefs.GetString("Avatar"))
        {
            ChangedAvatar();
        }
    }

    private void ChangedAvatar()
    {
        if (PlayerPrefs.HasKey("Avatar"))
        {
            string nameAvatar = PlayerPrefs.GetString("Avatar");
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Avatars/" + nameAvatar);
        }
    }
}
