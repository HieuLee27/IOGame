using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.UI;

public class AddSkill : ButtonSkill
{
    private GameObject player;
    private GameObject skill;

    [SerializeField] private GameObject skillPrefab;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void Healing() //Kỹ năng hồi máu
    {
        GameObject currentBlood = player.transform.Find("Blood_Front").gameObject;
        if (currentBlood.transform.localScale.x < player.GetComponent<DecreaseBlood>().maxBlood)
        {
            float space = player.GetComponent<DecreaseBlood>().maxBlood * 30 / 100;
            if (currentBlood.transform.localScale.x + space > player.GetComponent<DecreaseBlood>().maxBlood)
            {
                space = player.GetComponent<DecreaseBlood>().maxBlood - currentBlood.transform.localScale.x;
            }
            currentBlood.transform.localScale += new Vector3(space, 0, 0);
            currentBlood.transform.localPosition += new Vector3(space / 4, 0, 0);
        }
        player.GetComponent<ControllerPlayer>().health += 100;
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
