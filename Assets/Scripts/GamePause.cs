using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
	public static bool isPause;
	public static bool isOver;
	[SerializeField] private GameObject pausePanel;
	private GameObject FadeOutManager;
	private FadeOut FadeOutScript;
	void Start()
	{
		isPause=false;
		isOver=false;
		FadeOutManager = GameObject.Find("FadeOut_Clear");
		FadeOutScript = FadeOutManager.GetComponent<FadeOut>();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.P) && !isOver)
		{
			changePause(!isPause);
		}
	}
	public void Resume()
	{
		changePause(false);
	}
	public void Restart()
	{
		changePause(false);
		FadeOutScript.StartFadeOut(1.0f, "FishGameMain");
	}
	public void Exit()
	{
		changePause(false);
		FadeOutScript.StartFadeOut(1.0f, "StartScreen");
	}
	private void changePause(bool pausing)
	{
		if(pausing)
		{
			Time.timeScale = 0;
			isPause = true;
			pausePanel.SetActive(true);
		}
		else{
			Time.timeScale = 1;
			isPause = false;
			pausePanel.SetActive(false);
		}
	}
}
