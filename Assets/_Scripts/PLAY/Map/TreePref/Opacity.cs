using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opacity : MonoBehaviour
{
    public float opacity;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //if(GetComponent<SpriteRenderer>().color.a <= opacity)
            //{
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, opacity);
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //if(GetComponent<SpriteRenderer>().color.a < 1)
            //{
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            //}
        }
    }
}
