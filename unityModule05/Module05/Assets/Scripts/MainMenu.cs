using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource              clickSound = null;
    public Button                   resumeButton = null;
    public TMPro.TextMeshProUGUI    resumeText = null;
    
    void Start()
    {
        if (float.IsNaN(GameManager.GetLastPos().x) ||
            float.IsNaN(GameManager.GetLastPos().y))
        {
            this.resumeButton.interactable = false;
            this.resumeText.color = Color.gray;
        }
    }

    public void OnResumeClick()
    {
        this.clickSound.Play();
        GameManager.SetFadeIn(true);
    }

    public void OnNewGameClick()
    {
        this.clickSound.Play();
        GameManager.ResetStats();
        GameManager.SetFadeIn(true);
    }

    public void OnDiaryClick()
    {
        this.clickSound.Play();
    }
}