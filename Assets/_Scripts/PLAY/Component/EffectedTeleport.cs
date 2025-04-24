using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectedTeleport : MonoBehaviour
{
    public GameObject[] effectedComponent;
    private GameObject btnNextLevel;

    private void Start()
    {
        btnNextLevel = GameObject.Find("BtnNextLevel");
        btnNextLevel.SetActive(false); // Hide the button at the start
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (var component in effectedComponent)
            {
                component.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); // Reset color to white
            }
        }
        if (ManagerGame.instance.result == ManagerGame.Results.None)
        {
            btnNextLevel.SetActive(true); // Show the button
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var component in effectedComponent)
        {
            component.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0); // Set color to transparent
        }
        if (btnNextLevel != null)
        {
            btnNextLevel.SetActive(false); 
        }
    }
}
