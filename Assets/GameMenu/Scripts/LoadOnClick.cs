using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour 
{
	public void LoadScene (string scene)
	{
		SceneManager.LoadScene (scene);
	}

	public void LoadMap()
	{
		SceneManager.LoadScene ("Arcade");
		Static_Gamemaster.mapname = "Map";
	} 

	public void ExitGame ()
	{
		Application.Quit ();
	}
}
