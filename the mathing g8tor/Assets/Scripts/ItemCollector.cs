using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class ItemCollector : MonoBehaviour
{
    private int currentTotalEnergy = 0;

    //*********************************************** ADD THIS ***********************************************//
    // This attribute makes the list visible in the Unity Editor
    [SerializeField] private List<Friend> friends = new List<Friend>(); // List of friends

    // This makes the struct visible in the Unity Editor
    [System.Serializable]
    public class Friend
    {
        public Transform doorLocation; // Transform component indicating where the door is located

        public string friendName;
        public int requiredEnergy;
        public bool isFreed = false;
        public GameObject friendSpriteObject; // This will hold the reference to which friend, friend1, friend2, friend3
    }
    //*********************************************** END OF ADDITION ***********************************************//
    


    [SerializeField] private TextMeshProUGUI energyCellText;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        bool isBattery = false;
        
        foreach (var battery in GlobalVariables.batteryValues) {
           
            if (collision.gameObject.CompareTag(battery.Key))
            {
                FindObjectOfType<AudioManager>().Play("CollectObject");
                currentTotalEnergy = currentTotalEnergy + GlobalVariables.batteryValues[battery.Key];
                Debug.Log(GlobalVariables.batteryValues[battery.Key]);
                energyCellText.text = "Energy Cell Count: " + currentTotalEnergy;
                isBattery = true; // Mark as battery to avoid friend check
                break; // Exit loop once battery is found and processed
            }
        }

        if (!isBattery) {
            foreach (var friend in friends)
            {
                if (collision.gameObject == friend.friendSpriteObject && !friend.isFreed)
                {
                    if (currentTotalEnergy == friend.requiredEnergy)
                    {
                        friend.isFreed = true;
                        StartCoroutine(MoveToDoor(friend.friendSpriteObject, friend.doorLocation.position));
                    }

                    else if (currentTotalEnergy > friend.requiredEnergy)
                    {
                    Debug.Log("Too much energy collected. All energy lost!");
                    currentTotalEnergy = 0; 
                    energyCellText.text = "Energy Cell Count: " + currentTotalEnergy; 
                    }
                    else
                    {
                        Debug.Log("Not enough energy to free your friend, " + friend.friendName + ". Collect more energy!");
                    }
                }
            }
        }


        if (collision.gameObject.CompareTag("Portal")){
           if (GlobalVariables.friendCount == 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator MoveToDoor(GameObject friendObject, Vector3 doorPosition)
    {
        float speed = 2f; // Speed at which the friend moves towards the door
        while (Vector3.Distance(friendObject.transform.position, doorPosition) > 0.1f)
        {
            // Move the friend towards the door location
            friendObject.transform.position = Vector3.MoveTowards(friendObject.transform.position, doorPosition, speed * Time.deltaTime);
            yield return null; // Wait until next frame
        }

        // Once the friend reaches the door, deactivate or destroy the object
        Destroy(friendObject); // or use friendObject.SetActive(false);
        GlobalVariables.friendCount = GlobalVariables.friendCount - 1;
        Debug.Log("Friends still not freed:" + GlobalVariables.friendCount);
        currentTotalEnergy = 0;
        energyCellText.text = "Energy Cell Count: " + currentTotalEnergy; // Update UI text
    }

   


}
    

/*



 
 
 */