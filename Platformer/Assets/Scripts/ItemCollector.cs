using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    private int melons = 0;

    [SerializeField] private Text melonsText; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Melon"))
        {
            Destroy(collision.gameObject);
            melons++;
            melonsText.text = "Melons: " + melons;
        }
    }
}
