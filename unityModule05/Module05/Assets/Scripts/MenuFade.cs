using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFade : StateMachineBehaviour
{
    void UnsetFadeIn(Scene current)
    {
        GameManager.SetFadeIn(false);
        SceneManager.sceneUnloaded -= this.UnsetFadeIn;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.sceneUnloaded += this.UnsetFadeIn;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (GameManager.Diary())
            {
                SceneManager.LoadScene(4);
            }
            else
            {
                SceneManager.LoadScene(GameManager.GetUnlockedStages());
            }
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
