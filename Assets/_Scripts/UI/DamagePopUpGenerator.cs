using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUpGenerator : MonoBehaviour
{
    public static DamagePopUpGenerator current;
    public GameObject prefabs;

    private void Awake()
    {
        current = this;
    }

    public void CreatePopUp(Vector3 position, string text, Color color) //Tạo pop up hiển thị damage
    {
        var popup = Instantiate(prefabs, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;
        temp.faceColor = color;

        Destroy(popup, 1f);
    }
}
