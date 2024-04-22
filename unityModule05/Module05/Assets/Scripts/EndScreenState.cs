using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenState : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		GameManager.ResetLeafCounter();
		SceneManager.LoadScene(
			(SceneManager.GetActiveScene().buildIndex + (World.IsWin() ? 1 : 0))
			% SceneManager.sceneCountInBuildSettings
		);
	}
}