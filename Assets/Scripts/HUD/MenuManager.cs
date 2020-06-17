using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum MenuNames
{
	MainMenu,
	HelpMenu,
	PauseMenu,
	GameOverMenu
}

public static class MenuManager
{    

	public static void GoToMenu(MenuNames menuName)
	{
		switch (menuName)
		{
			case MenuNames.MainMenu:
				AudioManager.Play(AudioClipName.ButtonSelect);
				SceneManager.LoadScene("MainMenu");
				break;
			case MenuNames.HelpMenu:
				AudioManager.Play(AudioClipName.ButtonSelect);
				SceneManager.LoadScene("HelpMenu");
				break;
			case MenuNames.PauseMenu:
				AudioManager.Play(AudioClipName.ButtonSelect);
				Object.Instantiate(Resources.Load("PauseMenu"));
				break;
			case MenuNames.GameOverMenu:
				AudioManager.Play(AudioClipName.GameOver);
				Object.Instantiate(Resources.Load("GameOverMenu"));
				break;
		}
	}
	public static void GoToGameplay()
	{
		AudioManager.Play(AudioClipName.ButtonSelect);
		SceneManager.LoadScene("Gameplay");
	}

	public static void QuitGame()
	{
		AudioManager.Play(AudioClipName.ButtonSelect);
		Application.Quit();
	//	EditorApplication.isPlaying = false;
	}

}
