using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ItemCollector : MonoBehaviour
{
    private int currentTotalEnergy = 0;

    //*********************************************** ADD THIS ***********************************************//
    // This attribute makes the list visible in the Unity Editor
    [SerializeField] 
    private List<Friend> friends = new List<Friend>(); // List of friends

    // This makes the struct visible in the Unity Editor
    [System.Serializable] 
    public class Friend
    {
        public string friendName;
        public int requiredEnergy;
        public bool isFreed = false;
        public GameObject friendSpriteObject; // This will hold the reference to which friend, friend1, friend2, friend3
    }
    //*********************************************** END OF ADDITION ***********************************************//

    [SerializeField] private TextMeshProUGUI energyCellText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var batteryValues = new Dictionary<string, int>() {
            {"Battery1",1 },
            {"Battery3",3 },
            {"Battery5",5 },
            {"Battery7",7 },
            {"Battery9",9 }
        };

        //*********************************************** ADD THIS ***********************************************//
        bool isBattery = false;
        //*********************************************** END OF ADDITION ***********************************************//

        foreach (var battery in batteryValues) {

            if (collision.gameObject.CompareTag(battery.Key))
            {
                Destroy(collision.gameObject); //MARIA instead of destroy animate to shut flame off and relight after one friend goes to portal
                currentTotalEnergy = currentTotalEnergy + batteryValues[battery.Key];
                Debug.Log(batteryValues[battery.Key]);
                energyCellText.text = "Energy Cell Count: " + currentTotalEnergy;
                //*********************************************** ADD THIS ***********************************************//
                isBattery = true; // Mark as battery to avoid friend check
                break; // Exit loop once battery is found and processed
                //*********************************************** END OF ADDITION ***********************************************//
            }
        }

        //*********************************************** ADD THIS ***********************************************//
        if (!isBattery) {
            foreach (var friend in friends)
            {
                if (collision.gameObject == friend.friendSpriteObject && !friend.isFreed)
                {
                    if (currentTotalEnergy == friend.requiredEnergy)
                    {
                        friend.isFreed = true;
                        Destroy(collision.gameObject);
                        //collision.gameObject.SetActive(false); // Consider animating this instead per Maria's suggestion
                        Debug.Log(friend.friendName + " is freed!"); // Maybe we could put this in the UI as a message
                        currentTotalEnergy = 0;
                    }
                    else
                    {
                        Debug.Log("Not enough energy to free " + friend.friendName); // Maybe we could put this in the UI as a message
                    }
                }
            }
        }
        //*********************************************** END OF ADDITION ***********************************************//
    }
}
    

/*

        if (collision.gameObject.CompareTag("Battery1")) {
            Destroy(collision.gameObject); //MARIA instead of destroy animate to shut flame off and respawn after one friend goes to portal
            currentTotalEnergy = currentTotalEnergy + 1;
 //           Debug.Log("Batteries: " +  batteries);
            energyCellText.text = "Energy Cell Count: " + currentTotalEnergy;
        }

        if (collision.gameObject.CompareTag("Battery3"))
        {
            Destroy(collision.gameObject);
            currentTotalEnergy = currentTotalEnergy + 3;
            energyCellText.text = "Energy Cell Count: " + currentTotalEnergy;
        }


 
 
 */