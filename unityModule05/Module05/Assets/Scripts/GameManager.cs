using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int          leafPoints = 5;
    public GameObject   fadeIn = null;
    public AudioSource  backgroundMusic = null;

    private int             leafCounter = 0;
    private int             unlockedStages = 0;
    private float           lastHealthPoints = 0;
    private Vector3         lastPosition = Vector3.zero;
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
        this.leafCounter = PlayerPrefs.GetInt("Score", 0);
        this.unlockedStages = PlayerPrefs.GetInt("Stages", 1);
        this.lastHealthPoints = PlayerPrefs.GetFloat("HP", 3);
        this.lastPosition.x = PlayerPrefs.GetFloat("X", float.NaN);
        this.lastPosition.y = PlayerPrefs.GetFloat("Y", float.NaN);
        this.eatenLeafs = new List<string>();
        for ((string val, int i) = (PlayerPrefs.GetString("0", "-1"), 0);
            val != "-1";
            val = PlayerPrefs.GetString((++i).ToString(), "-1"))
        {
            this.eatenLeafs.Add(val);
        }
    }

    void OnDestroy()
    {
        if (this == GameManager.instance)
        {
            int i = 0;
    
            PlayerPrefs.SetInt("Score", this.leafCounter);
            PlayerPrefs.SetInt("Stages", this.unlockedStages);
            PlayerPrefs.SetFloat("HP", this.lastHealthPoints);
            PlayerPrefs.SetFloat("X", this.lastPosition.x);
            PlayerPrefs.SetFloat("Y", this.lastPosition.y);
            foreach (string elem in this.eatenLeafs)
            {
                PlayerPrefs.SetString(i.ToString(), elem);
                ++i;
            }
            PlayerPrefs.Save();
        }
    }

    public static void ResetStats()
    {
        GameManager.ResetLeafCounter();
        GameManager.instance.unlockedStages = Mathf.Max(SceneManager.GetActiveScene().buildIndex, 1);
        GameManager.instance.lastHealthPoints = 3;
        GameManager.instance.lastPosition.x = float.NaN;
        GameManager.instance.lastPosition.y = float.NaN;
        PlayerPrefs.DeleteAll();
    }

    public static int GetUnlockedStages() => GameManager.instance.unlockedStages;

    public static void SetUnlockedStages(int val) => GameManager.instance.unlockedStages = val;

    public static float GetLastHP() => GameManager.instance.lastHealthPoints;

    public static void SetLastHP(float val) => GameManager.instance.lastHealthPoints = val;

    public static Vector3 GetLastPos() => GameManager.instance.lastPosition;

    public static void SetLastPos(Vector3 val) => GameManager.instance.lastPosition = val;

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

    public static void SetFadeIn(bool val)
    {
        GameManager.instance.fadeIn.SetActive(val);
    }
}
