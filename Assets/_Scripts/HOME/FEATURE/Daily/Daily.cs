using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daily : MonoBehaviour
{
    public int lastDate;
    public RectTransform view;

    public int Day_1;
    public int Day_2;

    public GameObject OFF_1;
    public GameObject ACTIVE_1;
    public GameObject CHECK_1;

    public GameObject OFF_2;
    public GameObject ACTIVE_2;
    public GameObject CHECK_2;

    private void Start()
    {
        Day_1 = PlayerPrefs.GetInt("Day 1");
        Day_2 = PlayerPrefs.GetInt("Day 2");
        lastDate = PlayerPrefs.GetInt("lastDate");

        Reward();

        if(lastDate != System.DateTime.Now.Day)
        {
            if(Day_1 == 0)
            {
                Day_1 = 1;
            }
            else if(Day_2 == 0)
            {
                Day_2 = 1;
            }

            Reward();
        }
    }

    public void Reward()
    {
        if(Day_1 == 0)
        {
            OFF_1.SetActive(true);
            ACTIVE_1.SetActive(false);
            CHECK_1.SetActive(false);
        }
        if(Day_1 == 1)
        {
            OFF_1.SetActive(false);
            ACTIVE_1.SetActive(true);
            CHECK_1.SetActive(false);
        }
        if (Day_1 == 2)
        {
            OFF_1.SetActive(false);
            ACTIVE_1.SetActive(false);
            CHECK_1.SetActive(true);
        }


        if (Day_2 == 0)
        {
            OFF_2.SetActive(true);
            ACTIVE_2.SetActive(false);
            CHECK_2.SetActive(false);
        }
        if (Day_2 == 1)
        {
            OFF_2.SetActive(false);
            ACTIVE_2.SetActive(true);
            CHECK_2.SetActive(false);
        }
        if (Day_2 == 2)
        {
            OFF_2.SetActive(false);
            ACTIVE_2.SetActive(false);
            CHECK_2.SetActive(true);
        }
    }

    public void GetReward_1()
    {
        lastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("lastDate", lastDate);
    
        print("Reward 1");

        Day_1 = 2;
        PlayerPrefs.SetInt("Day 1", 2);

        Reward();
    }

    public void GetReward_2()
    {
        lastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("lastDate", lastDate);

        print("Reward 2");

        Day_2 = 2;
        PlayerPrefs.SetInt("Day 2", 2);

        Reward();
    }
}
