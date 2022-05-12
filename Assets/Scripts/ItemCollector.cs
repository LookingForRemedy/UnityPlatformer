using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int apples = 0;

    [SerializeField] private Text appleText;
    [SerializeField] private AudioSource itemCollectSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            itemCollectSoundEffect.Play();
            Destroy(collision.gameObject);
            apples++;
            appleText.text = "Apples: " + apples;
        }
    }
}
