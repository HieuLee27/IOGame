using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSkill : MonoBehaviour
{
    [SerializeField] protected GameObject panelSkills;

    public void TurnOff() //Tắt panelSkills sau khi chọn
    {
        panelSkills.SetActive(false);
        ManagerGame.instance.isPaused = false;
    }
}
