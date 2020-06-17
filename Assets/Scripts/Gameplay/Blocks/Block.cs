using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This is Block Script.
/// Main goal to be Destroyed when hit by a ball
/// </summary>


public class Block : MonoBehaviour
{
	protected int scoreWorth = 1;

	FloatEventInvoker _floatEventInvoker = null;

	protected virtual void Start()
	{
		// Advanced Event Invoker
		_floatEventInvoker = new FloatEventInvoker();
		_floatEventInvoker.AddSupportedEvent(EventName.AddPointsEvent);
	}

	
	virtual protected void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ball"))
		{
			PlayAudioDeath();
			// Invokes Specific Listeners for this event
			_floatEventInvoker.InvokeEvent(EventName.AddPointsEvent, scoreWorth);
			Destroy(this.gameObject);
		}
	}

	protected virtual void PlayAudioDeath()
	{
		AudioManager.Play(AudioClipName.BlockHit);
	}

}
