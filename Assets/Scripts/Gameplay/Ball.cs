using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Ball Behaviour script
/// </summary>
public class Ball : MonoBehaviour
{

	#region Fields

	Rigidbody2D _localRigid;
	Timer _lifeTimer;
	Timer _waitTimer;
	bool _isLaunched;
	bool _isAlive;
	bool _isInvisible = false;

	bool _isSpeeded = false;

	FloatEventInvoker _eventInvoker;

	#endregion

	#region MonoBehave

	// Start is called before the first frame update
	void Start()
	{
		// Getting Components
		_localRigid = GetComponent<Rigidbody2D>();

		// Setting States
		_isLaunched = false;
		_isAlive = true;
		SetupTimers();
		
		// Freeze Ball
		_waitTimer.Run();
		_localRigid.isKinematic = true;

		// Event Stuff
		_eventInvoker = new FloatEventInvoker();
		// RemoveBall
		// Ball Dies
		_eventInvoker.AddSupportedEvent(EventName.RemoveBallEvent);
		_eventInvoker.AddSupportedEvent(EventName.BallDiesEvent);

	}

	// Update is called once per frame
	void Update()
	{
		// Timers
		if (_isLaunched)
		{// When Ball is in Fly
			// Speed
			if (_isSpeeded)
			{
				if (!EffectUtils.SpeederActive)
				{
					SpeederStop();
				}
			}
			else
			{
				if (EffectUtils.SpeederActive && _isLaunched)
				{
					SpeederStart();
				}
			}
		}
	}

	/// <summary>
	/// Destroys ball when it leaves the screen
	/// </summary>
	void OnBecameInvisible()
	{
		if (!_isInvisible)
		{// Protection from using after Destroy
			_isInvisible = true;
			SelfDestruct();
		}
	}

	#endregion

	#region Methods

	/// <summary>
	/// Setuping all Ball Timers Here
	/// </summary>
	void SetupTimers()
	{
		// Spawn Wait Timer
		_waitTimer = gameObject.AddComponent<Timer>();
		_waitTimer.Duration = ConfigurationUtils.BallWaitTime;
		_waitTimer.AddListener_Timer(LaunchBall); // This will launch ball when time ends

		// Life Timer
		_lifeTimer = gameObject.AddComponent<Timer>();
		_lifeTimer.Duration = ConfigurationUtils.BallLifeTime;
		_lifeTimer.AddListener_Timer(SelfDestruct); // This will kill ball when time ends
	}

	/// <summary>
	/// Launching Ball somewhere to fly. Also Starting LifeTimer
	/// </summary>
	void LaunchBall()
	{
		_isLaunched = true;

		// Exit the freeze state
		_localRigid.isKinematic = false;
		// Pushing Ball to paddle by Physics
		_localRigid.AddForce(new Vector2(0, -ConfigurationUtils.BallImpulseForce));
		// Launching Life Timer
		_lifeTimer.Run();

		if (EffectUtils.SpeederActive)
		{
			_isSpeeded = true;
			_localRigid.AddForce(new Vector2(0, -ConfigurationUtils.BallImpulseForce));
		}
	}


	/// <summary>
	/// Setting direction for a ball by a paddle
	/// </summary>
	/// <param name="newDirection"></param>
	public void SetDirection(Vector2 newDirection)
	{
		_localRigid.velocity = new Vector2(0f, 0f);
		_localRigid.AddForce(new Vector2(
			newDirection.x * ConfigurationUtils.BallImpulseForce,
			newDirection.y * ConfigurationUtils.BallImpulseForce));
		if (EffectUtils.SpeederActive)
		{
			_localRigid.AddForce(new Vector2(
				newDirection.x * ConfigurationUtils.BallImpulseForce,
				newDirection.y * ConfigurationUtils.BallImpulseForce));
		}
	}
	
	/// <summary>
	/// Self Destructing and ask's to spawn a new ball
	/// </summary>
	void SelfDestruct()
	{
		if (_isAlive)
		{// Protection from multiple calling
			_isAlive = false;
			// Removing Ball from score
			if (_isInvisible)
				_eventInvoker.InvokeEvent(EventName.RemoveBallEvent, 0f);
			// Telling to listeners that ball died
			_eventInvoker.InvokeEvent(EventName.BallDiesEvent, 0f);
			// SelfDestroing
			Destroy(gameObject);
		}
	}


	#endregion

	#region Speeder_Handling

	private void SpeederStart()
	{
		if (!_isSpeeded)
		{
			_isSpeeded = true;
			_localRigid.velocity *= 2;
		}
	}

	private void SpeederStop()
	{
		if (_isSpeeded)
		{
			_isSpeeded = false;
			_localRigid.velocity /= 2;
		}
	}

	#endregion
}
