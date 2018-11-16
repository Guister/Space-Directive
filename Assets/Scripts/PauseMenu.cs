using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour 
{
	public GameObject PausePanel;
	public bool Paused;
	
	void Start () 
	{
		Paused = false;
	}
	
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			if (Paused == false)
			{
				Pause();
			}
			else
			{
				Despause();
			}
		}
	}

	public void Pause()
	{
		Paused = true;
		PausePanel.SetActive (true);
		Time.timeScale = 0;
	}

	public void Despause()
	{
		Paused = false;
		PausePanel.SetActive (false);
		Time.timeScale = 1;
	}

	public void Restart()
	{
		Paused = false;
		Time.timeScale = 1;
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void BackToMain()
	{
		SceneManager.LoadScene ("MainMenu");
	}
}