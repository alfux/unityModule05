using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int          leafPoints = 5;
    public AudioSource  backgroundMusic = null;

    private int             leafCounter = 0;
    private List<string>    eatenLeafs = null;

    public static GameManager   instance = null;

    void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
            Object.DontDestroyOnLoad(this.gameObject);
            return ;
        }
        Object.Destroy(this.gameObject);
    }

    void Start()
    {
        this.leafCounter = 0;
        this.eatenLeafs = new List<string>();
    }

    public static int TotalEaten() => GameManager.instance.leafCounter;

    public static bool HasBeenEaten(string name)
    {
        if (GameManager.instance.eatenLeafs != null)
        {
            return (GameManager.instance.eatenLeafs.Contains(name));
        }
        return (false);
    }

    public static void AddToLeafCounter(string name)
    {
        GameManager.instance.leafCounter += GameManager.instance.leafPoints;
        GameManager.instance.eatenLeafs.Add(name);
    }

    public static void ResetLeafCounter()
    {
        GameManager.instance.leafCounter = 0;
        GameManager.instance.eatenLeafs.Clear();        
    }

    public static void StopBackGroundMusic() => GameManager.instance.backgroundMusic.Stop();

    public static void PauseBackGroundMusic() => GameManager.instance.backgroundMusic.Pause();

    public static void PlayBackGroundMusic()
    {
        if (!GameManager.instance.backgroundMusic.isPlaying)
            GameManager.instance.backgroundMusic.Play();
    }
}
