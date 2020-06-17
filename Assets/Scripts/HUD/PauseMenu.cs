using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	// Singleton
	private static PauseMenu _instance = null;


    // Start is called before the first frame update
    protected virtual void Start()
    {
		//////////////
		if (_instance != null)
		{// Singleton Lock
			Destroy(gameObject);
			return;
		}

		_instance = this;
		//////////////

		StopTime();
    }

	private void StopTime()
	{
		Time.timeScale = 0;
	}

	public void HandleResumeButton()
	{
		AudioManager.Play(AudioClipName.ButtonSelect);

		Time.timeScale = 1;
		Destroy(gameObject);
		// Singleton Release
		_instance = null;
	}

	public void HandleQuitButton()
	{
		HandleResumeButton();
		MenuManager.GoToMenu(MenuNames.MainMenu);
	}
}
