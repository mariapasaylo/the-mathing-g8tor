using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ItemCollector : MonoBehaviour
{
    private int batteries = 0;
    [SerializeField] private TextMeshProUGUI energyCellText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Battery")) {
            Destroy(collision.gameObject);
            batteries++;
            Debug.Log("Batteries: " +  batteries);
            energyCellText.text = "Energy Cell Count: " + batteries;
        }
    }
}
