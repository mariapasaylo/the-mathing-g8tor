using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int batteries = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Battery")) {
            Destroy(collision.gameObject);
            batteries++;
            Debug.Log("Batteries: " +  batteries);
        }
    }
}
