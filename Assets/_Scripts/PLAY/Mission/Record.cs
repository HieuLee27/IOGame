using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Record : MonoBehaviour
{
    private List<GameObject> monsterList; // List of monsters in the scene

    private TaskDataList taskData; // Task data list

    private int beforeEnemyCount, currentEnemyCount; // Default enemy count

    private int destroyedEnemyCount = 0; // Count of destroyed enemies

    void Start()
    {
        taskData = JsonUtility.FromJson<TaskDataList>(File.ReadAllText(CreateTask.FileName())); // Load task data from JSON file
    }

    void Update()
    {
        Update_Mission(); // Update the mission progress

        monsterList = GameObject.FindGameObjectsWithTag("Enemy").ToList(); // Get the list of monsters in the scene
        beforeEnemyCount = monsterList.Count; // Get the default enemy count
    }

    #region Counter
    private int Destroyed_Emeny(ref int destroy)
    {
        destroy = 0; // Initialize the destroy count to 0
        currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length; // Get the current enemy count
        if (currentEnemyCount < beforeEnemyCount) // If the current enemy count is less than the previous count
        {
            destroy = beforeEnemyCount - currentEnemyCount; // Calculate the number of destroyed enemies
        }
        return destroy; // Return the number of destroyed enemies
    }

    private int Play_Count()
    {
        int amount = 0;
        if(ManagerGame.instance.result == ManagerGame.Results.Win)
        {
            amount = 1; // Return 1 if the game is won
            ManagerGame.instance.result = ManagerGame.Results.None; // Reset the game result to none
        }
        else
        {
            amount = 0; // Return 0 if the game is lost
        }
        return amount; // Return the amount based on the game result
    }
    #endregion

    #region Update_TaskData
    public void Update_Mission() 
    {
        Update_Destroy(); // Update the mission progress based on the number of destroyed enemies
        Update_PlayCount(); // Update the mission progress based on the number of plays

        string json = JsonUtility.ToJson(taskData, true); // Convert the task data to JSON format
        File.WriteAllText(CreateTask.FileName(), json); // Write the JSON data to the file
    }

    public void Update_Destroy()
    {
        for(int i = 0; i < taskData.taskDataList.Count; i++)
        {
            if(taskData.taskDataList[i].taskName == "Kill Enemy")
            {
                TaskData tmp = taskData.taskDataList[i]; // Get the current progress of the task
                tmp.progress += (Destroyed_Emeny(ref destroyedEnemyCount) / (float)tmp.taskAmount); // Update the progress based on the number of destroyed enemies

                if(tmp.progress >= 1)
                {
                    tmp.taskStatus = 2; // Set the task status to completed
                }
                taskData.taskDataList[i] = tmp; // Update the progress based on the number of destroyed enemies
            }
        }
    }

    public void Update_PlayCount()
    {
        for(int i = 0; i < taskData.taskDataList.Count; i++)
        {
            if(taskData.taskDataList[i].taskName == "Play Amount")
            {
                TaskData tmp = taskData.taskDataList[i]; // Get the current progress of the task
                tmp.progress += (Play_Count() / (float)tmp.taskAmount); // Update the progress based on the number of plays

                if(tmp.progress >= 1)
                {
                    tmp.taskStatus = 2; // Set the task status to completed
                }
                taskData.taskDataList[i] = tmp; // Update the progress based on the number of plays
            }
        }
    }
    #endregion
}
