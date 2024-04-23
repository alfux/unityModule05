using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeafItem : MonoBehaviour
{
    public AudioSource  sound = null;

    void Start()
    {
        if (GameManager.HasBeenEaten(this.name))
            Object.Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameManager.AddToLeafCounter(this.name);
            if (this.sound != null)
            {
                this.sound.volume = 0.3f;
                this.sound.Play();
            }
            Object.Destroy(this.gameObject);
        }
    }
}