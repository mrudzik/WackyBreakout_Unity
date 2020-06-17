using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	Text _scoreText;
	int _score;

	Text _ballsLeftText;
	int _ballsLeft;

	FloatEventInvoker _eventInvoker = null;


	[SerializeField] private Text _gameOverText;
	bool _isGameOver;

	#region MonoBehave

	private void Start()
	{
		// Score Stuff
		_score = 0;
		_scoreText = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
		if (_scoreText == null) // Just in case
			Debug.Log("Score Text not Found!");
		// Score Event Listener
		EventManager.AddListener(EventName.AddPointsEvent, HandleEvent_AddScore);

		// Balls Left Stuff
		_ballsLeft = ConfigurationUtils.BallsPerGame;
		_ballsLeftText = GameObject.FindWithTag("BallsLeftText").GetComponent<Text>();
		if (_ballsLeftText == null) // Just in case
			Debug.Log("Balls Left Text not Found!");
		// Balls Left Event Listener
		EventManager.AddListener(EventName.RemoveBallEvent, HandleEvent_RemoveBall);
		
		// Lost Ball Event Invoker
		_eventInvoker = new FloatEventInvoker();
		_eventInvoker.AddSupportedEvent(EventName.LastBallLostEvent);


		// Game Over stuff
		_gameOverText.text = "";
		_isGameOver = false;

		// Refreshing UI
		RefreshUIText();
	}

	#endregion


	#region Methods

	void RefreshUIText()
	{
		if (_scoreText == null || _ballsLeftText == null)
			return; // Protection

		_scoreText.text = "Score: " + _score;
		_ballsLeftText.text = "Balls Left: " + _ballsLeft;

		if (_isGameOver)
		{
			_gameOverText.text = "Game Over";
		}
	}

	#endregion



	#region Event Handlers

	void HandleEvent_AddScore(float toAdd)
	{
		if (toAdd < 0)
			return;

		_score += (int)toAdd;
		RefreshUIText();
	}

	void HandleEvent_RemoveBall(float notUsed)
	{
		_ballsLeft--;
		if (!_isGameOver)
		{// This checking only when game is running
			if (_ballsLeft < 0)
			{// This will be called only once
				_isGameOver = true;
				_eventInvoker.InvokeEvent(EventName.LastBallLostEvent, 0);
				//_lastBallLostEvent.Invoke(0);
			}
		}
		
		RefreshUIText();
	}

	#endregion



}
