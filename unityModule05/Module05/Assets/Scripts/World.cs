using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class World : MonoBehaviour
{
    public Animator endScreen = null;
    public TMPro.TextMeshProUGUI text = null;

    static private bool end = false;
    static private bool win = false;

    void Start()
    {
        World.end = false;
        World.win = false;
        this.endScreen.enabled = false;
    }

    void Update()
    {
        if (World.end && !this.endScreen.enabled)
            this.endScreen.enabled = true;
        else if (World.win && !this.endScreen.enabled)
        {
            this.endScreen.enabled = true;
            this.text.text = "YOU WON";
        }
    }

    static public void GameOver() => World.end = true;
    static public void Win() => World.win = true;
    static public bool IsWin() => World.win;
}
