using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]public int          leafPoints = 5;
    [SerializeField]public GameObject   fadeIn = null;
    [SerializeField]public AudioSource  backgroundMusic = null;
    [SerializeField]public Jauge        healthJauge = null;
    [SerializeField]public Jauge        pointsJauge = null;

    [SerializeField]private int             leafCounter = 0;
    [SerializeField]private int             leafCounterSinceStart = 0;
    [SerializeField]private int             deathCounterSinceStart = 0;
    [SerializeField]private int             unlockedStages = 0;
    [SerializeField]private float           lastHealthPoints = 0;
    [SerializeField]private Vector3         lastPosition = Vector3.zero;
    [SerializeField]private List<string>    eatenLeafs = null;
    [SerializeField]private bool            diary = false;

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
        this.leafCounterSinceStart = PlayerPrefs.GetInt("TotalScore", 0);
        this.deathCounterSinceStart = PlayerPrefs.GetInt("Death", 0);
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
        SceneManager.sceneLoaded += this.CountLeaves;
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
            PlayerPrefs.SetInt("TotalScore", this.leafCounterSinceStart);
            PlayerPrefs.SetInt("Death", this.deathCounterSinceStart);
            PlayerPrefs.Save();
        }
    }

    void CountLeaves(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 0 && scene.buildIndex != 4)
        {
            this.pointsJauge.Total = GameObject.FindGameObjectsWithTag("Leaf").Length * this.leafPoints;
            this.pointsJauge.Portion = GameManager.instance.leafCounter;
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

    public static void SetLastHP(float val)
    {
        GameManager.instance.lastHealthPoints = val;
        GameManager.instance.healthJauge.Portion = val;
    }

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
        GameManager.instance.leafCounterSinceStart += GameManager.instance.leafPoints;
        GameManager.instance.eatenLeafs.Add(name);
        GameManager.instance.pointsJauge.Portion += GameManager.instance.leafPoints;
    }

    public static void AddDeath() => GameManager.instance.deathCounterSinceStart += 1;

    public static int GetTotalPoints() => GameManager.instance.leafCounterSinceStart;

    public static int GetTotalDeath() => GameManager.instance.deathCounterSinceStart;

    public static void ResetLeafCounter()
    {
        GameManager.instance.leafCounter = 0;
        GameManager.instance.eatenLeafs.Clear();        
    }

    public static void NewGameStats()
    {
        GameManager.ResetStats();
        GameManager.instance.leafCounterSinceStart = 0;
        GameManager.instance.deathCounterSinceStart = 0;
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

    public static bool Diary() => GameManager.instance.diary;

    public static void SetDiary(bool val) => GameManager.instance.diary = val;
}
