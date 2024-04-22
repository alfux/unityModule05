using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafItem : MonoBehaviour
{
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
            Object.Destroy(this.gameObject);
        }
    }
}