using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes the game
/// </summary>
public class GameInitializer : MonoBehaviour 
{
    /// <summary>
    /// Awake is called before Start
    /// </summary>
	void Awake()
    {
        // initialize screen utils
        ScreenUtils.Initialize();
		// Initializing Configuration Data
		ConfigurationUtils.Initialize();
		// Initializing Event Dictionaries
		EventManager.Initialize();

		// Adding listener to the list of LastBallLost listeners
		// This will do game over when last ball is lost
		EventManager.AddListener(EventName.LastBallLostEvent, DoGameOver);
		// This will check all blocks for their existion
		// If there are no blocks, than its a game over
		EventManager.AddListener(EventName.AddPointsEvent, FindAllBlocks);

    }


	// Update is called once per frame
	void Update()
	{
		CheckPause();
	}




	private void CheckPause()
	{
		// CheckPause
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			MenuManager.GoToMenu(MenuNames.PauseMenu);
		}
	}

	private void DoGameOver(float unused)
	{
		MenuManager.GoToMenu(MenuNames.GameOverMenu);
	}

	private void FindAllBlocks(float unused)
	{
		GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
		if (allBlocks.Length == 1)
		{
			DoGameOver(0);
		}
	}


}
