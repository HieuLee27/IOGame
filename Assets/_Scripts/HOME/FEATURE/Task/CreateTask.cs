using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class CreateTask : MonoBehaviour
{
    public RectTransform taskPanel; // Panel to display the task

    private TaskDataList taskData;

    public void Start()
    {
        taskData = new ();
        taskData.taskDataList = new List<TaskData>();
        taskData.taskDataList.Add(new TaskData { taskName = "Task 1", progress = 0, taskStatus = 0, taskRewards = "Reward1", taskCheck = false });
        Create();
    }

    public static string FileName()
    {
        string path = Application.persistentDataPath + "/TaskData.json";
        return path;
    }

    public void Create()
    {
        if(!File.Exists(FileName()))
        {
            File.WriteAllText(FileName(), JsonUtility.ToJson(taskData, true));
        }
        else
        {
            string json = File.ReadAllText(FileName());
            taskData = JsonUtility.FromJson<TaskDataList>(json);

            for(int i = 0; i < taskPanel.childCount; i++)
            {
                TMP_Text text = taskPanel.GetChild(i).GetComponentInChildren<TMP_Text>();
                Slider slider = taskPanel.GetChild(i).GetComponentInChildren<Slider>();
                Image image = taskPanel.GetChild(i).transform.Find("Gift").GetComponentInChildren<Image>();
                Button button = taskPanel.GetChild(i).GetComponentInChildren<Button>();
                Image tick = image.transform.Find("Tick").GetComponent<Image>();

                switch (taskData.taskDataList[i].taskStatus)
                {
                    case 0:
                        button.interactable = false; // Enable the button
                        break;
                    case 1:
                        button.interactable = false; // Enable the button
                        break;
                    case 2:
                        if (taskData.taskDataList[i].taskCheck == true)
                        {
                            button.interactable = false; // Enable the button
                            tick.gameObject.SetActive(true); // Show the checkmark
                        }
                        else
                        {
                            button.interactable = true; // Disable the button
                        }
                        break;
                }

                // Update the task information in the UI
                text.text = taskData.taskDataList[i].taskName + " " + taskData.taskDataList[i].taskAmount + " time";
                slider.value = taskData.taskDataList[i].progress;
                image.sprite = Resources.Load<Sprite>("ImageItems/" + taskData.taskDataList[i].taskRewards);
            }
        }
    }
}

[System.Serializable]
public struct TaskData
{
    public string taskName; // Name of the task
    public float progress; // Progress of the task (0-1)
    public int taskStatus; // 0: not started, 1: in progress, 2: completed
    public string taskRewards; // Name of rewards for completing the task (Image)
    public int taskAmount; // Amount of task to be completed
    public bool taskCheck;
}

[System.Serializable]
public class TaskDataList
{
    public List<TaskData> taskDataList; // List of task data
}
