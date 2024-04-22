using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ItemCollector : MonoBehaviour
{
    private int currentTotalEnergy = 0;
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

        foreach (var battery in batteryValues) {

            if (collision.gameObject.CompareTag(battery.Key))
            {
                Destroy(collision.gameObject); //MARIA instead of destroy animate to shut flame off and relight after one friend goes to portal
                currentTotalEnergy = currentTotalEnergy + batteryValues[battery.Key];
                Debug.Log(batteryValues[battery.Key]);
                energyCellText.text = "Energy Cell Count: " + currentTotalEnergy;
            }
        }


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