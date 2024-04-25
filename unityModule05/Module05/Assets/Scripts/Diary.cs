using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Diary : MonoBehaviour
{
    public TMPro.TextMeshProUGUI    leafPoints = null;
    public TMPro.TextMeshProUGUI    deathCount = null;
    public Image                    stage1 = null;
    public Image                    stage2 = null;
    public Image                    stage3 = null;

    void Start()
    {
        this.leafPoints.text = GameManager.GetTotalPoints().ToString();
        this.deathCount.text = GameManager.GetTotalDeath().ToString();
        switch (GameManager.GetUnlockedStages())
        {
            case 3:
                this.stage3.color = Color.white;
                this.stage2.color = Color.white;
                this.stage1.color = Color.white;
                break;
            case 2:
                this.stage2.color = Color.white;
                this.stage1.color = Color.white;
                break;
            case 1:
                this.stage1.color = Color.white;
                break;
        }
    }

    public void BackToMenu()
    {
        GameManager.SetDiary(false);
        GameManager.SetFadeIn(true);
    }
}
