using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonMethod
{
    public static Vector2 GetDirection(GameObject a, GameObject b)
    {
        Vector2 direction = b.transform.position - a.transform.position;
        return direction;
    }
}
