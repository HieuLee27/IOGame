using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour
{
    [Header("UI Animation")]
    public AnimationCurve animationCurve;
    public float animationSpeed = 0.5f;

    [System.Serializable]
    public class Panel
    {
        public string panelName;
        public GameObject panelObject; // GameObject chứa Panel
    }

    public Panel[] panels; // List các panel

    // Bật Panel
    public void ShowPanel(string panelName)
    {
        foreach (Panel p in panels)
        {
            if (p.panelName == panelName)
            {
                p.panelObject.SetActive(true);
            }
        }
    }

    // Tắt Panel
    public void HidePanel(string panelName)
    {
        foreach (Panel p in panels)
        {
            if (p.panelName == panelName)
            {
                p.panelObject.SetActive(false);
            }
        }
    }

    // Bật Panel này và tắt các Panel khác (Switch Panel kiểu tab menu)
    public void SwitchPanel(string panelName)
    {
        foreach (Panel p in panels)
        {
            p.panelObject.SetActive(p.panelName == panelName);
        }
    }

    public void ShowWithAnim(string panelName)
    {
        GameObject panel = GetPanelByName(panelName);
        if (panel != null)
        {
            StartCoroutine(FadePanel(panel, true));
        }
    }

    public void HideWithAnim(string panelName)
    {
        GameObject panel = GetPanelByName(panelName);
        if (panel != null)
        {
            StartCoroutine(FadePanel(panel, false));
        }
    }

    IEnumerator FadePanel(GameObject panel, bool isShowing, System.Action onComplete = null)
    {
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = panel.AddComponent<CanvasGroup>();

        float time = 0f;
        float startAlpha = canvasGroup.alpha;
        float endAlpha = isShowing ? 1f : 0f;

        if (isShowing)
        {
            panel.SetActive(true);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        while (time < animationSpeed)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, animationCurve.Evaluate(time / animationSpeed));
            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        if (!isShowing)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            yield return new WaitForSeconds(0.1f); // Thêm delay
            panel.SetActive(false);
        }

        onComplete?.Invoke(); // Gọi callback khi hoàn thành
    }


    private GameObject GetPanelByName(string panelName)
    {
        foreach (Panel p in panels)
        {
            if (p.panelName == panelName) return p.panelObject;
        }
        return null;
    }

    public void SwitchWithAnim(string panelName)
    {
        foreach (Panel p in panels)
        {
            if (p.panelName == panelName)
                ShowWithAnim(p.panelName);
            else
                HideWithAnim(p.panelName);
        }
    }

    IEnumerator PopUpAnim(GameObject panel)
    {
        Vector3 startScale = new Vector3(0.8f, 0.8f, 1);
        Vector3 endScale = Vector3.one;

        panel.transform.localScale = startScale;
        panel.SetActive(true);

        float time = 0f;
        while (time < animationSpeed)
        {
            time += Time.deltaTime;
            panel.transform.localScale = Vector3.Lerp(startScale, endScale, animationCurve.Evaluate(time / animationSpeed));
            yield return null;
        }
    }

}
