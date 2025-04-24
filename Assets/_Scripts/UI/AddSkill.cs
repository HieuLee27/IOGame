using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.UI;

public class AddSkill : ButtonSkill
{
    [SerializeField] private GameObject player;
    private GameObject skill;

    [SerializeField] private GameObject skillPrefab;


    public void Healing() //Kỹ năng hồi máu
    {
        player.GetComponent<ControllerPlayer>().sliderHealth.value += 0.2f; //Hồi máu cho player
    }

    public void Thunder() //Kỹ năng tạo sét
    {
        skillPrefab.SetActive(true);
        if (skillPrefab.GetComponent<Thunder>().timeToSpawn > 1.5f)
        {
            skillPrefab.GetComponent<Thunder>().timeToSpawn -= 0.5f;
            player.transform.Find("ThunderSkill").GetComponent<Thunder>().maxCountOfThunder += 1;
        }
    }

    public void LevelUpDart() //Kỹ năng tăng cấp độ phi tiêu
    {
        if (player.GetComponent<Attack>().levelOfDart < 3.0f)
        {
            player.GetComponent<Attack>().levelOfDart += 0.4f;

        }
    }
}
