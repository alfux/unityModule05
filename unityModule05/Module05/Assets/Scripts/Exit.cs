using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public int      leafTreshold = 25;
    public Animator displayAnim = null;

    private int     id = 0;

    void Start()
    {
        this.id = Animator.StringToHash("display");
    }

    bool IsPlayer(Collider2D col, out PlayerController pass)
    {
        pass = null;
        return (
            col.transform.parent != null
            && col.transform.parent.TryGetComponent<PlayerController>(out pass)
        );
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.IsPlayer(col, out PlayerController player))
        {
            if (GameManager.TotalEaten() >= this.leafTreshold)
            {
                World.Win();
                player.SetDeath();
            }
            else
            {
                this.displayAnim.SetBool(this.id, true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            this.displayAnim.SetBool(this.id, false);
    }
}
