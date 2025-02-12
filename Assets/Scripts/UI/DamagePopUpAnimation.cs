using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DamagePopUpAnimation : MonoBehaviour
{
    public AnimationCurve opacityCurve;
    public AnimationCurve scaleCurve;
    public AnimationCurve heightCurve;

    private TextMeshProUGUI tmp;
    private float time = 0f;
    private Vector3 origin;

    private void Awake()
    {
        tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        tmp.color = new Color(1, 1, 1, opacityCurve.Evaluate(time));
        transform.position = origin + new Vector3(0, 0.1f + heightCurve.Evaluate(time), 0);
        transform.localScale = Vector3.one * scaleCurve.Evaluate(time);
        time += Time.deltaTime;
    }
}
