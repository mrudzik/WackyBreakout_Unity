using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeederEffectMonitor : MonoBehaviour
{
	private static bool _speederActive = false;
	public static bool SpeederActive => _speederActive;

	private Timer _speederTimer = null;

	private void Start()
	{
		EventManager.AddListener(EventName.SpeederEvent, HandlePickupSpeederEvent);
		_speederTimer = gameObject.AddComponent<Timer>();
		_speederTimer.AddListener_Timer(HandleSpeederTimerFinish);
	}

	private void HandlePickupSpeederEvent(float time)
	{
		if (!_speederActive)
		{
			_speederActive = true;
			_speederTimer.Duration = time;
			_speederTimer.Run();
		}
		else
		{
			_speederTimer.Duration += time;
		}
	}

	private void HandleSpeederTimerFinish()
	{
		_speederActive = false;
	}
}
