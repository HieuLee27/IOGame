using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveReward : MonoBehaviour
{
    private GameObject reward;
    private GameObject _checked;
    private DataPurchased purchasedItem;
    private ShowInfoItem.DataItem dataList;

    private TaskDataList taskList;

    private void Reward(ref GameObject reward)
    {
        GameObject parent = gameObject.transform.parent.gameObject; // Get the parent object of the current game object
        reward = parent.transform.Find("Gift").gameObject; // Find the "Reward" child object of the parent

        _checked = reward.transform.Find("Tick").gameObject; // Find the "Check" child object of the parent

        taskList = JsonUtility.FromJson<TaskDataList>(File.ReadAllText(CreateTask.FileName()));
    }

    public void Received()
    {
        Reward(ref reward); // Call the Reward method to get the reward object
        string nameReward = reward.GetComponent<Image>().sprite.name; // Get the name of the reward object

        string pathRenderer = "Hat/" + nameReward; // Create the path to the reward sprite

        string purchased = File.ReadAllText(Shopping.FileName());
        purchasedItem = JsonUtility.FromJson<DataPurchased>(purchased);
        string data = File.ReadAllText(ShowInfoItem.FilePath());
        dataList = JsonUtility.FromJson<ShowInfoItem.DataItem>(data);
        if(dataList.infoItems == null)
        {
            Debug.Log("datalist is null");
        }
        else
        {
            Debug.Log("Data is : " + data);
            Debug.Log("Data Purchased : " + purchased);
            Debug.Log("Count 0 : " + purchasedItem.infoItems.Count); // Log the count of purchased items
            Debug.Log("name reward : " + nameReward); // Log the name of the reward
            Debug.Log("Count 1 : " + dataList.infoItems.Length);
            Debug.Log("Count 2 : " + taskList.taskDataList.Count);

            for (int i = 0; i < dataList.infoItems.Length; i++)
            {
                if (dataList.infoItems[i].name == nameReward)
                {
                    purchasedItem.infoItems.Add(dataList.infoItems[i]); // Add the reward item to the purchased items list
                }
            }

            for(int i = 0; i < taskList.taskDataList.Count; i++)
            {
                if (taskList.taskDataList[i].taskStatus == 2)
                {
                    Debug.Log("Task status is 2");
                    TaskData taskDataList = taskList.taskDataList[i];
                    taskDataList.taskCheck = true; // Mark the reward as received
                    Debug.Log("Check: " + taskDataList.taskCheck); // Log the check status
                    Debug.Log("Check 1: " + taskList.taskDataList[i].taskCheck);
                    taskList.taskDataList[i] = taskDataList; // Mark the task as completed
                    Debug.Log("Check 2: " + taskList.taskDataList[i].taskCheck);
                }
            }

            File.WriteAllText(Shopping.FileName(), JsonUtility.ToJson(purchasedItem, true)); // Save the updated purchased items list to file
            File.WriteAllText(CreateTask.FileName(), JsonUtility.ToJson(taskList, true)); // Save the updated data list to file
            _checked.SetActive(true); // Activate the "Check" object to indicate that the reward has been received
            gameObject.GetComponent<Button>().interactable = false; // Disable the button to prevent further interaction
        }
    }
}
