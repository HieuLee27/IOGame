using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSkill : MonoBehaviour
{
    [SerializeField] protected GameObject panelSkills;

    public void TurnOff()
    {
        panelSkills.SetActive(false);
        ManagerGame.instance.isPaused = false;
    }
}
