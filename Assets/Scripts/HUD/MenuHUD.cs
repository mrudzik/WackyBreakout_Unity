using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHUD : MonoBehaviour
{
	public void QuitGame()
	{
		MenuManager.QuitGame();
	}

	public void GoToMenuMain()
	{
		MenuManager.GoToMenu(MenuNames.MainMenu);
	}
	public void GoToMenuHelp()
	{
		MenuManager.GoToMenu(MenuNames.HelpMenu);
	}

	public void GoToGameplay()
	{
		MenuManager.GoToGameplay();
	}


}
